using System;
using System.Collections.Generic;
using System.Text;

namespace ApiInfrastructure.Models
{
    public class MeasurementApiModel
    {
        public int Id { get; set; }
        public int Result { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
