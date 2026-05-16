using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D201_Assignment_01
{
  internal class UserNode
  {
    public User Data { get; set; }
    public UserNode Next { get; set; }

    public UserNode(User user)
    {
      Data = user;
      Next = null;
    }
  }
}
