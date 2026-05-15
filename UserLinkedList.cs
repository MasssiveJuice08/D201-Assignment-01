using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D201_Assignment_01
{
  internal class UserLinkedList
  {
    private UserNode head;
    private UserNode tail;
    public int Count { get; private set; }

    public void AddLast(User user)
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
  }
}
