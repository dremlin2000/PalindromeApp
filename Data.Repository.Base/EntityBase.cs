namespace Data.Repository.Base
{
    public abstract class EntityBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
