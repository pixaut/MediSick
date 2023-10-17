using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Program
{
    public class SymptomsList
    {
        public required Symptoms[] Symptoms { get; set; }

    }
    public class Symptoms
    {
        public required string Сategory { get; set; }
        public required string List { get; set; }
    }

    public class Textbot
    {
        public required Textarray[] Textforbot { get; set; }

    }
    public class Textarray
    {
        public required string TextName { get; set; }
        public required string Text { get; set; }
    }




    public class User
    {
        public bool symptommenu { get; set; }
        public bool mainmenu { get; set; }
        public User()
        {
            mainmenu = mainmenu;
            symptommenu = symptommenu;
        }


    }
}