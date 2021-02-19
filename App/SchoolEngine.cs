using System;
using System.Collections.Generic;
using System.Linq;
using CoreSchool.entities;

namespace CoreSchool
{
    // Sealed doesn't let any class to inherit from SchoolEngine, only allows to create objects.
    public sealed class SchoolEngine
    {
        public School School { get; set; }

        public SchoolEngine()
        {
            
        }

        public void Initialize()
        {
            // By setting optional parameters in the ctor, we can skip setting them, or just set one of them, 
            // or set them in a different order, in this case, country was supposed to be set before city.
            School = new School("Platzi Academy", 2012, SchoolType.HighSchool,
            city:"Bogota", country:"Colombia");

            LoadCourses();
            LoadSubjects();
            
            LoadExams();

            
        }

        public List<BaseSchoolObject> GetBaseSchoolObjects()
        {
            var objectList = new List<BaseSchoolObject>();
            objectList.Add(School);
            objectList.AddRange(School.courses);
            foreach (Course course in School.courses)
            {
                objectList.AddRange(course.subjects);
                objectList.AddRange(course.students);
                foreach (var subject in course.subjects)
                {
                    objectList.AddRange(subject.exams);
                }
            }
            return objectList;
        }

        private void LoadExams()
        {
            
            foreach (var course in School.courses)
            {
                foreach (var subject in course.subjects)
                {
                    foreach (var student in course.students)
                    {
                        Random random = new Random();
                        for(int i = 0; i < 5; i++)
                        {
                            var exam = new Exam(){
                                    examSubject = subject,
                                    name = $"{subject.name} Quiz {i+1}",
                                    testedStudent = student,
                                    grade = (double)(random.NextDouble()*5)
                                };
                            // Add each exam to the student
                            student.exams.Add(exam);

                            // Add each exam to the subject to collect all the subject's exams
                            subject.exams.Add(exam);
                        }
                    }
                }
            }
        }

        private void LoadSubjects()
        {
            foreach (var course in School.courses)
            {
                var subjectList = new List<Subject>(){
                    new Subject{name = "Math"},
                    new Subject{name = "Physics"},
                    new Subject{name = "Language"},
                    new Subject{name = "P.E."},
                    new Subject{name = "Biology"}
                };
                course.subjects = subjectList;
            }
        }

        private List<Student> createStudents(int numberOfStudents)
        {
            string[] firstName = {"Fredy", "Anne", "Israel", "Fernando", "Igor", "Charly", "Travis"};
            string[] middleName = {"Max", "Lauren", "Dixie", "Daniel", "Louis", "Harry"};
            string[] lastName = {"Wick", "Hernandez", "Bezos", "Musk", "Evans", "Parker"};

            // Create query
            // fN will be the tmp name for all firstName's
            // For every firstName, select will create a new Student and will store it into studentList
            var studentList = from fN in firstName
                              from mN in middleName
                              from lN in lastName
                              select new Student{name = $"{fN} {mN} {lN}"};

            // Sorts students by ID then takes a the desired number of students and converts them to a list
            return studentList.OrderBy((student) => student.uniqueID).Take(numberOfStudents).ToList();
        }

        private void LoadCourses()
        {
            // Course ctor does not ask for parameters to create an object, but we can give the parameters in between {}.
            // This list only uses Course-type objects
            School.courses = new List<Course>()
            {
                new Course() {name = "101", workShift = WorkShift.Morning},
                new Course() {name = "201", workShift = WorkShift.Morning},
                new Course() {name = "301", workShift = WorkShift.Morning},
                new Course() {name = "401", workShift = WorkShift.Evening},
                new Course() {name = "501", workShift = WorkShift.Evening},
            };

            // This generates a random number between 5 and 20
            Random random = new Random();

            foreach (var course in School.courses)
            {
                int randomNumber = random.Next(5,20);
                course.students = createStudents(randomNumber);
            }

            
        }
    }
}

/*
            =============== Ways to add more items to a generic list ========================
            =================================================================================

            School.courses.Add(new Course(){name = "102", workShift = WorkShift.Evening});

            var anotherCourseList = new List<Course>()
            {
                new Course() {name = "401", workShift = WorkShift.Morning},
                new Course() {name = "501", workShift = WorkShift.Morning},
                new Course() {name = "502", workShift = WorkShift.Evening}
            };

            // Adding a list to another list
            School.courses.AddRange(anotherCourseList);
            */

            /*
            =============== Ways to remove items from a generic list ========================
            =================================================================================
            
            Course tmp = new Course(){name = "101-Vacacional", workShift = WorkShift.Night};
            School.courses.Add(tmp);

            // The framework removes the object whose HashCode matches the HashCode of the parameter inside Remove().
            School.courses.Remove(tmp);

            // This structure can be used when there are more parameters to compare
            mySchool.courses.RemoveAll(delegate(Course courseToRemove)
            {return courseToRemove.name == "501";});

            // Shorter version using lambda expressions            
            mySchool.courses.RemoveAll((courseToRemove) => courseToRemove.name == "501");
            
            */