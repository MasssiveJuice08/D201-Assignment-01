using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D201_Assignment_01
{
  internal class MovieLinkedList
  {
    private MovieNode head;
    private MovieNode tail;
    public int Count { get; private set; }

    public void AddLast(Movie movie)
    {
      MovieNode newNode = new MovieNode(movie);
      if (head == null)
      {
        head = newNode;
        tail = newNode;
      }
      else
      {
        tail.Next = newNode;
        tail = newNode;
      }
      Count++;
    }

    public void AddFirst(Movie movie)
    {
      MovieNode newNode = new MovieNode(movie);
      newNode.Next = head;
      head = newNode;
      if (tail == null)
      {
        tail = newNode;
      }
      Count++;
    }

    // remove by MovieID
    public bool Remove(string movieID)
    {
      if (head == null) return false;

      if (head.Data.MovieID == movieID)
      {
        head = head.Next;
        if (head == null)
        {
          tail = null;
        }
        Count--;
        return true;
      }

      MovieNode current = head;
      while (current.Next != null)
      {
        if (current.Next.Data.MovieID == movieID)
        {
          current.Next = current.Next.Next;
          if (current.Next == null)
          {
            tail = current;
          }
          Count--;
          return true;
        }
        current = current.Next;
      }
      return false;
    }

    // search by MovieID
    public Movie Find(string movieID)
    {
      MovieNode current = head;
      while (current != null)
      {
        if (current.Data.MovieID == movieID)
        {
          return current.Data;
        }
        current = current.Next;
      }
      return null;
    }

    // convert to List<Movie> for binding to ListView
    public List<Movie> ToList()
    {
      List<Movie> movies = new List<Movie>();
      MovieNode current = head;
      while (current != null)
      {
        movies.Add(current.Data);
        current = current.Next;
      }
      return movies;
    }

    // clear linked list
    public void Clear()
    {
      head = null;
      tail = null;
      Count = 0;
    }

    public bool IsEmpty()
    {
      return head == null;
    }
  }
}
