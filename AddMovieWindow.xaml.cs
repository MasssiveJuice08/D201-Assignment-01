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
    
    public AddMovieWindow()
    {
      InitializeComponent();
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

      // create new movie
      NewMovie = new Movie
      {
        MovieID = txtMovieID.Text,
        Title = txtTitle.Text,
        Director = txtDirector.Text,
        Genre = txtGenre.Text,
        ReleaseYear = releaseYear,
        Available = chkAvailable.IsChecked ?? false
      };

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
