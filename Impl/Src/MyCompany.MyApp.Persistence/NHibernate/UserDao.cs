using MyCompany.MyApp.Domain;

namespace MyCompany.MyApp.Persistence.NHibernate
{
    public class UserDao : AbstractHibernateDao<User, long?>, IUserDao
    {
    }
}
