using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public class Post : Base, IUpdatableEntity, ISoftDeletableEntity
    {
        /// <summary>
        /// Автор поста
        /// </summary>
        public User? Author { get; set; }
        public Guid? AuthorId { get; set; }

        /// <summary>
        /// Комментарии
        /// </summary>
        public IEnumerable<Comment> Comments { get; set; }

        public string UrlToPost { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public void Delete()
        {
            IsDeleted = true;
        }
    }
}