using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue several items with different priorities and dequeue once.
    //           Items: A (1), B (3), C (2)
    // Expected Result: Dequeue returns "B" because it has the highest priority.
    // Defect(s) Found: Dequeue does not remove the returned item from the queue (subsequent dequeues can return the same item again).
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);

        var first = priorityQueue.Dequeue();
        Assert.AreEqual("B", first);
    }

    [TestMethod]
    // Scenario: Enqueue several items with different priorities and dequeue repeatedly until empty order is verified.
    //           Items: A (1), B (3), C (2)
    // Expected Result: Dequeue sequence should be: "B", "C", "A"
    //                  because B highest, then C, then A.
    // Defect(s) Found: Dequeue does not remove items from the queue, so the same highest priority item may be returned repeatedly.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items where the LAST item has the highest priority.
    //           Items: A (1), B (2), C (10)
    // Expected Result: Dequeue returns "C" (highest priority item).
    // Defect(s) Found: Dequeue search loop does not examine the last element (off-by-one), so it may miss a highest-priority item at the end.
    public void TestPriorityQueue_HighPriorityAtEnd()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 10);

        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items with the same highest priority and ensure FIFO among ties.
    //           Items: A (5), B (5), C (1)
    // Expected Result: Dequeue returns "A" because A and B tie for highest priority,
    //                  and A is closest to the front (FIFO rule for same priority).
    // Defect(s) Found: Dequeue uses >= when comparing priorities, which can select the later item in a tie (violates FIFO tie-breaking).
    public void TestPriorityQueue_TieUsesFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: InvalidOperationException thrown with message "The queue is empty."
    // Defect(s) Found: None (if implemented correctly).
    public void TestPriorityQueue_EmptyQueueThrows()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }
}
