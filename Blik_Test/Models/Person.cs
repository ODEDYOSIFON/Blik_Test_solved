using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blik_Test.Models
{
    public record Person(int Id, string Name, Enums.Gender Gender, List<Hobby> Hobbies)
    {
        public override string ToString()
        {
            return $"{Id}, {Name}, {Gender}, [{string.Join(',', Hobbies)}]";
        }
    }
}
