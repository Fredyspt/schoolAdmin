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
            
            Printer.PrintTitle("Welcome to the school administrator");
            bool exit = false;
            while(!exit)
            {   
                WriteLine("Type the number of the report you want to read.");
                Printer.PrintLine(20);
                WriteLine("[1] Exam list");
                WriteLine("[2] Subject list");
                WriteLine("[3] Subject exams");
                WriteLine("[4] Student grades");
                WriteLine("[5] Top grades");
                WriteLine("[6] Exit");
                Printer.PrintLine(20);

                string choice = Console.ReadLine();
                switch(choice)
                {
                    case "1":
                        var examList = reporter.GetExamList();
                        reporter.PrintExams();
                        break;

                    case "2":
                        var subjectLsit = reporter.GetSubjectList();
                        break;

                    case "3":
                        var subjectExams = reporter.GetSubjectExams();
                        break;

                    case "4":
                        var studentGrades = reporter.AvgStudentGradeBySubject();
                        break;

                    case "5":
                        var topGrades = reporter.GetBestGrades(3);
                        break;

                    case "6":
                        exit = true;
                        break;

                    default:
                        break;
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
