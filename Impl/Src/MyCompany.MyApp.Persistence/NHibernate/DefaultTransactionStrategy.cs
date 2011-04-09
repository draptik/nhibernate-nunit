namespace MyCompany.MyApp.Persistence.NHibernate
{
    public class DefaultTransactionStrategy : ITransactionStrategy
    {
        #region Implementation of ITransactionStrategy

        public void Begin()
        {
            HibernateSessionManager.Instance.BeginTransaction();
        }

        public void Commit()
        {
            HibernateSessionManager.Instance.CommitTransaction();
        }

        public void Rollback()
        {
            HibernateSessionManager.Instance.RollbackTransaction();
        }

        #endregion
    }
}
