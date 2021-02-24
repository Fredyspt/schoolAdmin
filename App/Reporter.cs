using System;
using System.Linq;
using System.Collections.Generic;
using CoreSchool.entities;

namespace CoreSchool.App
{
    public class Reporter
    {
        // Good practice: Name private members with an underscore at the beginning.
        Dictionary<DictionaryKey, IEnumerable<BaseSchoolObject>> _objectsDictionary;
        public Reporter(Dictionary<DictionaryKey, IEnumerable<BaseSchoolObject>> objectsDictionary)
        {
            if(objectsDictionary == null)
                throw new ArgumentNullException(nameof(objectsDictionary));
            _objectsDictionary = objectsDictionary;
        }

        // TryGetValue gets the value associated with the correspondent key, it returns true if it could
        // get the value, or false if couldn't. If true, it has an output parameter to return the value, 
        // in this case, it's an IEnumerable<BaseSchoolObject> containing all the schools available.
        public IEnumerable<Exam> GetExamList()
        {
            if(_objectsDictionary.TryGetValue(DictionaryKey.Exam, out IEnumerable<BaseSchoolObject> list))
            {
                return list.Cast<Exam>();
            } else
            {
                return new List<Exam>();
            }
        }

        // Collects every Subject name and the .Distinct() function prevents the query from collecting
        // the same object twice.
        public IEnumerable<string> GetSubjectList(out IEnumerable<Exam> examList)
        {
            examList = GetExamList();

            return (from exam in examList
                    select exam.examSubject.name).Distinct();
        }

        public IEnumerable<string> GetSubjectList()
        {
            return GetSubjectList(out var ignore);
        }

        // Since we've already retrieved the exam list in the last method, we can make it an output
        // parameter to return the subject list and the exam list. To retrieve the exams that 
        // correspond to a subject, we need to use a dictionary with String as key, since we retrived
        // the subjects names. 
        public Dictionary<string, IEnumerable<Exam>> GetSubjectExams()
        {
            var subjectExams = new Dictionary<string, IEnumerable<Exam>>();
            var subjectList = GetSubjectList(out var examList);

            foreach (var subject in subjectList)
            {
                var subjectExamsTmp = from exam in examList
                                      where exam.examSubject.name == subject
                                      select exam;

                subjectExams.Add(subject, subjectExamsTmp);
            }

            return subjectExams;

        }

        public Dictionary<string, IEnumerable<StudentAvg>> AvgStudentGradeBySubject()
        {
            var response = new Dictionary<string, IEnumerable<StudentAvg>>();
            var subjectExams = GetSubjectExams();

            foreach (var subject in subjectExams)
            {
                // subject contains a Key (subject name in string) and a Value
                // (IEnumerable<Exam>) containing exams, we need to traverse 
                // the exam list so it's the value of each key.
                var averageStudentGrade = from exam in subject.Value  
                                          group exam by new {exam.testedStudent.uniqueID,
                                                             exam.testedStudent.name}
                                          into studentExams
                                          // to select multiple objects in a query
                                          // we can create an anonymous object
                                          select new StudentAvg                          
                                          {                                     
                                              // Since we grouped exams by Students uniqueID
                                              // the uniqueID became the key and exams the value
                                              // for each object in the group.
                                              studentID = studentExams.Key.uniqueID,
                                              studentName = studentExams.Key.name,
                                              average = studentExams.Average(test => test.grade)
                                          };
                response.Add(subject.Key, averageStudentGrade);
            }

            return response;
        }

        public Dictionary<string, IEnumerable<StudentAvg>> GetBestGrades (int topSelect)
        {
            var topGradesPerSubject = new Dictionary<string, IEnumerable<StudentAvg>>();
            var gradesPerSubject = AvgStudentGradeBySubject();

            foreach (var subject in gradesPerSubject)
            {
                var topGrades = (from grade in subject.Value
                                orderby grade.average descending
                                select grade);

                topGradesPerSubject.Add(subject.Key, topGrades.Take(topSelect));
            }

            return topGradesPerSubject;
        }

        public void PrintExams()
        {
            var subjectExams = GetSubjectExams();
            Console.WriteLine("From which subject would you like to print the exams?");
            int subjectNumber = 0;
            foreach (var subject in subjectExams)
            {
                Console.WriteLine($"[{subjectNumber}] {subject.Key}");
                subjectNumber++;
            }

            int choice = int.Parse(Console.ReadLine());
            var exams = subjectExams.ElementAt(choice);

            foreach (var exam in exams.Value)
            {
                Console.WriteLine(exam);
            }
        }
    }
}