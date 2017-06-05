using System;
using System.ComponentModel.DataAnnotations;

namespace Elasticsearch.Web.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }

        [Display(Name = "Registration Year")]
        public DateTime YearRegistered { get; set; }
    }
}
