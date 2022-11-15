using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDiary.Models.Domains
{
    public class Rating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }


        //ocena jest przypisana do konkretnego studenta 
        public Student Student { get; set; }
    }
}
