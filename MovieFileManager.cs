using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace D201_Assignment_01
{
  internal class MovieFileManager
  {
    // save movies to JSON
    public static void SaveToJsonFile(MovieLinkedList movieLibrary, string filePath)
    {
      List<Movie> movies = movieLibrary.ToList();
      string json = JsonSerializer.Serialize(movies, new JsonSerializerOptions { WriteIndented = true });
      File.WriteAllText(filePath, json);
    }

    // load movies from JSON
    public static void LoadFromJsonFile(MovieLinkedList movieLibrary, string filePath)
    {
      if (!File.Exists(filePath)) return;

      string json = File.ReadAllText(filePath);
      List<Movie> movies = JsonSerializer.Deserialize<List<Movie>>(json);

      movieLibrary.Clear(); // clear existing data
      foreach (Movie movie in movies)
      {
        movieLibrary.AddLast(movie);
      }
    }
  }
}
