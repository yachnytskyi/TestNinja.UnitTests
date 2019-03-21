using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.UnitTests.Fundamentals
{
    class Reservation
    {
        public User MadeBy { get; set; }

        public bool CanBeCandelledBy(User user)
        {
            if (user.IsAdmin)
            {
                return true;
            }

            if (MadeBy == user)
            {
                return true;
            }

            return false;
        }

    }

    public class User
    {
        public bool IsAdmin { get; set; }
    }
}
