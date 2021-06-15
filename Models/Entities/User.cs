using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public class User : Base, IUpdatableEntity
    {
        /// <summary>
        /// Идентификатор в распаршиваемой системе
        /// </summary>
        public int IdBySystem { get; set; }
        
        /// <summary>
        /// Имя
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Псевдоним
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Специальнотсть/описание
        /// </summary>
        public string Speciality { get; set; }
        
        /// <summary>
        /// Ссылка на пользователя
        /// </summary>
        public string UrlToUser => $"https://habr.com/ru/users/{Alias}/";

        /// <summary>
        /// Сссылка на аватар пользователя
        /// </summary>
        public string UrlToImage { get; set; }
        
        /// <summary>
        /// Посты пользователя
        /// </summary>
        public IEnumerable<Post> Posts { get; set; }

        /// <summary>
        /// Комментарии пользователя
        /// </summary>
        public IEnumerable<Comment> Comments { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}