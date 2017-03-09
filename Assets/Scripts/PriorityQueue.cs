using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue {

    private List<NavNode> nodes = new List<NavNode>();
	
	public void Enqueue(NavNode _node)
    {
        nodes.Add(_node);
        int index = nodes.Count - 1;

        bool sorted = false;
        while (!sorted)
        {
            int parentIndex = ((index - 1) / 2);
            if (nodes[parentIndex].Priority > nodes[index].Priority)
            {
                NavNode tempNode = nodes[parentIndex];
                nodes[parentIndex] = nodes[index];
                nodes[index] = tempNode;
                index = parentIndex;
            }
            else
                sorted = true;
        }
    }

    public NavNode Dequeue()
    {
        if (nodes.Count == 0)
            return null;

        NavNode data = nodes[0];
        nodes[0] = nodes[nodes.Count - 1];
        nodes.RemoveAt(nodes.Count - 1);
        int index = 0;

        bool sorted = false;
        while(!sorted)
        {
            int leftChildIndex = (2 * index) + 1;
            int rightChildIndex = (2 * index) + 2;

            if (index >= nodes.Count)
                break;

            NavNode tempNode = nodes[index];

            if (rightChildIndex < nodes.Count &&
                leftChildIndex < nodes.Count &&
                nodes[index].Priority > nodes[leftChildIndex].Priority &&
                nodes[index].Priority > nodes[rightChildIndex].Priority)
            {
                // Bigger than both
                if (nodes[leftChildIndex].Priority < nodes[rightChildIndex].Priority)
                {
                    nodes[index] = nodes[leftChildIndex];
                    nodes[leftChildIndex] = tempNode;
                    index = leftChildIndex;
                }
                else
                {
                    nodes[index] = nodes[rightChildIndex];
                    nodes[rightChildIndex] = tempNode;
                    index = rightChildIndex;
                }
            }
            else if (leftChildIndex < nodes.Count &&
                nodes[index].Priority > nodes[leftChildIndex].Priority)
            {
                // Just bigger than left
                nodes[index] = nodes[leftChildIndex];
                nodes[leftChildIndex] = tempNode;
                index = leftChildIndex;
            }
            else if (rightChildIndex < nodes.Count &&
                nodes[index].Priority > nodes[rightChildIndex].Priority)
            {
                // Just bigger than right
                nodes[index] = nodes[rightChildIndex];
                nodes[rightChildIndex] = tempNode;
                index = rightChildIndex;
            }
            else
                sorted = true;
        }//end of loop
        return data;
    }

    private void Sort()
    {
        int index = nodes.Count - 1;
        bool sorted = false;

        while(!sorted)
        {
            int parentIndex = ((index - 1) / 2);
            if (nodes[parentIndex].Priority > nodes[index].Priority)
            {
                NavNode tempNode = nodes[parentIndex];
                nodes[parentIndex] = nodes[index];
                nodes[index] = tempNode;
                index = parentIndex;
            }
            else sorted = true;
        }
    }

    public void Remove(NavNode n)
    {
        nodes.Remove(n);
        Sort();
    }

    public NavNode Peek()
    {
        if (nodes.Count > 0)
            return nodes[0];
        else return null;
    }

    public bool IsEmpty()
    {
        return (nodes.Count == 0);
    }

    public bool Contains(NavNode n)
    {
        return nodes.Contains(n);
    }
}
