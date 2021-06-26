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
        
        /// <summary>
        /// Ссылка на пост
        /// </summary>
        public string UrlToPost { get; set; }
        
        /// <summary>
        /// Идентификатор в системе
        /// </summary>
        public int IdBySystem { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public void Delete()
        {
            IsDeleted = true;
        }
    }
}