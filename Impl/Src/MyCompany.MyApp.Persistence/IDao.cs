using System.Collections.Generic;

namespace MyCompany.MyApp.Persistence
{
    public interface IDao<T, TId>
    {
        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Loads an instance of type TypeOfListItem from the DB based on its ID.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        T FindUnique(TId id, bool shouldLock);

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Loads every instance of the requested type with no filtering.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        IList<T> Find();

        /// ------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// TODO Finds records by page and sorted.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------
        //PagedResult<T> FindSortedPage(int startResultAt, int numberOfResults, string orderBy, SortOrderDirection sortOrder, Dictionary<string, string> restrictions);




        IList<T> FindByExample(T exampleInstance, params string[] propertiesToExclude);
        T FindUniqueByExample(T exampleInstance, params string[] propertiesToExclude);
        T Save(T entity);
        T SaveOrUpdate(T entity);
        void Delete(T entity);
        void CommitChanges();

    }
}
