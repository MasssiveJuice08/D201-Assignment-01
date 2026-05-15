using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D201_Assignment_01
{
  internal class Movie
  {
    public string MovieID { get; set; } // Primary key
    public string Title { get; set; }
    public string Director { get; set; }
    public string Genre { get; set; }
    public int ReleaseYear { get; set; }
    public bool Available { get; set; }
    public Queue<User> WaitingList { get; set; }

    public Movie(string movieId, string title, string director, string genre, int releaseYear, bool available)
    {
      MovieID = movieId;
      Title = title;
      Director = director;
      Genre = genre;
      ReleaseYear = releaseYear;
      Available = available;
      WaitingList = new Queue<User>(); // initialise queue
    }

    // default constructor for deserialisation from JSON
    public Movie() { } 

    public override string ToString()
    {
      return $"{Title} ({ReleaseYear}), Director: {Director}, Genre: {Genre}, ID: {MovieID}, Available: {Available}";
    }
  }
}
