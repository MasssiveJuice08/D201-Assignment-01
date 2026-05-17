namespace D201_Assignment_01
{
  internal interface IMovieService
  {
    BorrowResult BorrowMovie(string movieID, User user);
    void ReturnMovie(string movieID);
  }
}
