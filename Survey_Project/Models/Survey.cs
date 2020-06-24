using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Survey_Project.Models
{
    public class Survey
    {
        [Key]
        public int SurveyId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        
        //DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Created")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Fiscal Quarter")]
        public string Quarter { get; set; }

        [ForeignKey("AdminId")]

        public Admin Admin { get; set; }
        public int AdminId { get; set; }
    }
}
