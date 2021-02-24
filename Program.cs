using System;
using System.Collections.Generic;
using System.Linq;
using CoreSchool.App;
using CoreSchool.entities;
using CoreSchool.Utilities;
using static System.Console;

namespace CoreSchool
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += EventAction;

            var engine =  new SchoolEngine();
            engine.Initialize();

            var reporter = new Reporter(engine.GetObjectDictionary());
            var examList = reporter.GetExamList();
            var subjectLsit = reporter.GetSubjectList();
            var subjectExams = reporter.GetSubjectExams();
            var studentGrades = reporter.AvgStudentGradeBySubject();
            var topGrades = reporter.GetBestGrades(3);

            Printer.PrintTitle("Exam input on console");
            var newExam = new Exam();
            string name, gradeString;
            float grade;

            WriteLine("Enter exam name");
            Printer.PressEnter();
            name = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(name))
            {
                Printer.PrintTitle("Name cannot be null or empty.");
            } else
            {
                newExam.name = name.ToLower();
                WriteLine("Name saved\n");
            }

            WriteLine("Enter exam grade");
            Printer.PressEnter();
            gradeString = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(gradeString))
            {
                Printer.PrintTitle("Grade cannot be null or empty.");
            } else
            {
                // try: in this case, tries to convert the string to float if the input is 
                // in the correct form (4.5, 3.2, etc.), if it fails, catch will throw an
                // exception. If there are multiple exceptions, the exceptions order matters
                // when throwing an error.            
                try
                {
                    newExam.grade = float.Parse(gradeString);
                    if(newExam.grade < 0.0f || newExam.grade > 5.0f)
                    {
                        throw new ArgumentOutOfRangeException("Grade must be a value between 0 and 5");
                    }
                    WriteLine("Grade saved");
                } catch(ArgumentOutOfRangeException outOfRange)
                {
                    Printer.PrintTitle(outOfRange.Message);
                } catch(Exception)
                {
                    Printer.PrintTitle("Invalid number");
                } finally
                {
                    Printer.PrintTitle("FINALLY");
                }
                
            }
        }

        private static void EventAction(object sender, EventArgs e)
        {
            Printer.PrintTitle("Exiting...");
            Printer.Beep(750, 150, 3);
            Printer.PrintTitle("Exited successfully");
        }
    }
}
