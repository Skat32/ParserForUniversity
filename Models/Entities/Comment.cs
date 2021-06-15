using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public class Comment : Base, IUpdatableEntity, ISoftDeletableEntity
    {
        public string CommentText { get; set; }

        /// <summary>
        /// Ссылка на пользователя
        /// </summary>
        public User User { get; set; }
        
        public Guid UserId { get; set; }

        /// <summary>
        /// Дата создания комментария
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Ссылка на родительский комментарий
        /// </summary>
        public Comment? CommentParent { get; set; }
        public Guid? CommentParentId { get; set; }

        /// <summary>
        /// Комментарии дети
        /// </summary>
        public IEnumerable<Comment> CommentChildren { get; set; }

        /// <summary>
        /// Идентификатор родительского комментария в распаршиваемой системе
        /// </summary>
        public int? CommentParentIdInSystem { get; set; }

        /// <summary>
        /// Комментарий оставлен автором поста
        /// </summary>
        public bool IsPostAuthor { get; set; }
        
        /// <summary>
        /// Оценка
        /// </summary>
        public int Score { get; set; }
        
        /// <summary>
        /// Ссылка на пост
        /// </summary>
        public Post Post { get; set; }
        public Guid PostId { get; set; }

        public DateTime TimePublished { get; set; }

        public DateTime TimeChanged { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        
        public void Delete()
        {
            IsDeleted = true;
        }
    }
}