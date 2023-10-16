using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Program
{
    public class Textbot
    {
        public required Textarray[] Textforbot { get; set; }

    }
    public class Textarray
    {
        public required string TextName { get; set; }
        public required string Text { get; set; }
    }
}