using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Survey_Project.Models
{
    public class Options
    {
        [Key]
        public int OptionId { get; set; }

        public string Option { get; set; }

        [ForeignKey("QuestionId")]

        public Questions question { get; set; }
        public int QuestionId { get; set; }
    }
}
