using Elasticsearch.Web.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Elasticsearch.Web.ViewModels
{
    public class VehicleDashboardViewModel
    {
        public List<Vehicle> Vehicles { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }
    }
}