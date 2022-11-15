using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDiary.Models.Domains
{
    public class Group
    {
        public Group()
        {
            Students = new Collection<Student>();
        }


        public int Id { get; set; }
        public string Name { get; set; }

        // jest to zbudowanie relacji w kodzie , w jednej grupie moze byc wielu studentow
        public ICollection<Student> Students { get; set; }
    }
}
