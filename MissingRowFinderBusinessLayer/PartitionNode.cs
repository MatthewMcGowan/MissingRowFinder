namespace MissingRowFinder.BusinessService
{
    public class PartitionNode
    {
        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="PartitionNode"/> class.
        /// </summary>
        /// <param name="nodePartitionPair">The node partition pair.</param>
        public PartitionNode(PartitionPair nodePartitionPair)
        {
            NodePartitionPair = nodePartitionPair;

            #region Set the properties with private set

            if (NodePartitionPair.Children.Item1.SubscriberId == nodePartitionPair.Children.Item2.SubscriberId)
            {
                SubscriberId = nodePartitionPair.Children.Item1.SubscriberId;
            }
            if (NodePartitionPair.Children.Item1.MinId == nodePartitionPair.Children.Item2.MinId)
            {
                MinId = nodePartitionPair.Children.Item1.MinId;
            }
            if (NodePartitionPair.Children.Item1.MaxId == nodePartitionPair.Children.Item2.MaxId)
            {
                MaxId = nodePartitionPair.Children.Item1.MaxId;
            }

            #endregion
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="PartitionNode"/> class.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        /// <param name="minId">The minimum identifier.</param>
        /// <param name="maxId">The maximum identifier.</param>
        public PartitionNode(int subscriberId, long minId, long maxId)
        {
            NodePartitionPair = new PartitionPair(subscriberId, minId, maxId);

            #region Set private fields

            SubscriberId = subscriberId;
            MinId = minId;
            MaxId = maxId;

            #endregion
        }

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Gets the node partition pair.
        /// </summary>
        /// <value>
        /// The node partition pair.
        /// </value>
        public PartitionPair NodePartitionPair { get; private set; }

        /// <summary>
        /// Gets the subscriber identifier.
        /// </summary>
        /// <value>
        /// The subscriber identifier.
        /// </value>
        public int SubscriberId { get; private set; }

        /// <summary>
        /// Gets the minimum identifier.
        /// </summary>
        /// <value>
        /// The minimum identifier.
        /// </value>
        public long MinId { get; private set; }

        /// <summary>
        /// Gets the maximum identifier.
        /// </summary>
        /// <value>
        /// The maximum identifier.
        /// </value>
        public long MaxId { get; private set; }

        /// <summary>
        /// Gets or sets the left node.
        /// </summary>
        /// <value>
        /// The left node.
        /// </value>
        public PartitionNode LeftNode { get; set; }

        /// <summary>
        /// Gets or sets the right node.
        /// </summary>
        /// <value>
        /// The right node.
        /// </value>
        public PartitionNode RightNode { get; set; }

        #endregion
    }
}