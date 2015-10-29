namespace MissingRowFinder
{
    using System;
    using System.Configuration;

    using BusinessService;

    /// <summary>
    /// The console application.
    /// </summary>
    internal class Program
    {
        #region Main

        /// <summary>
        /// The program entry method.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            GetMissingIdsForSubscriber();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the missing ids for the subscriber.
        /// </summary>
        private static void GetMissingIdsForSubscriber()
        {
            int subscriberId = GetSubscriberId();

            // Get the missing IDs
            var partitionNodeCollection = new PartitionNodeCollection(subscriberId);
            var missingIdsList = partitionNodeCollection.GetMissingIds();

            // Write missing IDs to console
            if (missingIdsList.Count > 0)
            {
                Console.WriteLine("Missing IDs:");
                missingIdsList.ForEach(i => Console.Write("{0}\n", i));
            }
            else
            {
                Console.WriteLine("No IDs missing.");
            }

            // Repeat
            GetMissingIdsForSubscriber();
        }

        /// <summary>
        /// Gets the Subscriber Id.
        /// </summary>
        /// <returns>
        /// The Subscriber Id.
        /// </returns>
        private static int GetSubscriberId()
        {
            int subscriberId;
            string input = string.Empty;

            // Check SubscriberID is valid and in config
            while (!int.TryParse(input, out subscriberId) || ConfigurationManager.ConnectionStrings[input] == null)
            {
                input = PromptUserForSubscriberId();
            }

            return subscriberId;
        }

        /// <summary>
        /// Prompts the user for the Subscriber Id.
        /// </summary>
        /// <returns>User input.</returns>
        private static string PromptUserForSubscriberId()
        {
            Console.WriteLine("Enter valid Subscriber Id:");

            return Console.ReadLine();
        }

        #endregion
    }
}
