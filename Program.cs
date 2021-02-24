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
                WriteLine("[3] Student grades");
                WriteLine("[4] Top grades");
                WriteLine("[5] Exit");
                Printer.PrintLine(20);

                string choice = Console.ReadLine();
                switch(choice)
                {
                    case "1":
                        reporter.PrintExams();
                        break;

                    case "2":
                        reporter.PrintSubjectList();
                        break;

                    case "3":
                        reporter.PrintStudentGrades();
                        break;

                    case "4":
                        reporter.PrintTopGrades();
                        break;

                    case "5":
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
