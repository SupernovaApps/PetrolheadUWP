using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using UWPBackend.DataObjects;
using UWPBackend.Models;

namespace UWPBackend.Controllers
{
    public class VehicleController : TableController<Vehicle>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Vehicle>(context, Request);
        }

        // GET tables/Vehicle
        public IQueryable<Vehicle> GetAllVehicle()
        {
            return Query(); 
        }

        // GET tables/Vehicle/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Vehicle> GetVehicle(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Vehicle/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Vehicle> PatchVehicle(string id, Delta<Vehicle> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Vehicle
        public async Task<IHttpActionResult> PostVehicle(Vehicle item)
        {
            Vehicle current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Vehicle/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteVehicle(string id)
        {
             return DeleteAsync(id);
        }
    }
}
