using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Survey_Project.Models
{
    public class Responses
    {
        [Key]
        public int ResponsesId { get; set; }


        [ForeignKey("OptionsId")]
        public Options option { get; set; }
        public int? OptionsId { get; set; }

        [ForeignKey("CustomerId")]

        public Customer customer { get; set; }
        public int? CustomerId { get; set; }
    }
}
