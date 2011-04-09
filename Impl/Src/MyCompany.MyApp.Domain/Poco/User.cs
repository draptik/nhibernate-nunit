using System;

namespace MyCompany.MyApp.Domain.Poco
{
    [Serializable]
    public class User : AbstractDomainObject<Guid>
    {
        #region Constants for HQL
        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Konstanten mit den Namen der Klasse und aller Properties für HQL
        ///</summary>
        /// ------------------------------------------------------------------------------------------------------------
        public const string CLASSNAME = "User";
        public const string PROP_FIRSTNAME = "FirstName";
        public const string PROP_LASTNAME = "LastName";
        #endregion



        #region Properties
        public virtual String FirstName { get; set; }
        public virtual String LastName { get; set; }
        #endregion


        #region Overrides of AbstractDomainObject<long?>

        #region GetHashCode
        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///</summary>
        /// ------------------------------------------------------------------------------------------------------------
        public override int GetHashCode()
        {
            int result = GetType().FullName.GetHashCode();
            result += 29 * (this.FirstName == null ? 0 : this.FirstName.GetHashCode());
            result += 29 * (this.LastName == null ? 0 : this.LastName.GetHashCode());
            return result;
        }
        #endregion

        #endregion
    }
}
