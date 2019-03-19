using System;

namespace Data.Repository.Base
{
    public abstract class LogEntityBase<TKey> : EntityBase<TKey>
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        protected LogEntityBase()
        {
            CreatedBy = string.Empty;
            CreatedDate = DateTime.Now;
            IsDeleted = false;
        }
    }
}
