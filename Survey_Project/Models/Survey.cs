using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Date Created")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Fiscal Quarter")]
        public string Quarter { get; set; }

        public IList<CustomerSurvey> customerSurveys { get; set; }
    }
}
