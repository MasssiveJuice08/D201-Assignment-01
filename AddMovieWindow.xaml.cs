using System;
using System.Collections.Generic;
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

namespace D201_Assignment_01
{
  /// <summary>
  /// Interaction logic for AddMovieWindow.xaml
  /// </summary>
  public partial class AddMovieWindow : Window
  {
    internal Movie NewMovie { get; private set; }
    private bool isModifyMode = false; // add vs modify movie
    private Movie movieToModify;
    
    // constructor for adding new movie
    internal AddMovieWindow()
    {
      InitializeComponent();
      isModifyMode = false;
      this.Title = "Add Movie";
    }

    //constructor for modifying existing movie
    internal AddMovieWindow(Movie movie)
    {
      InitializeComponent();
      isModifyMode = true;
      movieToModify = movie;
      this.Title = "Modify Movie";
      this.addMovieBtn.Content = "Save Movie"; // differentiate button when modifying movie

      txtMovieID.Text = movie.MovieID;
      txtTitle.Text = movie.Title;
      txtDirector.Text = movie.Director;
      txtGenre.Text = movie.Genre;
      txtReleaseYear.Text = movie.ReleaseYear.ToString();
      txtMovieID.IsEnabled = false; // prevent changing MovieID
    }

    private void AddMovieButton_Click(object sender, RoutedEventArgs e)
    {
      // validate input
      if (string.IsNullOrWhiteSpace(txtMovieID.Text) ||
          string.IsNullOrWhiteSpace(txtTitle.Text) ||
          string.IsNullOrWhiteSpace(txtDirector.Text) ||
          string.IsNullOrWhiteSpace(txtGenre.Text) ||
          !int.TryParse(txtReleaseYear.Text, out int releaseYear))
      {
        MessageBox.Show("Please fill in all fields with valid values.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      if (isModifyMode)
      {
        // update existing movie
        movieToModify.Title = txtTitle.Text.Trim();
        movieToModify.Director = txtDirector.Text.Trim();
        movieToModify.Genre = txtGenre.Text.Trim();
        movieToModify.ReleaseYear = releaseYear;

        NewMovie = movieToModify;
      }

      else
      {
        // create new movie
        NewMovie = new Movie
        {
          MovieID = txtMovieID.Text.Trim(),
          Title = txtTitle.Text.Trim(),
          Director = txtDirector.Text.Trim(),
          Genre = txtGenre.Text.Trim(),
          ReleaseYear = releaseYear,
          Available = true
        };
      }
        
      DialogResult = true;
      Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
      Close();
    }
  }
}
