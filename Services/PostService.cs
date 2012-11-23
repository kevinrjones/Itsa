using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
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

        public Post Get(Guid postId)
        {
            return _postRepository.GetBlogPost(postId);
        }

        public void Update(Guid postId, string title, string content)
        {
            _postRepository.Update(postId, title, content);
        }

        public void Delete(Guid postId)
        {
            _postRepository.Delete(postId);
        }

        public void CreatePost(Post post)
        {
            _postRepository.Create(post);
        }
    }
}
