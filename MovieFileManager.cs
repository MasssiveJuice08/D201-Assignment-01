using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace D201_Assignment_01
{
  internal static class MovieFileManager
  {

    // save movies to JSON
    public static void SaveMoviesToJsonFile(MovieLinkedList movieLibrary, string filePath)
    {
      List<Movie> movies = movieLibrary.ToList();
      string json = JsonSerializer.Serialize(movies, new JsonSerializerOptions { WriteIndented = true });
      File.WriteAllText(filePath, json);
    }

    // load movies from JSON
    public static bool LoadMoviesFromJsonFile(MovieLinkedList movieLibrary, string filePath, bool checkForDuplicates = true)
    {
      if (!File.Exists(filePath)) return false;

      string json = File.ReadAllText(filePath);
      List<Movie> movies = JsonSerializer.Deserialize<List<Movie>>(json);

      if (checkForDuplicates && movieLibrary.HasDuplicateMovieID(movies))
      {
        return false; // duplicates found
      }

      movieLibrary.Clear(); // clear existing data
      foreach (Movie movie in movies)
      {
        movieLibrary.BulkAdd(movie); // skips duplicate checks
      }
      return true;
    }

    // save users to JSON
    public static void SaveUsersToJsonFile(UserLinkedList userLibrary, string filePath)
    {
      List<User> users = userLibrary.ToList();
      string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
      File.WriteAllText(filePath, json);
    }

    // load users from JSON
    public static void LoadUsersFromJsonFile(UserLinkedList userLibrary, string filePath)
    {
      if (!File.Exists(filePath)) return;

      string json = File.ReadAllText(filePath);
      List<User> users = JsonSerializer.Deserialize<List<User>>(json);

      userLibrary.Clear();
      foreach (User user in users)
      {
        userLibrary.BulkAdd(user);
      }
    }
  }
}
