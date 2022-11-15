using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDiary.Models.Domains
{
    public class Student
    {
        public Student()
        {
            Ratings = new Collection<Rating>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }
        public bool Activities { get; set; }
        public int GroupId { get; set; }

        //zbudowanie relacji w kodzie , student moze byc w jednej grupie 
        public Group Group { get; set; }

        public ICollection<Rating> Ratings { get; set; }

    }
}
