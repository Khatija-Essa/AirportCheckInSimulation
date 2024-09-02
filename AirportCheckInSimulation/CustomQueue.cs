using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;

public class CustomQueue<T>
{
    private List<T> elements;

    public CustomQueue()
    {
        elements = new List<T>();
    }

    public int Count => elements.Count;

    public void Enqueue(T item)
    {
        elements.Add(item);
    }

    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty");

        T item = elements[0];
        elements.RemoveAt(0);
        return item;
    }

    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("Queue is empty");

        return elements[0];
    }

    public bool IsEmpty()
    {
        return Count == 0;
    }
}