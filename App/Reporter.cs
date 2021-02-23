using System;
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
    }
}