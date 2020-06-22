using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey_Project.Models
{
    public class QuestionViewModel
    {
        public Question Question { get; set; } 
        public string OptionOne { get; set; }
        public string OptionTwo { get; set; }
        public string OptionThree { get; set; }
        public string OptionFour{ get; set; }

    }

}
