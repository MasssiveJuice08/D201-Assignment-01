using System;
using System.Collections.Generic;

namespace D201_Assignment_01
{
  internal static class MergeSort
  {
    public static void SortByYear(List<Movie> movies)
    {
      if (movies == null || movies.Count <= 1) return;

      List<Movie> sortedMovies = Sort(movies, 0, movies.Count - 1);
      movies.Clear();
      movies.AddRange(sortedMovies);
    }

    // recursion
    private static List<Movie> Sort(List<Movie> movies, int left, int right)
    {
      if (left < right)
      {
        int mid = left + (right - left) / 2;

        Sort(movies, left, mid);
        Sort(movies, mid + 1, right);

        Merge(movies, left, mid, right);
      }
      return movies;
    }

    private static void Merge(List<Movie> movies, int left, int mid, int right)
    {
      // subarray sizes
      int n1 = mid - left + 1;
      int n2 = right - mid;

      // temp arrays
      Movie[] leftArray = new Movie[n1];
      Movie[] rightArray = new Movie[n2];

      for (int i = 0; i < n1; ++i)
      {
        leftArray[i] = movies[left + i];
      }
      for (int j = 0; j < n2; ++j)
      {
        rightArray[j] = movies[mid + 1 + j];
      }

      // merge temp arrays into movies
      int k = left;
      int iLeft = 0, iRight = 0;

      while (iLeft < n1 && iRight < n2)
      {
        if (leftArray[iLeft].ReleaseYear <= rightArray[iRight].ReleaseYear)
        {
          movies[k] = leftArray[iLeft];
          iLeft++;
        }
        else
        {
          movies[k] = rightArray[iRight];
          iRight++;
        }
        k++;
      }

      // copy remaining leftArray elements
      while (iLeft < n1)
      {
        movies[k] = leftArray[iLeft];
        iLeft++;
        k++;
      }

      while (iRight < n2)
      {
        movies[k] = rightArray[iRight];
        iRight++;
        k++;
      }
    }
  }
}
