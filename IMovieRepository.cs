using System.Collections.Generic;

namespace D201_Assignment_01
{
  internal interface IMovieRepository
  {
    int Count { get; }
    bool AddLast(Movie movie);
    bool AddFirst(Movie movie);
    bool Remove(string movieID);
    List<Movie> ToList();
    void Clear();
    bool IsEmpty();
    void SortByTitle();
    void SortByYear();
    List<Movie> SearchByTitle(string title);
    Movie Find(string movieID);
    bool HasDuplicateMovieID(string movieID);
    bool HasDuplicateMovieID(List<Movie> movies);
    void BulkAdd(Movie movie);
  }
}
