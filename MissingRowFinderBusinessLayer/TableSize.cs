namespace MissingRowFinder.BusinessService
{
    using DataAccess;

    /// <summary>
    /// The difference between zero and the maximum used Id.
    /// </summary>
    public class TableSize
    {
        #region Fields

        /// <summary>
        /// The subscriber identifier.
        /// </summary>
        private readonly int subscriberId;

        /// <summary>
        /// The maximum table Id.
        /// </summary>
        private readonly long maxId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="TableSize" /> class.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        public TableSize(int subscriberId)
        {
            this.subscriberId = subscriberId;
            maxId = GetMax(subscriberId);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the maximum table Id.
        /// </summary>
        /// <value>
        /// The maximum table Id.
        /// </value>
        public long MaxId
        {
            get { return maxId; }
        }

        /// <summary>
        /// Gets the subscriber identifier.
        /// </summary>
        /// <value>
        /// The subscriber identifier.
        /// </value>
        public int SubscriberId
        {
            get { return subscriberId; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the maximum table Id.
        /// </summary>
        /// <param name="subscriberId">The subscriber identifier.</param>
        /// <returns>The maximum table Id.</returns>
        private long GetMax(int subscriberId)
        {
            var tableSizeDa = new TableSizeDA();
            return tableSizeDa.GetTableSize(subscriberId);
        }

        #endregion
    }
}
