using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using GoogleDriveDemo.Services;
using Microsoft.Practices.Unity;

namespace GoogleDriveDemo.Controllers
{
    public class DriveController : Controller
    {
        [Dependency]
        public IDriveWorker DriveWorker { get; set; }

        public async Task<ActionResult> IndexAsync(CancellationToken cancellationToken)
        {
            return await DriveWorker.RunAction(this, cancellationToken,
                async service =>
                {
                    var list = await service.Files.List().ExecuteAsync();
                    ViewBag.Message = "Total files: " + list.Files.Count();
                    ViewBag.FileNames = list.Files.Select(f => f.Name).ToArray();

                    return View();
                });
        }
    }
}