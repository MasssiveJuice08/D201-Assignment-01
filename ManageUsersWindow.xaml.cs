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

namespace D201_Assignment_01
{
  /// <summary>
  /// Interaction logic for ManageUsersWindow.xaml
  /// </summary>
  public partial class ManageUsersWindow : Window
  {
    private UserLinkedList userLibrary;
    private string userJsonFilePath;

    internal ManageUsersWindow(UserLinkedList users, string userJsonFilePath) // changed constructor access modifier to internal to prevent CS0051 error
    {
      InitializeComponent();
      userLibrary = users;
      this.userJsonFilePath = userJsonFilePath;
      RefreshUserListView();
    }

    private void RefreshUserListView()
    {
      listViewUsers.ItemsSource = null;
      listViewUsers.ItemsSource = userLibrary.ToList();
    }

    private void AddUserButton_Click(object sender, RoutedEventArgs e)
    {
      // dialog to input user details
      AddUserWindow addUserWindow = new AddUserWindow();
      if (addUserWindow.ShowDialog() == true)
      {
        userLibrary.AddLast(addUserWindow.NewUser);
        MovieFileManager.SaveUsersToJsonFile(userLibrary, userJsonFilePath);
        RefreshUserListView();
      }
    }
    private void RemoveUserButton_Click(object sender, RoutedEventArgs e)
    {
      if (listViewUsers.SelectedItem is User selectedUser)
      {
        MessageBoxResult result = MessageBox.Show(
          $"Are you sure you want to remove {selectedUser.FirstName} {selectedUser.LastName}?",
          "Confirm Removal",
          MessageBoxButton.YesNo,
          MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
          userLibrary.Remove(selectedUser);
          MovieFileManager.SaveUsersToJsonFile(userLibrary, userJsonFilePath);
          RefreshUserListView();
        }
      }
      else
      {
        MessageBox.Show("Please select a user to remove.", 
          "No user selected", MessageBoxButton.OK, MessageBoxImage.Information);
      }
    }
  }
}
