using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D201_Assignment_01
{
  internal class MovieNode
  {
    public Movie Data { get; set; }
    public MovieNode Next { get; set; }

    public MovieNode(Movie movie)
    {
      Data = movie;
      Next = null;
    }
  }
}
