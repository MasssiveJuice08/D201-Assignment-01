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
using System.Windows.Shapes;

namespace D201_Assignment_01
{
  /// <summary>
  /// Interaction logic for AddUserWindow.xaml
  /// </summary>
  public partial class AddUserWindow : Window
  {
    internal User NewUser { get; private set; }

    public AddUserWindow()
    {
      InitializeComponent();
    }

    private void AddUserButton_Click(object sender, RoutedEventArgs e)
    {
      string firstName = txtFirstName.Text.Trim();
      string lastName = txtLastName.Text.Trim();

      if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
      {
        MessageBox.Show("Please enter both first and last names.", 
          "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      NewUser = new User(firstName, lastName);
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
