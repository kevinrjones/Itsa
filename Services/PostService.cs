using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Exceptions;
using ItsaRepository.Interfaces;
using ServiceInterfaces;

namespace Services
{
    public class PostService : IPostService
    {
        private IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public Post GetPost(Guid postId)
        {
            return (from e in _postRepository.Entities
                    where e.Id == postId
                    select e).FirstOrDefault();
        }

        public void UpdatePost(Post post)
        {
            if (GetPost(post.Id) != null)
            {
                _postRepository.Update(post);
            }
            else
            {
                throw new ItsaException("Cannot find post to update");
            }
        }

        public void DeletePost(Guid postId)
        {
            var post = GetPost(postId);            
            _postRepository.Delete(post);
        }

        public Post CreatePost(Post post)
        {
            return _postRepository.Create(post);
        }

        public int GetCountOfPostsForBlog()
        {
            return _postRepository.Entities.Count();
        }

        public IList<Post> GetPosts()
        {
            return _postRepository.Entities.ToList();
        }
    }
}
