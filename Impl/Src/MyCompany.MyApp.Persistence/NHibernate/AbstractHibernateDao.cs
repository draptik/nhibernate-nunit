using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace MyCompany.MyApp.Persistence.NHibernate
{
    public class AbstractHibernateDao<T, TId> : IDao<T, TId>
    {
        private readonly Type _persistentType = typeof(T);

        #region Implementation of IDao<T,TId>

        /// <summary>
        /// Loads an instance of type TypeOfListItem from the DB based on its ID.
        /// </summary>
        public T FindUnique(TId id, bool shouldLock)
        {
            T entity;

            if (shouldLock)
            {
                entity = (T)HibernateSession.Load(_persistentType, id, LockMode.Upgrade);
            }
            else
            {
                entity = (T)HibernateSession.Load(_persistentType, id);
            }

            return entity;
        }

        /// <summary>
        /// Loads every instance of the requested type with no filtering.
        /// </summary>
        public IList<T> Find()
        {
            return FindByCriteria(null);
        }

        /// <summary>
        /// Loads every instance of the requested type using the supplied <see cref="ICriterion" />.
        /// If no <see cref="ICriterion" /> is supplied, this behaves like <see cref="GetAll" />.
        /// </summary>
        protected IList<T> FindByCriteria(Order[] orders, params ICriterion[] criterion)
        {
            ICriteria criteria = HibernateSession.CreateCriteria(_persistentType);

            foreach (ICriterion criterium in criterion)
            {
                criteria.Add(criterium);
            }

            if (orders != null)
                foreach (Order order in orders)
                {
                    criteria.AddOrder(order);
                }

            return criteria.List<T>();
        }

        public IList<T> FindByExample(T exampleInstance, params string[] propertiesToExclude)
        {
            ICriteria criteria = HibernateSession.CreateCriteria(_persistentType);
            Example example = Example.Create(exampleInstance);

            foreach (string propertyToExclude in propertiesToExclude)
            {
                example.ExcludeProperty(propertyToExclude);
            }

            criteria.Add(example);

            return criteria.List<T>() as List<T>;
        }

        /// <summary>
        /// Looks for a single instance using the example provided.
        /// </summary>
        /// <exception cref="NonUniqueResultException" />
        public T FindUniqueByExample(T exampleInstance, params string[] propertiesToExclude)
        {
            IList<T> foundList = FindByExample(exampleInstance, propertiesToExclude);

            if (foundList.Count > 1)
            {
                throw new NonUniqueResultException(foundList.Count);
            }

            if (foundList.Count > 0)
            {
                return foundList[0];
            }
            else
            {
                return default(T);
            }
        }

        //public IList<T> GetByExample(T exampleInstance, params string[] propertiesToInclude)
        //{
        //    // get the properties that will be excluded
        //    List<string> propertiesToExclude =
        //        persistentType.GetProperties().Where(p => propertiesToInclude.Contains(p.Name) == false).Select(p => p.Name).ToList();

        //    // create the criteria based on the example and excluding the given properties
        //    ICriteria criteria = HibernateSession.CreateCriteria(persistentType);
        //    Example example = Example.Create(exampleInstance);
        //    foreach (string propertyToExclude in propertiesToExclude) {
        //        example.ExcludeProperty(propertyToExclude);
        //    }
        //    criteria.Add(example);

        //    // return the result 
        //    return criteria.List<T>();
        //}



        /// <summary>
        /// For entities that have assigned ID's, you must explicitly call Save to add a new one.
        /// See http://www.hibernate.org/hib_docs/reference/en/html/mapping.html#mapping-declaration-id-assigned.
        /// </summary>
        public T Save(T entity)
        {
            HibernateSession.Save(entity);
            return entity;
        }

        /// <summary>
        /// For entities with automatatically generated IDs, such as identity, SaveOrUpdate may 
        /// be called when saving a new entity.  SaveOrUpdate can also be called to update any 
        /// entity, even if its ID is assigned.
        /// </summary>
        public T SaveOrUpdate(T entity)
        {
            HibernateSession.SaveOrUpdate(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            HibernateSession.Delete(entity);
        }

        /// <summary>
        /// Commits changes regardless of whether there's an open transaction or not
        /// </summary>
        public void CommitChanges()
        {
            if (HibernateSessionManager.Instance.HasOpenTransaction())
            {
                HibernateSessionManager.Instance.CommitTransaction();
            }
            else
            {
                // If there's no transaction, just flush the changes
                HibernateSessionManager.Instance.GetSession().Flush();
            }
        }

        /// <summary>
        /// Exposes the ISession used within the DAO.
        /// </summary>
        protected ISession HibernateSession
        {
            get
            {
                return HibernateSessionManager.Instance.GetSession();
            }
        }
        #endregion
    }
}
