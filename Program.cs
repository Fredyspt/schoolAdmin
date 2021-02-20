using System;
using System.Collections.Generic;
using System.Linq;
using CoreSchool.entities;
using CoreSchool.Utilities;
using static System.Console;

namespace CoreSchool
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var engine =  new SchoolEngine();
            engine.Initialize();
        
            print_school_courses(engine.School);

            //engine.School.ClearData();

            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary.Add(10, "Hola");
            dictionary.Add(23, "Lorem Ipsum");

            foreach (var keyValPair in dictionary)
            {
                WriteLine($"Key: {keyValPair.Key}, Value: {keyValPair.Value}");
            }

            Printer.PrintTitle("Search through dictionary");
            WriteLine(dictionary[23]);

            dictionary[0] = "SampleText";
            WriteLine(dictionary[0]);

            // We kan use any variable type as key value
            // We can create a key, and we can replace its value afterwards
            // like dictionary.Add(TKey, Value) but we cannot add another
            // key with the same name, if we want to replace the key's 
            // value, we must do this -> dictionary[TKey] = Value.
            
            var dictionary2 = new Dictionary<string, string>();
            dictionary2["Moon"] = "Celestial body revolving around the earth";
            WriteLine(dictionary2["Moon"]);

        }

        public static void print_school_courses(School school)
        {
            Printer.PrintTitle("School Courses");

            // ? verifies the object (if it is != null) before verifying the attribute.
            if(school?.courses == null) return;
            foreach (var course in school.courses)
            {
                WriteLine($"Name: {course.name}, ID: {course.uniqueID}");
            }
        }
    }
}
