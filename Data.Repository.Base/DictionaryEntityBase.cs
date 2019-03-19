namespace Data.Repository.Base
{
    public class DictionaryEntityBase<TKey> : EntityBase<TKey>
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
