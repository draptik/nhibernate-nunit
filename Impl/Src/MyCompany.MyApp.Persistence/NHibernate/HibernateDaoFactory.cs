namespace MyCompany.MyApp.Persistence.NHibernate
{
    public class HibernateDaoFactory : AbstractDaoFactory
    {
        #region Overrides of AbstractDaoFactory

        public override IUserDao GetUserDao()
        {
            return new UserDao();
        }

        #endregion
    }
}
