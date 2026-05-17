using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
// using System.Windows.Shapes; // removed due to ambiguity with System.IO.Path

namespace D201_Assignment_01
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private MovieLinkedList movieLibrary;
    private UserLinkedList userLibrary;
    private string movieJsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FilmClub", "movies.json");
    private string userJsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FilmClub", "users.json");

    public MainWindow()
    {
      InitializeComponent();

      // check dir exists
      Directory.CreateDirectory(Path.GetDirectoryName(movieJsonFilePath)); 
      Directory.CreateDirectory(Path.GetDirectoryName(userJsonFilePath));

      // initialise movie & user data linked lists
      movieLibrary = new MovieLinkedList();
      userLibrary = new UserLinkedList();

      // load data from json
      MovieFileManager.LoadMoviesFromJsonFile(movieLibrary, movieJsonFilePath);
      MovieFileManager.LoadUsersFromJsonFile(userLibrary, userJsonFilePath);
      RefreshListView(); // bind to ListView
    }

    private void RefreshListView()
    {
      listViewMovies.ItemsSource = null;
      listViewMovies.ItemsSource = movieLibrary.ToList();
    }

    private void AddMovieButton_Click(object sender, RoutedEventArgs e)
    {
      AddMovieWindow addMovieWindow = new AddMovieWindow();
      // only save if user clicks 'add movie' in AddMovieWindow
      if (addMovieWindow.ShowDialog() == true)
      {
        movieLibrary.AddLast(addMovieWindow.NewMovie);
        MovieFileManager.SaveMoviesToJsonFile(movieLibrary, movieJsonFilePath); // save to JSON
        RefreshListView();
      }
    }

    private void RemoveMovieButton_Click(object sender, RoutedEventArgs e)
    {
      if (listViewMovies.SelectedItem == null)
      {
        MessageBox.Show("No movie is selected.", "No Movie Selected", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
      }

      if (listViewMovies.SelectedItem is Movie selectedMovie)
      {
        // confirm deletion with warning about associated data loss
        MessageBoxResult result = MessageBox.Show(
          $"Are you sure you want to remove '{selectedMovie.Title}'?\n\n" +
          "This will also remove borrower history associated with the movie.\n\n" +
          "This action cannot be undone.",
          "Confirm Removal",
          MessageBoxButton.YesNo,
          MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
          movieLibrary.Remove(selectedMovie.MovieID);
          MovieFileManager.SaveMoviesToJsonFile(movieLibrary, movieJsonFilePath); // save to JSON
          RefreshListView();
        }
      }
    }

    private void ImportButton_Click(object sender, RoutedEventArgs e)
    {
      Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
      {
        Filter = "JSON Files (*.json|*.json"
      };
      if (openFileDialog.ShowDialog() == true)
      {
        // check for duplicates before accepting import
        bool success = MovieFileManager.LoadMoviesFromJsonFile(movieLibrary, openFileDialog.FileName, checkForDuplicates: true);
        
        if (!success)
        {
          MessageBox.Show(
            "One or more movies in the imported file have duplicate MovieIDs.\n\n" +
            "Please ensure all MovieIDs are unique before importing.",
            "Duplicate MovieIDs",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
          return;
        }

        MovieFileManager.SaveMoviesToJsonFile(movieLibrary, movieJsonFilePath); // save after import
        RefreshListView();
      }
    }

    private void ExportButton_Click(object sender, RoutedEventArgs e)
    {
      Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
      {
        Filter = "JSON Files (*.json|*.json",
        DefaultExt = "json"
      };
      if (saveFileDialog.ShowDialog() == true)
      {
        MovieFileManager.SaveMoviesToJsonFile(movieLibrary, saveFileDialog.FileName);
        RefreshListView();
      }
    }

    private void SortByTitleButton_Click(object sender, RoutedEventArgs e)
    {
      movieLibrary.SortByTitle();
      MovieFileManager.SaveMoviesToJsonFile(movieLibrary, movieJsonFilePath);
      RefreshListView();
    }

    private void SortByYearButton_Click(object sender, RoutedEventArgs e)
    {
      movieLibrary.SortByYear();
      MovieFileManager.SaveMoviesToJsonFile(movieLibrary, movieJsonFilePath);
      RefreshListView();
    }

    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
      string searchTerm = SearchBox.Text.Trim();
      if (string.IsNullOrEmpty(searchTerm)) return;

      // ComboBox search type
      string selectedSearchType = (SearchTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

      List<Movie> searchResults = new List<Movie>();

      if (selectedSearchType == "Search by Title")
      {
        searchResults = movieLibrary.SearchByTitle(searchTerm);
      }
      else if (selectedSearchType == "Search by MovieID")
      {
        Movie foundMovie = movieLibrary.Find(searchTerm);
        if (foundMovie != null)
        {
          searchResults.Add(foundMovie);
        }
      }
      // display results
      listViewMovies.ItemsSource = searchResults;
    }

    private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
    {
      SearchBox.Text = "";
      RefreshListView();
    }

    private void ManageUsersBtn_Click(object sender, RoutedEventArgs e)
    {
      ManageUsersWindow manageUsersWindow = new ManageUsersWindow(userLibrary, userJsonFilePath);
      manageUsersWindow.ShowDialog();
    }

    private void BorrowMovieButton_Click(object sender, RoutedEventArgs e)
    {
      if (listViewMovies.SelectedItem is Movie selectedMovie)
      {
        // Open a dialog to select a user
        if (userLibrary.Count == 0)
        {
          MessageBox.Show("No users available. Add users first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }

        // open user selection window
        SelectUserWindow selectUserWindow = new SelectUserWindow(userLibrary);
        if (selectUserWindow.ShowDialog() == true)
        {
          User selectedUser = selectUserWindow.SelectedUser;
          BorrowResult result = movieLibrary.BorrowMovie(selectedMovie.MovieID, selectedUser);

          switch (result)
          {
            case BorrowResult.Success:
              MessageBox.Show(
                $"Movie '{selectedMovie.Title}' borrowed by {selectedUser.FirstName} {selectedUser.LastName}.",
                "Success", MessageBoxButton.OK, MessageBoxImage.Information);
              break;

            case BorrowResult.AlreadyBorrowing:
              // No additional message needed (already shown in BorrowMovie)
              break;

            case BorrowResult.AddedToWaitingList:
              MessageBox.Show(
                $"Movie '{selectedMovie.Title}' is not available. " +
                $"{selectedUser.FirstName} {selectedUser.LastName} added to the waiting list.",
                "Not Available", MessageBoxButton.OK, MessageBoxImage.Information);
              break;

            case BorrowResult.AlreadyInWaitingList:
              // No additional message needed (already shown in BorrowMovie)
              break;

            case BorrowResult.MovieNotFound:
              MessageBox.Show(
                $"Movie not found.",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
              break;
          }

          MovieFileManager.SaveMoviesToJsonFile(movieLibrary, movieJsonFilePath); // save to JSON
          RefreshListView();
        }
      }
    }

    private void ReturnMovieButton_Click(object sender, RoutedEventArgs e)
    {
      if (listViewMovies.SelectedItem is Movie selectedMovie)
      {
        movieLibrary.ReturnMovie(selectedMovie.MovieID);
        MovieFileManager.SaveMoviesToJsonFile(movieLibrary, movieJsonFilePath);
        RefreshListView();
      }
    }
  }
}
