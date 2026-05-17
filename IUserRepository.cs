using System.Collections.Generic;

namespace D201_Assignment_01
{
  internal interface IUserRepository
  {
    int Count { get; }
    bool AddLast(User user);
    bool Remove(User user);
    List<User> ToList();
    void Clear();
    bool HasDuplicateUserID(string userID);
    bool HasDuplicateName(string firstName, string lastName);
    void BulkAdd(User user);
  }
}
