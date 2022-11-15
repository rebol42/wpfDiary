using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDiary.Models.Domains;
using WpfDiary.Models.Wrappers;
using System.Data.Entity;
using WpfDiary.Models.Converters;
using WpfDiary.Models;

namespace WpfDiary
{
    public class Repository
    {
        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Groups.ToList();
            }

            

        }

        public List<StudentWrapper> GetStudents(int groupId)
        {
            using (var context = new ApplicationDbContext())
            {

                var students = context.
                               Students
                               .Include(x => x.Group)
                               .Include(x => x.Ratings).ToList()
                               .AsQueryable();


                if (groupId != 0)
                    students = students.Where(x => x.GroupId == groupId);


             //   var student = students.First().ToWrapper();

                return students
                        .ToList()
                        .Select(x => x.ToWrapper())
                        .ToList();

            }


        }

        public void DeleteStudent(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var studentToDelete = context.Students.Find(id);
                context.Students.Remove(studentToDelete);
                context.SaveChanges();
            }
        }

        public void UpdateStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.toDao();
            // pobranie ocen z obiektu , nie z bazy 
            var ratigns = studentWrapper.toRatingDao();

            using (var context = new ApplicationDbContext())
            {
                // metoda do aktualizacji danych studenta 
                UpdateStudentProperties(student, context);

                // pobranie ocen z bazy danych 
                var studentsRatings = GetStudentsRatingns(student, context);


                UpdateRate(student, ratigns, context, studentsRatings, Subject.Math);
                UpdateRate(student, ratigns, context, studentsRatings, Subject.Physics);
                UpdateRate(student, ratigns, context, studentsRatings, Subject.Technology);
                UpdateRate(student, ratigns, context, studentsRatings, Subject.ForeignLang);
                UpdateRate(student, ratigns, context, studentsRatings, Subject.PolishLang);

                context.SaveChanges();

            }
        }

        private static List<Rating> GetStudentsRatingns(Student student, ApplicationDbContext context)
        {
            return context.
                     Ratings.
                     Where(x => x.StudentId == student.Id).ToList();

        }




        private void UpdateStudentProperties(Student student, ApplicationDbContext context)
        {
            var stundentToUpate = context.Students.Find(student.Id);

            stundentToUpate.Activities = student.Activities;
            stundentToUpate.Comments = student.Comments;
            stundentToUpate.FirstName = student.FirstName;
            stundentToUpate.LastName = student.LastName;
            stundentToUpate.GroupId = student.GroupId;
        }

        private static void UpdateRate(Student student, List<Rating> newRatings,
            ApplicationDbContext context, List<Rating> studentRatings, Subject subject)
        {
            // pobranie ocen z bazy 
            var subRatings = studentRatings.
                   Where(x => x.SubjectId == (int)subject)
                   .Select(x => x.Rate);

            // pobranie nowych ocen z obiektu do aktualizacji
            var newSubRatings = newRatings.
              Where(x => x.SubjectId == (int)subject)
              .Select(x => x.Rate);

            // pobranie ocen z bazy za wyjatkiem tych nowy ocen w subRateToAdd
            var subRatingsToDelete = subRatings.Except(newSubRatings).ToList();
            // tu sprawdzamy ktore sa do dodania
            var subRatingsToAdd = newSubRatings.Except(newSubRatings).ToList();

            // usuwanie tych ocen ktore mamy wybrane w subRatingsToDelete
            // robimy to w petli forEach
            subRatingsToDelete.ForEach(x =>
            {
                var ratingtoDelete = context.Ratings.First(y =>
                    y.Rate == x &&
                    y.StudentId == student.Id &&
                    y.SubjectId == (int)subject);

                context.Ratings.Remove(ratingtoDelete);
            });

            subRatingsToAdd.ForEach(x =>
            {
                var ratingToAdd = new Rating
                {
                    Rate = x,
                    StudentId = student.Id,
                    SubjectId = (int)subject
                };
                context.Ratings.Add(ratingToAdd);
            });
        }

        public void AddStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.toDao();
            var ratigns = studentWrapper.toRatingDao();

            using (var context = new ApplicationDbContext())
            {
                var dbStduent = context.Students.Add(student);

                ratigns.ForEach(x =>
                {
                    x.StudentId = dbStduent.Id;
                    context.Ratings.Add(x);
                });

                context.SaveChanges();

            }
        }
    }
}
