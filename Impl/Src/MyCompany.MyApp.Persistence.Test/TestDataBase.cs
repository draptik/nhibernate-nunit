using System;
using log4net;
using MyCompany.MyApp.Domain;
using MyCompany.MyApp.Persistence.NHibernate;
using NUnit.Framework;

namespace MyCompany.MyApp.Persistence.Test
{
    [TestFixture]
    public class TestDataBase
    {
        #region Fields and Properties

        private static readonly ILog Log = LogManager.GetLogger(typeof(TestDataBase));
        private ITransactionStrategy _transaction;
        private AbstractDaoFactory _daoFactory;

        #endregion

        #region Test Setup/Teardown

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [SetUp]
        public void SetUp()
        {
            _transaction = new DefaultTransactionStrategy();
            _daoFactory = new HibernateDaoFactory();
        }

        #endregion


        #region TESTS

        [Test]
        public void CreateDatabase()
        {
            try
            {
                _transaction.Begin();
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                Log.Error("PL-E-CREATEDB, ", ex);
                _transaction.Rollback();
            }
        }

        [Test]
        public void FillDatabase()
        {
            try
            {
                var start = DateTime.Now;
                _transaction.Begin();

                InsertUser();
                InsertMultipleUsers(1000);

                _transaction.Commit();
                var done = DateTime.Now;
                var duration = done - start;
                Log.Info("Duration of tests (ms): " + duration.TotalMilliseconds);
            }
            catch (Exception ex)
            {
                Log.Error("PL-E-FILLDB, ", ex);
                _transaction.Rollback();
            }
        }

        #endregion




        private void InsertUser()
        {
            var user = new User {FirstName = "Homer", LastName = "Simpson"};
            _daoFactory.GetUserDao().SaveOrUpdate(user);
        }

        private void InsertMultipleUsers(int numUsers)
        {
            for (var i = 0; i < numUsers; i++)
            {
                var user = new User {FirstName = "FirstName_" + i, LastName = "LastName_" + i};
                _daoFactory.GetUserDao().SaveOrUpdate(user);
            }
        }
    }
}