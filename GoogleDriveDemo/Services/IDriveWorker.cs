using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Google.Apis.Drive.v3;

namespace GoogleDriveDemo.Services
{
    public interface IDriveWorker
    {
        Task<ActionResult> RunAction(Controller controller, CancellationToken cancellationToken, Func<DriveService, Task<ActionResult>> viewResult);
    }
}
