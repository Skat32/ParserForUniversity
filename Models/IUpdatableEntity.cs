using System;

namespace Models
{
    public interface IUpdatableEntity
    {
        /// <summary>
        /// Дата создания записи
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата изменения записи
        /// </summary>
        DateTime UpdatedAt { get; set; }
    }
}