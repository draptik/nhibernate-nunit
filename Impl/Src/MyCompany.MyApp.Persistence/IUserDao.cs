using MyCompany.MyApp.Domain;

namespace MyCompany.MyApp.Persistence
{
    public interface IUserDao : IDao<User, long?>
    {
    }
}