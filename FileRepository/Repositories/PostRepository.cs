using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SystemFileAdapter;
using Entities;
using Exceptions;
using FileSystemInterfaces;
using ItsaRepository.Interfaces;
using Repository;
using Serialization;

namespace FileRepository.Repositories
{
    public class PostRepository : BaseFileRepository<Post>, IPostRepository
    {

        public PostRepository(string path, IFileInfoFactory fileInfo, IDirectoryInfo directoryInfo) : base(path + "/posts", fileInfo, directoryInfo)
        {
        }

        protected override string GenerateFileName(Post entity)
        {
            return GenerateFileName(entity.Id);
        }

        private string GenerateFileName(Guid id)
        {
            return string.Format("{0}/{1}.json", Path, id);
        }

        public IList<Post> GetPosts()
        {
            return Entities.ToList();
        }

        public Post GetBlogPost(Guid id)
        {
            return Entities.First(p => p.Id == id);
        }


        public IList<Post> GetBlogPosts(int year, int month, int day, string link)
        {
            throw new NotImplementedException();
        }

        public Post AddComment(Guid id, string name, string comment)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid postId, string title, string entry)
        {
            var p = GetBlogPost(postId);
            p.Title = title;
            p.Body = entry;
            Update(p);
        }

        public void Delete(Guid postId)
        {
            Post p = GetBlogPost(postId);
            Delete(p);
        }

        public int GetCountOfPostsForBlog()
        {
            return Entities.Count();
        }
    }
}
