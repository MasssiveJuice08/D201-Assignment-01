using System;
using System.Collections.Generic;

namespace D201_Assignment_01
{
  public enum BorrowResult
  {
    Success,               // Movie borrowed successfully
    AlreadyBorrowing,      // User already borrowing this movie
    AddedToWaitingList,    // Movie unavailable, add user to waiting list
    AlreadyInWaitingList,  // User already in waiting list
    MovieNotFound          // Movie not found
  }

  internal class MovieLinkedList : IMovieRepository
  {
    private MovieNode head;
    private MovieNode tail;
    private Dictionary<string, MovieNode> movieIDToNode; // hashtable
    public int Count { get; private set; }

    public MovieLinkedList() // constructor for hashtable
    {
      movieIDToNode = new Dictionary<string, MovieNode>(StringComparer.OrdinalIgnoreCase);
    }

    // returns true if added, false if duplicate MovieID
    public bool AddLast(Movie movie)
    {
      if (HasDuplicateMovieID(movie.MovieID))
      {
        return false; // Reject the addition
      }
      
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
      return true;
    }

    // returns true if added, false if duplicate MovieID
    public bool AddFirst(Movie movie)
    {
      if (HasDuplicateMovieID(movie.MovieID))
      {
        return false; // Reject the addition
      }

      MovieNode newNode = new MovieNode(movie);
      newNode.Next = head;
      head = newNode;
      if (tail == null)
      {
        tail = newNode;
      }
      Count++;
      movieIDToNode[movie.MovieID] = newNode; // add to hashtable
      return true;
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
      return true;
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

    // search by MovieID using hashtable
    public Movie Find(string movieID)
    {
      if (movieIDToNode.TryGetValue(movieID, out MovieNode node))
      {
        return node.Data;
      }
      return null;
    }

    // check if a single movie has a duplicate movieID
    public bool HasDuplicateMovieID(string movieID)
    {
      MovieNode current = head;
      while (current != null)
      {
        if (current.Data.MovieID.Equals(movieID, StringComparison.OrdinalIgnoreCase))
        {
          return true;
        }
        current = current.Next;
      }
      return false;
    }

    // overload for checking a list of movies for duplicate movieIDs
    public bool HasDuplicateMovieID(List<Movie> movies)
    {
      foreach (Movie movie in movies)
      {
        if (HasDuplicateMovieID(movie.MovieID)) // Reuse original method
        {
          return true;
        }
      }
      return false;
    }

    // Bulk add method for loading from JSON (skips duplicate checks)
    public void BulkAdd(Movie movie)
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
      movieIDToNode[movie.MovieID] = newNode; // Add to hashtable
    }
  }
}
