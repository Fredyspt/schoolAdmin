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

        public Dictionary<string, IEnumerable<object>> AvgStudentGradeBySubject()
        {
            var response = new Dictionary<string, IEnumerable<object>>();
            var subjectExams = GetSubjectExams();

            foreach (var subject in subjectExams)
            {
                // subject contains a Key (subject name in string) and a Value
                // (IEnumerable<Exam>) containing exams, we need to traverse 
                // the exam list so it's the value of each key.
                var group = from exam in subject.Value  
                            select new                      //to select multiple objects in a query
                            {                               //we can create an anonymous object
                                exam.examSubject.name, 
                                exam.grade
                            };
            }

            return response;
        }
        
    }
}