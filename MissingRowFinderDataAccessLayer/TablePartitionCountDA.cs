namespace MissingRowFinder.DataAccess
{
    /// <summary>
    /// The count of rows within a given range of a table.
    /// </summary>
    public class TablePartitionCountDA : BaseDA
    {
        #region Methods

        /// <summary>
        /// Gets the publisher table partition count.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        /// <param name="minId">The minimum identifier.</param>
        /// <param name="maxId">The maximum identifier.</param>
        /// <returns>
        /// The number of database rows between the two input IDs, for a particular
        /// subscriber at publisher.
        /// </returns>
        public long GetPublisherTablePartitionCount(int subscriberId, long minId, long maxId)
        {
            // Create sql query
            string sqlQuery = GetSqlPartitionCountQuery(subscriberId, minId, maxId);

            // Query the publisher
            return GetLongResultByQuery(PublisherConnectionString, sqlQuery);
        }

        /// <summary>
        /// Gets the subscriber table partition count.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        /// <param name="minId">The minimum identifier for this interval.</param>
        /// <param name="maxId">The maximum identifier for this interval.</param>
        /// <returns>The number of database rows between the two input IDs, for a particular
        /// subscriber at publisher.</returns>
        public long GetSubscriberTablePartitionCount(int subscriberId, long minId, long maxId)
        {
            // Create sql query
            string sqlQuery = GetSqlPartitionCountQuery(subscriberId, minId, maxId);

            // Get the connection string for this subscriber
            string subscriberConnectionString = GetSubscriberConnectionString(subscriberId);

            // Query the subscriber
            return GetLongResultByQuery(subscriberConnectionString, sqlQuery);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the SQL partition count query.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        /// <param name="minId">The minimum identifier.</param>
        /// <param name="maxId">The maximum identifier.</param>
        /// <returns>The sql query.</returns>
        private string GetSqlPartitionCountQuery(int subscriberId, long minId, long maxId)
        {
            return string.Format(
                    "SELECT COUNT(*) FROM {0} WHERE {0}.{2} = {3} AND {0}.{1} BETWEEN {4} AND {5};",
                    TableName,
                    TableReplicationIdColumn,
                    SubscriberIdColumn,
                    subscriberId,
                    minId,
                    maxId);
        }

        #endregion
    }
}
