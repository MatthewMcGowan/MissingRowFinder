namespace MissingRowFinder.BusinessService
{
    using System.Collections.Generic;

    /// <summary>
    /// A collection of partition nodes.
    /// </summary>
    public class PartitionNodeCollection
    {
        #region Fields

        /// <summary>
        /// The list of node ends.
        /// A node end represent a missing Id.
        /// </summary>
        private readonly List<long> nodeEndList;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="PartitionNodeCollection"/> class.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        public PartitionNodeCollection(int subscriberId)
        {
            // Obtain the table's max Id
            var tableSize = new TableSize(subscriberId);
            long maxId = tableSize.MaxId;
            
            // Set privates
            SubscriberId = subscriberId;
            nodeEndList = new List<long>();
            
            // Create starting node for this subscriber, between zero and max replicated Id
            StartingNode = new PartitionNode(subscriberId, 0L, maxId);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the subscriber identifier.
        /// </summary>
        /// <value>
        /// The subscriber identifier.
        /// </value>
        public int SubscriberId { get; private set; }

        /// <summary>
        /// Gets the starting node.
        /// </summary>
        /// <value>
        /// The starting node.
        /// </value>
        public PartitionNode StartingNode { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the missing ids.
        /// </summary>
        /// <returns>
        /// Missing Ids.
        /// </returns>
        public List<long> GetMissingIds()
        {
            CreateChildNodes(StartingNode);
            return nodeEndList;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates the child nodes recursively, and adds missing Table IDs 
        /// if it is a final node.
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        private void CreateChildNodes(PartitionNode parentNode)
        {
            // Ignore intervals with no difference between publisher and subscriber
            if (parentNode.NodePartitionPair.DifferenceCount > 0)
            {
                // If the interval size > 0
                if (parentNode.MaxId != parentNode.MinId)
                {
                    // Get the interval and divide it in two
                    long intervalSize = parentNode.MaxId - parentNode.MinId + 1;
                    long midpoint = parentNode.MinId + (intervalSize / 2);

                    // Assign each half of the interval to a new child node
                    parentNode.LeftNode = new PartitionNode(SubscriberId, parentNode.MinId, midpoint - 1);
                    parentNode.RightNode = new PartitionNode(SubscriberId, midpoint, parentNode.MaxId);

                    // Create new child nodes for each child node
                    CreateChildNodes(parentNode.LeftNode);
                    CreateChildNodes(parentNode.RightNode);
                }
                else
                {
                    // If it's the final node which can't be split further, it's a single value.
                    // Add it to the list of missing IDs
                    nodeEndList.Add(parentNode.MinId);
                }
            }
        }

        #endregion
    }
}
