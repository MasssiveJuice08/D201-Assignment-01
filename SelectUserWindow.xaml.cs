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
  /// Interaction logic for SelectUserWindow.xaml
  /// </summary>
  public partial class SelectUserWindow : Window
  {
    private UserLinkedList userLibrary;
    internal User SelectedUser { get; private set; }
    
    internal SelectUserWindow(UserLinkedList users)
    {
      InitializeComponent();
      userLibrary = users;
      RefreshUserListView();
    }

    private void RefreshUserListView(List<User> users = null)
    {
      if (users == null)
      {
        users = userLibrary.ToList();
      }
      listViewUsers.ItemsSource = users;
    }

    private void SearchUserButton_Click(object sender, RoutedEventArgs e)
    {
      string searchTerm = SearchUserBox.Text.Trim();
      if (string.IsNullOrEmpty(searchTerm)) return;

      // Linear Search by UserID or name
      List<User> results = userLibrary.ToList().Where(u =>
        u.UserID.Equals(searchTerm, StringComparison.OrdinalIgnoreCase) ||
        u.FirstName.Equals(searchTerm, StringComparison.OrdinalIgnoreCase) ||
        u.LastName.Equals(searchTerm, StringComparison.OrdinalIgnoreCase) ||
        $"{u.FirstName} {u.LastName}".Equals(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

      RefreshUserListView(results);
    }

    private void ClearUserSearchButton_Click(object sender, RoutedEventArgs e)
    {
      SearchUserBox.Text = "";
      RefreshUserListView();
    }

    private void SelectUserButton_Click(object sender, RoutedEventArgs e)
    {
      if (listViewUsers.SelectedItem is User selectedUser)
      {
        SelectedUser = selectedUser;
        DialogResult = true;
        Close();
      }
      else
      {
        MessageBox.Show("Please select a user.", "No User Selected",
          MessageBoxButton.OK, MessageBoxImage.Information);
      }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
      Close();
    }
  }
}
