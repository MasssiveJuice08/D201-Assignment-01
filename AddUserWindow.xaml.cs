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
    private bool isModifyMode = false;
    private User userToModify;

    // constructor for adding new user
    internal AddUserWindow()
    {
      InitializeComponent();
      isModifyMode = false;
      this.Title = "Add User";
    }

    // constructor for modifying existing user
    internal AddUserWindow(User user)
    {
      InitializeComponent();
      isModifyMode = true;
      userToModify = user;
      this.Title = "Modify User";
      this.addUserBtn.Content = "Save User";

      txtUserID.Text = user.UserID;
      txtFirstName.Text = user.FirstName;
      txtLastName.Text = user.LastName;
      txtUserID.IsEnabled = false; // prevent changing userID
    }

    private void AddUserButton_Click(object sender, RoutedEventArgs e)
    {
      string userID = txtUserID.Text.Trim();
      string firstName = txtFirstName.Text.Trim();
      string lastName = txtLastName.Text.Trim();

      if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
      {
        MessageBox.Show("Please enter all fields.", 
          "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      if (isModifyMode)
      {
        // update existing user
        userToModify.FirstName = firstName;
        userToModify.LastName = lastName;
        NewUser = userToModify;
      }
      else
      {
        // create new user
        NewUser = new User(userID, firstName, lastName);
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
