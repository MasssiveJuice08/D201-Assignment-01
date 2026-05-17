using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace D201_Assignment_01
{
  internal class UserLinkedList
  {
    private UserNode head;
    private UserNode tail;
    public int Count { get; private set; }

    public void AddLast(User user)
    {
      // check for duplicate userID
      if (HasDuplicateUserID(user.UserID))
      {
        MessageBox.Show(
          $"A user with the ID '{user.UserID}' already exists.\n\n" +
          "Please enter a unique UserID.",
          "Duplicate UserID",
          MessageBoxButton.OK,
          MessageBoxImage.Error);
        return; // Reject the addition
      }
      
      // if new userID unique, check for duplicate names
      if (HasDuplicateName(user.FirstName, user.LastName))
      {
        MessageBoxResult result = MessageBox.Show(
          $"A user with the name {user.FirstName} {user.LastName} already exists.\n\n" +
          $"Would you like to add this user anyway?",
          "Duplicate Name",
          MessageBoxButton.YesNo,
          MessageBoxImage.Question);

        if (result == MessageBoxResult.No)
        {
          return; // user not added
        }
      }
      
      UserNode newNode = new UserNode(user);
      if (head == null)
      {
        head = newNode;
        tail = newNode;
      }
      else
      {
        tail.Next = newNode;
        tail = newNode;
      }
      Count++;
    }

    public bool HasDuplicateUserID(string userID)
    {
      UserNode current = head;
      while (current != null)
      {
        if (current.Data.UserID.Equals(userID, StringComparison.OrdinalIgnoreCase))
        {
          return true;
        }
        current = current.Next;
      }
      return false;
    }

    public bool HasDuplicateName(string firstName, string lastName)
    {
      UserNode current = head;
      while (current != null)
      {
        if (current.Data.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
          current.Data.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
        {
          return true;
        }
        current = current.Next;
      }
      return false;
    }

    public bool Remove(User user)
    {
      if (head == null) return false;

      if (head.Data == user)
      {
          head = head.Next;
          if (head == null)
          {
              tail = null;
          }
          Count--;
          return true;
      }

      UserNode current = head;
      while (current.Next != null)
      {
          if (current.Next.Data == user)
          {
              current.Next = current.Next.Next;
              if (current.Next == null)
              {
                  tail = current;
              }
              Count--;
              return true;
          }
          current = current.Next;
      }
      return false;
    }

    public List<User> ToList()
    {
      List<User> users = new List<User>();
      UserNode current = head;
      while (current != null)
      {
        users.Add(current.Data);
        current = current.Next;
      }
      return users;
    }

    public void Clear()
    {
      head = null;
      tail = null;
      Count = 0;
    }

    // method to skip duplicate name check for loading user.json on startup
    public void BulkAdd(User user)
    {
      UserNode newNode = new UserNode(user);
      if (head == null)
      {
        head = newNode;
        tail = newNode;
      }
      else
      {
        tail.Next = newNode;
        tail = newNode;
      }
      Count++;
    }
  }
}
