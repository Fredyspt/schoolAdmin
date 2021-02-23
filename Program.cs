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
        }

        private static void EventAction(object sender, EventArgs e)
        {
            Printer.PrintTitle("Exiting...");
            Printer.Beep(500, 150, 3);
            Printer.PrintTitle("Exited successfully");
        }
    }
}
