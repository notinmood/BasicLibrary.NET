using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationConsole.ClassesForTest
{
    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public virtual string Number { get; set; }

        public string GetName()
        {
            return this.Name;
        }
    }
}