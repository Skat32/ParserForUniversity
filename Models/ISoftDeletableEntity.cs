namespace Models
{
    public interface ISoftDeletableEntity
    {
        /// <summary>
        /// Признак удалена ли запись в базе 
        /// </summary>
        bool IsDeleted { get; set; }
        
        void Delete();
    }
}