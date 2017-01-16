using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace GoogleDriveDemo.Controllers
{
    public class DriveController : Controller
    {
        public async Task<ActionResult> IndexAsync(CancellationToken cancellationToken)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AuthFlowMetadata()).
                AuthorizeAsync(cancellationToken);

            if (result.Credential != null)
            {
                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = result.Credential,
                    ApplicationName = "GoogleDriveDemo"
                });

                var list = await service.Files.List().ExecuteAsync();
                ViewBag.Message = "Total files: " + list.Files.Count();
                ViewBag.FileNames = list.Files.Select(f => f.Name).ToArray();

                return View();
            }
            else
            {
                return new RedirectResult(result.RedirectUri);
            }
        }
    }
}