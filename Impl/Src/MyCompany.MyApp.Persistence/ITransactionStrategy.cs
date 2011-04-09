namespace MyCompany.MyApp.Persistence
{
    public interface ITransactionStrategy
    {
        void Begin();
        void Commit();
        void Rollback();
    }
}
