namespace Data.Repository.Base.Interfaces
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}