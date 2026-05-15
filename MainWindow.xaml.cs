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
    private string jsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FilmClub", "movies.json");

    public MainWindow()
    {
      InitializeComponent();

      Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath)); // check dir exists

      movieLibrary = new MovieLinkedList(); // initialise linked list

      MovieFileManager.LoadFromJsonFile(movieLibrary, jsonFilePath); // load from json
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
        MovieFileManager.SaveToJsonFile(movieLibrary, jsonFilePath); // save to JSON
        RefreshListView();
      }
    }

    private void RemoveMovieButton_Click(object sender, RoutedEventArgs e)
    {
      if (listViewMovies.SelectedItem is Movie selectedMovie)
      {
        movieLibrary.Remove(selectedMovie.MovieID);
        MovieFileManager.SaveToJsonFile(movieLibrary, jsonFilePath); // save to JSON
        RefreshListView();
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
        MovieFileManager.LoadFromJsonFile(movieLibrary, openFileDialog.FileName);
        MovieFileManager.SaveToJsonFile(movieLibrary, jsonFilePath); // save after import
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
        MovieFileManager.SaveToJsonFile(movieLibrary, saveFileDialog.FileName);
        RefreshListView();
      }
    }

    private void SortByTitleButton_Click(object sender, RoutedEventArgs e)
    {
      movieLibrary.SortByTitle();
      MovieFileManager.SaveToJsonFile(movieLibrary, jsonFilePath);
      RefreshListView();
    }

    private void SortByYearButton_Click(object sender, RoutedEventArgs e)
    {
      movieLibrary.SortByYear();
      MovieFileManager.SaveToJsonFile(movieLibrary, jsonFilePath);
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
      ManageUsersWindow manageUsersWindow = new ManageUsersWindow();
      manageUsersWindow.ShowDialog();
    }

    private void BorrowMovieButton_Click(object sender, RoutedEventArgs e)
    {
      if (listViewMovies.SelectedItem is Movie selectedMovie)
      {
        // Open a dialog to select a user (or use the first user for simplicity)
        if (userLibrary.Count == 0)
        {
          MessageBox.Show("No users available. Add users first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
          return;
        }

        // For simplicity, borrow the movie for the first user in the list
        User firstUser = userLibrary.ToList()[0];
        bool success = movieLibrary.BorrowMovie(selectedMovie.MovieID, firstUser);

        if (success)
        {
          MessageBox.Show($"Movie '{selectedMovie.Title}' borrowed by {firstUser.FirstName} {firstUser.LastName}.",
                        "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
          MessageBox.Show($"Movie '{selectedMovie.Title}' is not available. " +
                         $"{firstUser.FirstName} {firstUser.LastName} added to the waiting list.",
                        "Not Available", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Save changes and refresh
        MovieFileManager.SaveToJsonFile(movieLibrary, jsonFilePath);
        RefreshListView();
      }
    }

    private void ReturnMovieButton_Click(object sender, RoutedEventArgs e)
    {
      if (listViewMovies.SelectedItem is Movie selectedMovie)
      {
        movieLibrary.ReturnMovie(selectedMovie.MovieID);
        MovieFileManager.SaveToJsonFile(movieLibrary, jsonFilePath);
        RefreshListView();
      }
    }
  }
}
