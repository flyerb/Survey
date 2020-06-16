using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey_Project.Models
{
    public class CustomerSurvey
    {
        public Customer customer { get; set; }
        public int CustomerId { get; set; }
        public Survey survey { get; set; }
        public int SurveyId { get; set; }
    }
}
