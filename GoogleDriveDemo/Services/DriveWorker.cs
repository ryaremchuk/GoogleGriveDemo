using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace GoogleDriveDemo.Services
{
    public class DriveWorker: IDriveWorker
    {
        public async Task<ActionResult> RunAction(Controller controller, CancellationToken cancellationToken, Func<DriveService, ActionResult> viewResult)
        {
            var result = await new AuthorizationCodeMvcApp(controller, new AuthFlowMetadata()).AuthorizeAsync(cancellationToken);

            if (result.Credential != null)
            {
                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = result.Credential,
                    ApplicationName = "GoogleDriveDemo"
                });

                return viewResult(service);
            }
            else
            {
                return new RedirectResult(result.RedirectUri);
            }
        }
    }
}