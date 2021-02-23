using System;
using System.Collections.Generic;
using System.Linq;
using CoreSchool.entities;
using CoreSchool.Utilities;

namespace CoreSchool.App
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
        #region GetSchoolObjects with lists
        // This is a non efficient way to avoid the need to write every output parameter when
        // using the method. This way, we can type only the default parameters without any 
        // output parameter, but if we want to type only 1 output parameter, we must create 
        // a different version of this method's overload.

        public IReadOnlyList<BaseSchoolObject> GetBaseSchoolObjects(
            bool getExams = true, 
            bool getStudents = true, 
            bool getSubjects = true, 
            bool getCourses = true)
            {
                return GetBaseSchoolObjects(out int ignore, out ignore, out ignore, out ignore);
            }
        public IReadOnlyList<BaseSchoolObject> GetBaseSchoolObjects(
            out int examCount,
            bool getExams = true, 
            bool getStudents = true, 
            bool getSubjects = true, 
            bool getCourses = true)
            {
                return GetBaseSchoolObjects(out examCount, out int ignore, out ignore, out ignore);
            }
        public IReadOnlyList<BaseSchoolObject> GetBaseSchoolObjects(
            out int examCount,
            out int studentsCount,
            bool getExams = true, 
            bool getStudents = true, 
            bool getSubjects = true, 
            bool getCourses = true)
            {
                return GetBaseSchoolObjects(out examCount, out studentsCount, out int ignore, out ignore);
            }
        public IReadOnlyList<BaseSchoolObject> GetBaseSchoolObjects(
            out int examCount,
            out int studentsCount,
            out int subjectsCount,
            bool getExams = true, 
            bool getStudents = true, 
            bool getSubjects = true, 
            bool getCourses = true)
            {
                return GetBaseSchoolObjects(out examCount, out studentsCount, out subjectsCount, out int ignore);
            }

        // The code above is horrible, it's just for general knowledge.

        // By setting all the parameters in the method's definition, they become optional parameters.
        // Because they have a default value.
        // All default parameters must be at the end of the parameters list

        // To prevent other developers from adding objects as they please to a list, we can define 
        // the list as IReadOnlyList, and we must return the list with .AsReadOnly().
        // When delivering lists for other developers to use, it's better to return a generic list,
        // (IEnumerable, because it can be cast into anything they need) and as read only.
        public IReadOnlyList<BaseSchoolObject> GetBaseSchoolObjects(
            out int examCount, 
            out int studentsCount, 
            out int subjectsCount, 
            out int coursesCount, 
            bool getExams = true, 
            bool getStudents = true, 
            bool getSubjects = true, 
            bool getCourses = true)
        {
            examCount = studentsCount = subjectsCount = coursesCount = 0;
    
            var objectList = new List<BaseSchoolObject>();
            objectList.Add(School);

            if(getCourses)
                objectList.AddRange(School.courses);
                coursesCount += School.courses.Count;

            foreach (Course course in School.courses)
            {
                if(getSubjects)
                    objectList.AddRange(course.subjects);
                    subjectsCount += course.subjects.Count;
                if(getStudents)
                    objectList.AddRange(course.students);
                    studentsCount += course.students.Count;

                if(getExams)
                {
                    foreach (var subject in course.subjects)
                    {
                        objectList.AddRange(subject.exams);
                        examCount += subject.exams.Count;
                    }
                }
            }
            return objectList.AsReadOnly();
        }

        #endregion
        
        #region Dictionary comments
        // To be able to add multiple values to one key, we can set the value type to
        // IEnumerable, so that we can add any kind of generic list to a single key.
        // Since School is not a list, only BaseSchoolObject, we can create a list
        // containing only the school.
        // If by some reason the compiler cannot converts an object to its father's
        // type, knowing that that object inherited from its father, we can force a cast
        // like this -> dictionary.Add("Courses", School.courses.Cast<BaseSchoolObject>());

        // To avoid any typo when adding values to dictionary keys, we can create an enum
        // Containing all the possible key names so that it's less likely to make a typo
        // and we must set our TKey value to the enum type, in this case it's DictionaryKey
        #endregion
        public Dictionary<DictionaryKey, IEnumerable<BaseSchoolObject>> GetObjectDictionary()
        {
            var dictionary = new Dictionary<DictionaryKey, IEnumerable<BaseSchoolObject>>();
            dictionary.Add(DictionaryKey.School, new List<BaseSchoolObject> {School});
            dictionary.Add(DictionaryKey.Course, School.courses);

            var subjectList = new List<Subject>();
            var studentList = new List<Student>();
            var examList = new List<Exam>();

            foreach (var course in School.courses)
            {
                subjectList.AddRange(course.subjects);
                studentList.AddRange(course.students);
                foreach (var student in course.students)
                {
                    examList.AddRange(student.exams);
                }
            }

            dictionary.Add(DictionaryKey.Subject, subjectList);  
            dictionary.Add(DictionaryKey.Student, studentList);  
            dictionary.Add(DictionaryKey.Exam, examList);  
            return dictionary;
        }

        // The dictionary has 5 objects in the type of IEnumerable,
        // To print each Key with its values, we need to print the
        // object's key, then print each value of the IEnumerable which
        // it's the object's value.
        public void PrintDictionary(Dictionary<DictionaryKey, IEnumerable<BaseSchoolObject>> dictionary,
            bool pritnExam = false)
        {
            foreach (var obj in dictionary)
            {
                Printer.PrintTitle(obj.Key.ToString());

                foreach (var value in obj.Value)
                {
                    switch(obj.Key)
                    {
                        case DictionaryKey.Exam:
                            if(pritnExam)
                                Console.WriteLine(value);  
                            break;

                        case DictionaryKey.School:
                            Console.WriteLine("School " + value);
                            break;

                        case DictionaryKey.Student:
                            Console.WriteLine("Student: " + value);
                            break;

                        case DictionaryKey.Course:
                            var tmpCourse = value as Course;
                            if(tmpCourse != null)
                            {
                                int count = tmpCourse.students.Count;
                                Console.WriteLine($"Course {tmpCourse.name} Number of students: {count}");
                            }
                            
                            break;

                        case DictionaryKey.Subject:
                            Console.WriteLine("Subject: " + value);
                            break;
                        
                        default:
                            Console.WriteLine(value);
                            break;
                    }
                    
                }
            }
        }

        #region Data generation methods
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
                                    grade = Math.Round((random.NextDouble()*5), 2)
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
                    new Subject{name = $"Math {course.name}"},
                    new Subject{name = $"Physics {course.name}"},
                    new Subject{name = $"Language {course.name}"},
                    new Subject{name = $"P.E. {course.name}"},
                    new Subject{name = $"Biology {course.name}"}
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

        #endregion

        #region Comments
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
        #endregion