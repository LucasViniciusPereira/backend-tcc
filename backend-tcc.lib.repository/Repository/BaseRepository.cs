namespace WDNJR.DW.TCC.Domain.Repository
{
    public class BaseRepository
    {
        public string CnnString { get; private set; }

        public readonly EfDbContext Contexto;

        public BaseRepository(string cnnString)
        {
            CnnString = cnnString;

            Contexto = new EfDbContext(cnnString);
        }
    }
}