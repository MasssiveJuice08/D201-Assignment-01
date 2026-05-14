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
    private string jsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FilmClub", "movies.json");
    
    public MainWindow()
    {
      InitializeComponent();

      Directory.CreateDirectory(Path.GetDirectoryName(jsonFilePath)); // check dir exists

      movieLibrary = new MovieLinkedList(); // initialise linked list

      MovieFileManager.LoadFromJsonFile(movieLibrary, jsonFilePath); // load from json
      RefreshListView(); // bind to ListView

      /* == placeholder movies == */
      //movieLibrary.AddLast(new Movie("M001", "Inception", "Chirstopher Nolan", "Sci-Fi", 2010, true));
      //movieLibrary.AddLast(new Movie("M002", "The Shawshank Redemption", "Frank Darabont", "Drama", 1994, true));
      //movieLibrary.AddLast(new Movie("M003", "Pulp Fiction", "Quentin Tarantino", "Crime", 1994, true));
    }

    private void RefreshListView()
    {
      listViewMovies.ItemsSource = null;
      listViewMovies.ItemsSource = movieLibrary.ToList();
    }

    /* == placeholder data add/remove tests == */
    // remove tests below after load/save functionality implemented
    // placeholder test - add movie
    private void AddMovieButton_Click(object sender, RoutedEventArgs e)
    {
      Movie newMovie = new Movie(
        "M004", 
        "The Dark Knight", 
        "Christopher Nolan", 
        "Action", 
        2008, 
        true);
      movieLibrary.AddLast(newMovie);
      MovieFileManager.SaveToJsonFile(movieLibrary, jsonFilePath);
      RefreshListView();
    }

    // placeholder test - remove movie
    private void RemoveMovieButton_Click(object sender, RoutedEventArgs e)
    {
      movieLibrary.Remove("M001");
      RefreshListView();
    }
  }
}
