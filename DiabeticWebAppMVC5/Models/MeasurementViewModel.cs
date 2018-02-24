using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiabeticWebAppMVC5.Models
{
    public class MeasurementViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Result")]
        [Range(1, 999, ErrorMessage = "Please enter a correct value")]
        public int Result { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}