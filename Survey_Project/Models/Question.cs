using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Survey_Project.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        public string QuestionString { get; set; }

        [ForeignKey("SurveyId")]

        public Survey survey { get; set; }
        public int SurveyId { get; set; }

    }
}
