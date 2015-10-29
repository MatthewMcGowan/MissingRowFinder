namespace MissingRowFinder.DataAccess
{
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;

    using Dapper;

    /// <summary>
    /// The base data access class.
    /// </summary>
    public class BaseDA
    {
        #region Protected Fields

        /// <summary>
        /// The table name.
        /// </summary>
        protected readonly string TableName = ConfigurationManager.AppSettings["TableName"];

        /// <summary>
        /// The replication Id column name.
        /// </summary>
        protected readonly string TableReplicationIdColumn = ConfigurationManager.AppSettings["TableReplicationIdColumn"];

        /// <summary>
        /// The subscriber Id column name.
        /// </summary>
        protected readonly string SubscriberIdColumn = ConfigurationManager.AppSettings["SubscriberIdColumn"];

        /// <summary>
        /// The publisher connection string.
        /// </summary>
        protected readonly string PublisherConnectionString;
        
        #endregion

        #region Private Fields

        /// <summary>
        /// The publisher configuration connection name.
        /// </summary>
        private const string PublisherConfigConnectionName = "publisher";

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="BaseDA"/> class.
        /// </summary>
        public BaseDA()
        {
            PublisherConnectionString = ConfigurationManager.ConnectionStrings[PublisherConfigConnectionName].ConnectionString;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the publisher connection.
        /// </summary>
        /// <returns>The publisher connection.</returns>
        protected SqlConnection GetPublisherConnection()
        {
            return new SqlConnection(PublisherConnectionString);
        }

        /// <summary>
        /// Gets the subscriber connection.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        /// <returns>The subscriber connection.</returns>
        protected SqlConnection GetSubscriberConnection(int subscriberId)
        {
            return new SqlConnection(GetSubscriberConnectionString(subscriberId));
        }

        /// <summary>
        /// Gets the subscriber connection string.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        /// <returns>The subscriber connection.</returns>
        protected string GetSubscriberConnectionString(int subscriberId)
        {
            return ConfigurationManager.ConnectionStrings[subscriberId.ToString()].ConnectionString;
        }

        /// <summary>
        /// Gets a single long result.
        /// </summary>
        /// <param name="sqlConnectionString">The SQL connection string.</param>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <returns> A long.</returns>
        protected virtual long GetLongResultByQuery(string sqlConnectionString, string sqlQuery)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                connection.Open();
                var queryResult = connection.Query<long>(sqlQuery);
                connection.Close();

                return queryResult.First();
            }
        }

        #endregion
    }
}
