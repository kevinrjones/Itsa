using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entities;
using Exceptions;
using ItsaRepository.interfaces;
using ServiceInterfaces;

namespace Services
{
    public class MediaService : IMediaService
    {
        private readonly IMediaRepository _mediaRepository;

        public MediaService(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        #region IMediaService Members

        public Media GetMedia(int year, int month, int day, string linkKey)
        {
            return _mediaRepository
                .Entities
                .FirstOrDefault(e => e.Year == year && e.Month == month && e.Day == day && e.LinkKey == linkKey);
        }

        public IEnumerable<Media> GetMedia(int pageNumber, int pageItems)
        {
            try
            {
                return _mediaRepository.Entities
                                       .OrderBy(e => e.Year)
                                       .ThenBy(e => e.Month)
                                       .ThenBy(e => e.Day)
                                       .Skip((pageNumber - 1)*pageItems)
                                       .Take(pageItems)
                                       .ToList();
            }
            catch 
            {
                throw new ItsaException("Unable to get media");
            }
        }

        public Media CreateMedia(string fileName, string contentType, Stream inputStream, int contentLength)
        {
            var mediaToCreate = new Media(fileName, contentType, inputStream, contentLength);

            try
            {
                Media media = (from e in _mediaRepository.Entities
                               where e.FileName == mediaToCreate.FileName
                               select e).FirstOrDefault();

                if (media == null)
                {
                    mediaToCreate = _mediaRepository.Create(mediaToCreate);
                    return mediaToCreate;
                }
                throw new ItsaException("Unable to add media. The media already exists in the database");
            }
            catch (ItsaException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ItsaException("Could not create media", e);
            }
        }

        public void UpdateMedia(string fileName, string title, string caption, string description, string alternate,
                               string contentType, int alignment, int size, Stream inputStream,
                               int contentLength)
        {
            byte[] bytes = ReadBytes(inputStream, contentLength);

            // todo: url?
            var media = new Media(fileName, title, caption, description, alternate, contentType, alignment, size,
                                  bytes);
            try
            {
                _mediaRepository.Update(media);
            }
            catch (Exception e)
            {
                throw new ItsaException("Could not create media", e);
            }
        }

        public Media UpdateMediaDetails(Guid mediaId, string title, string caption, string description, string alternate)
        {
            Media medium = (from i in _mediaRepository.Entities
                            where i.Id == mediaId
                            select i).FirstOrDefault();
            if (medium == null)
            {
                throw new ItsaException("Unable to find media");
            }

            medium.Title = title;
            medium.Caption = caption;
            medium.Description = description;
            medium.Alternate = alternate;

            try
            {
                _mediaRepository.Update(medium);
            }
            catch (Exception e)
            {
                throw new ItsaException("Could not update media", e);
            }
            return medium;
        }

        public void DeleteMedia(Guid mediaId)
        {
            Media medium = (from i in _mediaRepository.Entities
                            where i.Id == mediaId
                            select i).FirstOrDefault();
            try
            {
                _mediaRepository.Delete(medium);
            }
            catch (Exception)
            {
                throw new ItsaException("Unable to delete media");
            }
        }

        public Media GetMedia(Guid mediaId)
        {
            Media medium = (from i in _mediaRepository.Entities
                            where i.Id == mediaId
                            select i).FirstOrDefault();
            return medium;
        }

        #endregion

        private static byte[] ReadBytes(Stream inputStream, int contentLength)
        {
            var bytes = new byte[contentLength];
            inputStream.Read(bytes, 0, contentLength);
            return bytes;
        }
    }
}