using System;
using System.Collections.Generic;
using System.Linq;
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
}