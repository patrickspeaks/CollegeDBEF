using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using static System.Console;


namespace CollegeDBEF
{
    public class Student
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public long SSN { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public long ZipCode { get; set; }
        public long PhoneNumber { get; set; }
        [Key]
        public int StudentID { get; set; }
        public virtual List<Class> ClassID { get; set; }
    }

    public class Class
    {
        public string Title { get; set; }
        public int Number { get; set; }
        public string Department { get; set; }
        public string Instructor { get; set; }
        [Key]
        public int ID { get; set; }
        public virtual List<Score> ScoreID { get; set; }
    }

    public class Score
    {
        [Key]
        public int ID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime DateAssigned { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DateSubmitted { get; set; }
        public int PointsEarned { get; set; }
        public int PointsPossible { get; set; }
        
    }
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Score> Scores { get; set; }

        public StudentContext() : base("CollegeDBEF")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentContext, Migrations.Configuration>());
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            using (var db = new StudentContext())
            {
                String DateAssigned = "2018-02-28";
                DateTime parsedAssigned = DateTime.Parse(DateAssigned);

                String DateDue = "2018-03-25";
                DateTime parsedDue = DateTime.Parse(DateDue);

                String DateSubmitted= "2018-03-10";
                DateTime parsedSubmitted = DateTime.Parse(DateSubmitted);

                
                Student Student1 = new Student
                {
                    FName = "Marc",
                    LName = "Bryant",
                    SSN = 123456789,
                    Address = "123 Main St.",
                    City = "Chicago",
                    State = "IL",
                    ZipCode = 60628,
                    PhoneNumber = 3124567894,
                    ClassID = new List<Class>()
                };
                db.Students.Add(Student1);

                Student Student2 = new Student
                {
                    FName = "Jerry",
                    LName = "Howard",
                    SSN = 012345678,
                    Address = "654 Bell Dr.",
                    City = "Naperville",
                    State = "IL",
                    ZipCode = 60540,
                    PhoneNumber = 7736867854,
                    ClassID = new List<Class>()
                };
                db.Students.Add(Student2);

                Student Student3 = new Student
                {
                    FName = "John",
                    LName = "Ground",
                    SSN = 234567890,
                    Address = "10818 S. King Dr.",
                    City = "Chicago",
                    State = "IL",
                    ZipCode = 60628,
                    PhoneNumber = 7735201456,
                    ClassID = new List<Class>()
                };
                db.Students.Add(Student3);


                var Class1 = new Class
                {
                    Title = "Algebra",
                    Number = 101,
                    Department = "Math",
                    Instructor = "Mr. Stonich",
                    ScoreID = new List<Score>()
                };
                Student2.ClassID.Add(Class1);

                var Class2 = new Class
                {
                    Title = "English", 
                    Number = 205,
                    Department = "English",
                    Instructor = "Mrs. Rogers",
                    ScoreID = new List<Score>()
                };
                Student3.ClassID.Add(Class2);

                var Class3 = new Class
                {
                    Title = "Trigonometry",
                    Number = 201,
                    Department = "Math",
                    Instructor = "Mr. Hampton",
                    ScoreID = new List<Score>()
                };
                Student2.ClassID.Add(Class3);

                var Score1 = new Score
                {
                    Type = "HW",
                    Description = "Chapter5",
                    DateAssigned = parsedAssigned,
                    DateDue = parsedDue,
                    DateSubmitted = parsedSubmitted,
                    PointsEarned = 83,
                    PointsPossible = 100
                };
                Class2.ScoreID.Add(Score1);

                var Score2 = new Score
                {
                    Type = "Quiz",
                    Description = "Chapter3",
                    DateAssigned = parsedAssigned,
                    DateDue = parsedDue,
                    DateSubmitted = parsedSubmitted,
                    PointsEarned = 60,
                    PointsPossible = 100
                };
                Class1.ScoreID.Add(Score2);

                var Score3 = new Score
                {
                    Type = "HW",
                    Description = "Project",
                    DateAssigned = parsedAssigned,
                    DateDue = parsedDue,
                    DateSubmitted = parsedSubmitted,
                    PointsEarned = 92,
                    PointsPossible = 100
                };
                Class3.ScoreID.Add(Score3);

                db.SaveChanges();

                var query = from student in db.Students
                            orderby student.LName, student.FName
                            select student;

                Console.WriteLine("Student/Class Report");
                Console.WriteLine();
                               
                foreach (var student in query)
                {
                    Console.WriteLine($"{student.FName + " " + student.LName} located at {student.Address}");
                    foreach (Class course in student.ClassID)
                    {
                        Console.WriteLine($"- Title: {course.Title}");
                        Console.WriteLine($"- Number: {course.Number}");
                        Console.WriteLine($"- Department: {course.Department}");
                    }

                }
                Console.WriteLine("Press a key to exit...");
                Console.ReadKey();











            }
        }
    }
    
}
