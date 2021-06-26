using System;

namespace Models.Entities
{
    public class Comment : Base, IUpdatableEntity, ISoftDeletableEntity
    {
        public string CommentText { get; set; }

        public int IdBySystem { get; set; }


        /// <summary>
        /// Ссылка на пользователя
        /// </summary>
        public User User { get; set; }
        
        public Guid UserId { get; set; }

        /// <summary>
        /// Ссылка на родительский комментарий
        /// </summary>
        public Comment? CommentParent { get; set; }
        public Guid? CommentParentId { get; set; }
        
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

        /// <summary>
        /// Дата публикации комментария
        /// </summary>
        public DateTime TimePublished { get; set; }

        /// <summary>
        /// Дата последнего изменения комментария
        /// </summary>
        public DateTime? TimeChanged { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        
        public void Delete()
        {
            IsDeleted = true;
        }
    }
}