using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Survey_Project.Models
{
    public class Option
    {
        [Key]
        public int OptionId { get; set; }

        public string Choice { get; set; }

        [ForeignKey("QuestionId")]

        public Question question { get; set; }
        public int QuestionId { get; set; }
    }
}
