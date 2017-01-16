using Google.Apis.Auth.OAuth2.Mvc;

namespace GoogleDriveDemo.Controllers
{
    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
        protected override FlowMetadata FlowData => new AuthFlowMetadata();
    }
}