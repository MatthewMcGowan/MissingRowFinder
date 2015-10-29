namespace MissingRowFinder.DataAccess
{
    using System;

    /// <summary>
    /// Data access for the table size.
    /// </summary>
    public class TableSizeDA : BaseDA
    {
        #region Public Methods

        /// <summary>
        /// Gets the size of the table.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        /// <returns> The maximum replicated replication key.</returns>
        public long GetTableSize(int subscriberId)
        {
            // Build sql query. We want the max used Id from each table
            string sqlQuery = string.Format("SELECT MAX({1}) FROM {0} WHERE {0}.{2} = {3}", TableName, TableReplicationIdColumn, SubscriberIdColumn, subscriberId);

            // Query the publisher and subscriber to get the max Id for each
            long subscriberTableSize = GetLongResultByQuery(PublisherConnectionString, sqlQuery);
            long publisherTableSize = GetLongResultByQuery(GetSubscriberConnectionString(subscriberId), sqlQuery);

            // Return the smaller of the two
            return Math.Min(subscriberTableSize, publisherTableSize);
        }

        #endregion
    }
}
