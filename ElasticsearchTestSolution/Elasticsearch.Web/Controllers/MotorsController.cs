using Elasticsearch.Web.Interfaces.Services;
using Elasticsearch.Web.Models;
using Elasticsearch.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Elasticsearch.Web.Controllers
{
    public class MotorsController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public MotorsController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<ActionResult> Overview()
        {
            var vm = new VehicleDashboardViewModel
            {
                Vehicles = new List<Vehicle>()
            };

            var vehicles = await _vehicleService.GetVehicles();

            foreach (var vehicle in vehicles)
            {
                vm.Vehicles.Add(vehicle);
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> PerformSearch(VehicleDashboardViewModel vm)
        {
            var filteredVehicles = await _vehicleService.SearchVehicles(vm.SearchTerm);
            var vehicleDashboardViewModel = new VehicleDashboardViewModel
            {
                Vehicles = filteredVehicles
            };

            return View("Overview", vehicleDashboardViewModel);
        }

        public async Task<ActionResult> EditVehicle(string id)
        {
            var vehicle = await _vehicleService.GetVehicleById(id);

            var vm = new EditVehicleViewModel
            {
                Vehicle = vehicle
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateVehicle(EditVehicleViewModel vm)
        {
            var updateSuccessful = await _vehicleService.UpdateVehicle(vm.Vehicle);

            return updateSuccessful ? RedirectToAction("Overview") : RedirectToAction("EditVehicle", vm.Vehicle.Id);
        }
    }
}