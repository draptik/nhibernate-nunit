namespace MyCompany.MyApp.Persistence
{
    public abstract class AbstractDaoFactory
    {
        public abstract IUserDao GetUserDao();
    }
}
