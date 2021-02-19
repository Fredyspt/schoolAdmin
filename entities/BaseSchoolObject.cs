using System;

namespace CoreSchool.entities
{
    // Abstract doesn't let any object to be created with this class
    public abstract class BaseSchoolObject
    {
        public string name { get; set; }
        public string uniqueID { get; private set; }

        public BaseSchoolObject() => uniqueID = Guid.NewGuid().ToString();

        public override string ToString()
        {
            return $"{name}, {uniqueID}";
        }
    }
}