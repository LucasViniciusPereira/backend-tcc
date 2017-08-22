namespace backend_tcc.lib.repository.Interfaces
{
    public interface IUnitOfWork
    {
        int Commit(string UserID);

        void Rollback();
    }
}
