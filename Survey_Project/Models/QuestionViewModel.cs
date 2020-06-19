using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey_Project.Models
{
    public class QuestionViewModel
    {
        public List<Question> Question { get; set; }
        public List<Option> Option { get; set; }
    }
}
