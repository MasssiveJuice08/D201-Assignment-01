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
    private Dictionary<string, MovieNode> movieIDToNode; // hashtable
    public int Count { get; private set; }

    public MovieLinkedList() // constructor for hashtable
    {
      movieIDToNode = new Dictionary<string, MovieNode>();
    }

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
      movieIDToNode[movie.MovieID] = newNode; // add to hashtable
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
      movieIDToNode[movie.MovieID] = newNode; // add to hashtable
    }

    // remove by MovieID
    public bool Remove(string movieID)
    {
      if (head == null) return false;

      if (!movieIDToNode.ContainsKey(movieID)) return false;

      MovieNode nodeToRemove = movieIDToNode[movieID];

      // if node to remove is head
      if (nodeToRemove == head)
      {
        head = head.Next;
        if (head == null)
        {
          tail = null;
        }
      }
      else
      { 
        // find previous node
        MovieNode current = head;
        while (current.Next != nodeToRemove)
        {
          current = current.Next;
        }

        current.Next = nodeToRemove.Next;
        if (current.Next == null)
        {
          tail = current;
        }
      }

      movieIDToNode.Remove(movieID); // remove from hashtable
      Count--;
      return false;
    }

    // search by MovieID
    public Movie Find(string movieID)
    {
      if (movieIDToNode.TryGetValue(movieID, out MovieNode node))
      {
        return node.Data;
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
      movieIDToNode.Clear();
      Count = 0;
    }

    public bool IsEmpty()
    {
      return head == null;
    }

    // sort by title
    public void SortByTitle()
    {
      List<Movie> movies = ToList();
      BubbleSort.SortByTitle(movies);
      Clear();
      foreach (Movie movie in movies)
      {
        AddLast(movie);
      }
    }

    public void SortByYear()
    {
      List<Movie> movies = ToList();
      List<Movie> sortedMovies = MergeSort.SortByYear(movies);
      Clear();
      foreach (Movie movie in sortedMovies)
      {
        AddLast(movie);
      }
    }

    // linear search by title
    public List<Movie> SearchByTitle(string title)
    {
      List<Movie> results = new List<Movie>();
      MovieNode current = head;

      while (current != null)
      {
        if (current.Data.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0)
        {
          results.Add(current.Data);
        }
        current = current.Next;
      }

      return results;
    }
  }
}
