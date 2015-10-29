namespace MissingRowFinder.BusinessService
{
    public abstract class PartitionCount
    {
        #region Constructors

        /// <summary>
        /// Protected to force creation of new constructor.
        /// </summary>
        protected PartitionCount()
        {
            //Protected to force creation of new constructor.
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is publisher.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is publisher; otherwise, <c>false</c>.
        /// </value>
        public bool IsPublisher { get; protected set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public long Count { get; protected set; }

        /// <summary>
        /// Gets or sets the subscriber identifier.
        /// </summary>
        /// <value>
        /// The subscriber identifier.
        /// </value>
        public int SubscriberId { get; protected set; }

        /// <summary>
        /// Gets or sets the minimum identifier.
        /// </summary>
        /// <value>
        /// The minimum identifier.
        /// </value>
        public long MinId { get; protected set; }

        /// <summary>
        /// Gets or sets the maximum identifier.
        /// </summary>
        /// <value>
        /// The maximum identifier.
        /// </value>
        public long MaxId { get; protected set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the partition count.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        /// <param name="minId">The minimum identifier.</param>
        /// <param name="maxId">The maximum identifier.</param>
        /// <returns>The number of database rows between the two input IDs.</returns>
        protected abstract long GetPartitionCount(int subscriberId, long minId, long maxId);

        #endregion 
    }
}
