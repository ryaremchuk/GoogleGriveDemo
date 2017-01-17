using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using GoogleDriveDemo.Services;

namespace GoogleDriveDemo
{
    public class AuthFlowMetadata: FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow AuthorizationFlow =
           new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
           {
               ClientSecrets = new ClientSecrets
               {
                   ClientId = ConfigurationManager.AppSettings[ConfigKey.GoogleClientId],
                   ClientSecret = ConfigurationManager.AppSettings[ConfigKey.GoogleClientSecret]
               },
               Scopes = new[] { DriveService.Scope.DriveReadonly },

               
               DataStore = new FileDataStore(GetTokenStoragePath())
           });

        public override string GetUserId(Controller controller)
        {
            var user = controller.Session["userId"];
            if (user == null)
            {
                user = Guid.NewGuid();
                controller.Session["userId"] = user;
            }
            return user.ToString();
        }

        public override IAuthorizationCodeFlow Flow => AuthorizationFlow;

        public override string AuthCallback => "/GoogleDriveDemo/AuthCallback/IndexAsync";

        private static string GetTokenStoragePath()
        {
            //todo: this is workaraound to not grant additional permissions for demo. Reimplement it in more correct way RY
            return Path.Combine(Path.GetTempPath(), "GoogleDriveTokenStorage");
        }
    }
}