using Elasticsearch.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elasticsearch.Web.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetVehicles();
        Task<Vehicle> GetVehicleById(string id);
        Task<List<Vehicle>> SearchVehicles(string term);
        Task<bool> UpdateVehicle(Vehicle updatedVehicle);
    }
}
