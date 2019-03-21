using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.UnitTests.Models
{
    public interface IRepository
    {
        IEnumerable<Phone> GetAll();
        Phone Get(int id);
        void Create(Phone p);
    }
}