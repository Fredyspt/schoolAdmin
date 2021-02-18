using System;
using System.Collections.Generic;

namespace CoreSchool.entities
{
    public class School:BaseSchoolObject
    {
        //This is an easier way of declaring getters and setters without the need of 2 different names for the attribute.

        public int foundingYear {get; set;}
        public string country { get; set; }
        public string city { get; set; }
        public SchoolType schoolType { get; set; }
        public List<Course> courses { get; set; }


        public School(string name, int foundingYear)
        {
            this.name = name;
            this.foundingYear = foundingYear;
        }

        // We can create another ctor, but in this case, we can ask for more data to create the object.
        // By initializing country and city with = "", their default value will be not established, 
        // which allows us to create the object without typing a country and city if we don't want to.
        public School(string name, int foundingYear, SchoolType schoolType, string country = "", string city = "")
        {
            (this.name, this.foundingYear) = (name, foundingYear);
            this.schoolType = schoolType;
            this.country = country;
            this.city = city;
        }

        // Another way to declare the ctor, but the parameters must be named different as the attributes
        // public School(string name, int foundingYear) => (name, foundingYear) = (Name, FoundingYear);

        // Since everything is C# is an object, everything inherits from the father class "Object"
        // With override, we're overriding what the method ToString() does, since it does something else in the father class.
        // By typing {System.Environment.NewLine} we make sure that we use the assigned character for a new line in different OS.
        // With $ we can access variables to use them on a string

        public override string ToString() => $"Name: \"{name}\", School type: {schoolType} {System.Environment.NewLine}Country: {country}, City: {city}";

    }
}