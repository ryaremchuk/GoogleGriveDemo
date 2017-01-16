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

        [Dependency]
        public IDriveFilesService DriveFilesService { get; set; }

        public async Task<ActionResult> IndexAsync(CancellationToken cancellationToken)
        {
            return await DriveWorker.RunAction(this, cancellationToken,
                service =>
                {
                    //todo: implement ViewModel to have more flexibility on View-side. RY
                    ViewBag.PersonalFileNames = DriveFilesService.GetAllFiles(service).Select(f => f.Name).ToArray();

                    ViewBag.SharedWithMeFileNames = DriveFilesService.GetAllSharedWithMeFiles(service)
                        .Select(f =>
                        {
                            var user = f.SharingUser == null ? "Unknown" : f.SharingUser.DisplayName;
                            return $"{f.Name} (by {user})";
                        }).ToArray();

                    return View();
                });
        }
    }
}