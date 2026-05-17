using System.Linq;

namespace D201_Assignment_01
{
  internal class MovieService : IMovieService
  {
    private readonly IMovieRepository repository;

    public MovieService(IMovieRepository repository)
    {
      this.repository = repository;
    }

    public BorrowResult BorrowMovie(string movieID, User user)
    {
      Movie movie = repository.Find(movieID);
      if (movie == null) return BorrowResult.MovieNotFound;

      // check if user is already borrowing this movie
      if (movie.BorrowedBy != null && movie.BorrowedBy.UserID == user.UserID)
      {
        return BorrowResult.AlreadyBorrowing;
      }

      if (movie.Available)
      {
        movie.Available = false; // mark movie as borrowed
        movie.BorrowedBy = user; // assign user as borrower
        return BorrowResult.Success;
      }
      else
      {
        // check if user already in WaitingList
        if (movie.WaitingList.Contains(user))
        {
          return BorrowResult.AlreadyInWaitingList;
        }

        // add to waiting list
        movie.WaitingList.Enqueue(user);
        return BorrowResult.AddedToWaitingList;
      }
    }

    public void ReturnMovie(string movieID)
    {
      Movie movie = repository.Find(movieID);
      if (movie == null) return;

      movie.Available = true;
      movie.BorrowedBy = null; // unassign borrower

      // if users are in WaitingList, assign movie to next user in queue
      if (movie.WaitingList.Count > 0)
      {
        User nextUser = movie.WaitingList.Dequeue();
        movie.Available = false; // mark as borrowed
        movie.BorrowedBy = nextUser; // movie now borrowed by nextUser
        // UI should be responsible for any notifications
      }
    }
  }
}
