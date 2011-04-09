using FluentNHibernate.Mapping;

namespace MyCompany.MyApp.Domain.Mappings.Fluent
{
    public class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            Table("USR"); // "USER" is SQL keyword...
            Id(x => x.Id)
                .GeneratedBy.GuidComb();
            Map(x => x.FirstName);
            Map(x => x.LastName);
        }
    }
}
