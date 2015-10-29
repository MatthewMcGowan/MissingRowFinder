namespace MissingRowFinder.BusinessService
{
    using System;

    /// <summary>
    /// A pair of partition counts.
    /// </summary>
    public class PartitionPair
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="PartitionPair"/> class.
        /// </summary>
        /// <param name="subscriberPartitionCount">The subscriber partition count.</param>
        /// <param name="publisherPartitionCount">The publisher partition count.</param>
        public PartitionPair(SubscriberPartitionCount subscriberPartitionCount, PublisherPartitionCount publisherPartitionCount)
        {
            // Set the children
            Children = new Tuple<SubscriberPartitionCount, PublisherPartitionCount>(
                subscriberPartitionCount,
                publisherPartitionCount);
            
            // Calculate the difference
            DifferenceCount = Children.Item1.Count - Children.Item2.Count;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartitionPair"/> class.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        /// <param name="minId">The minimum identifier.</param>
        /// <param name="maxId">The maximum identifier.</param>
        public PartitionPair(int subscriberId, long minId, long maxId) : this(
            new SubscriberPartitionCount(subscriberId, minId, maxId), 
            new PublisherPartitionCount(subscriberId, minId, maxId))
        {
        }

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Gets the difference count.
        /// </summary>
        /// <value>
        /// The difference count.
        /// </value>
        public long DifferenceCount { get; private set; }

        /// <summary>
        /// Gets both subscriber and publisher table count.
        /// </summary>
        /// <value>
        /// Tuple with SubscriberPartitionCount and PublisherPartitionCount.
        /// </value>
        public Tuple<SubscriberPartitionCount, PublisherPartitionCount> Children { get; private set; }

        #endregion
    }
}
