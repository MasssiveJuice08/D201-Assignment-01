using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D201_Assignment_01
{
  internal static class BubbleSort
  {
    public static void SortByTitle(List<Movie> movies)
    {
      if (movies == null || movies.Count <= 1) return;

      for (int i = 0; i < movies.Count - 1; i++)
      {
        for (int j = 0; j < movies.Count - i - 1; j++)
        {
          if (string.Compare(movies[j].Title, movies[j + 1].Title, StringComparison.OrdinalIgnoreCase) > 0)
          {
            Movie temp = movies[j];
            movies[j] = movies[j + 1];
            movies[j + 1] = temp;
          }
        }
      }
    }
  }
}
