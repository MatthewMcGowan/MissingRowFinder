namespace MissingRowFinder.BusinessService
{
    using DataAccess;

    /// <summary>
    /// The publisher partition count.
    /// </summary>
    public sealed class PublisherPartitionCount : PartitionCount
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="PublisherPartitionCount"/> class.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        /// <param name="minId">The minimum identifier.</param>
        /// <param name="maxId">The maximum identifier.</param>
        public PublisherPartitionCount(int subscriberId, long minId, long maxId)
        {
            // Set the properties with private set
            IsPublisher = true;
            SubscriberId = subscriberId;
            MinId = minId;
            MaxId = maxId;

            // Calculate the count
            Count = GetPartitionCount(subscriberId, minId, maxId);
        }

        #endregion 

        #region Methods

        /// <summary>
        /// Gets the partition count.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        /// <param name="minId">The minimum identifier.</param>
        /// <param name="maxId">The maximum identifier.</param>
        /// <returns>The number of database rows between the two input IDs, for a particular
        /// subscriber at publisher.</returns>
        protected override long GetPartitionCount(int subscriberId, long minId, long maxId)
        {
            // Create new parition count
            var tablePartitionCountDA = new TablePartitionCountDA();
            
            // Get count for publisher
            return tablePartitionCountDA.GetPublisherTablePartitionCount(subscriberId, minId, maxId);
        }

        #endregion
    }
}
