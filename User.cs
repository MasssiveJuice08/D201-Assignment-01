using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D201_Assignment_01
{
  internal class User
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public User(string firstName, string lastName)
    {
      FirstName = firstName;
      LastName = lastName;
    }

    public override string ToString()
    {
      return $"{FirstName} {LastName}";
    }
  }
}
