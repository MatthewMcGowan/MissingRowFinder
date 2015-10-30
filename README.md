# MissingRowFinder
Missing Row Finder or: How I Learned To Stop Worrying and Love the Garbage Collector.

This was one of my first OOP applications.

## Problem
A common infrastructure model is to have one central database, referred to here as the Publisher, and multiple Subscriber databases with the same schema at company branches. The Publisher assigns ranges for the PK on a table to its Subscribers, and the data held at the Publisher represents the sum total of the data held at all the Subscribers. 
[In reality in the model being used both the central and local databases act as publishers and subscribers, replicating data in both directions. In particular with this scenario the local DBs publish up to the central one; the existing name convention is nevertheless followed here and in the code.]

An issue that can sometimes occur, normally after server/network issues, is that at a Subscriber one or more rows will fail to be marked for replication up to the Publisher. The presence of these rows can be found using a simple COUNT(*) and seeing a discrepency between the two DBs. Finding the Id of the missing row(s), however, is non-trivial. 

## Solution
This application queries both the Publisher and a given Subscriber, effectively performing a binary search to find the Subscriber row that does not exist at the Publisher.

i.e.

1. Between IDs 1-50 the Subscriber has 40 rows. Publisher has 30.
2. Split id range in half. 
  1. Look at first half. Subscriber has 20 rows between 1-20, Publisher also has 20. Move on and ignore this.
  2. Look at second half. Subscriber has 20 rows between 20-40, Publisher has 10. Split again
    1. Look at first half. Subscriber has 10 rows between 20-30, Publisher also has 10. Move on and ignore this.
    2. Look at second half. Subscriber has 10 rows between 30-40, Publisher has zero. Add IDs 30-40 to list.
3. Return list (containt IDs 30-40) to the user.

To prompt replication of the row an innocuous update can then be performed 
e.g. 
```
UPDATE Table SET BitFlagColumn = BitFlagColumn WHERE Id = {id}
```

## Business Layer
The Console project and Data Access are fairly straight forward, but the Business Layer warrants explanation:

* PartitionNodeCollection represents the "tree", it holds the first, root node.
* PartitionNode is one node. It has one parent, and contains two child PartitionNodes: a left and a right.
* A PartitionNode holds a PartitionPair. A PartitionPair holds the count for number of rows at Publisher (PublisherPartitionCount) and Subscriber (SubscriberPartitionCount).
* TableSize gets the Maximum replicated ID. We use this for the table size as otherwise we would also bring up any IDs that have not yet replicated.
