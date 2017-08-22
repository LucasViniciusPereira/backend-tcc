namespace backend_tcc.lib.repository.Interfaces
{
    public interface IServiceFacadeBase
    {
        int Commit(string userID);
        void Rollback();
    }
}
