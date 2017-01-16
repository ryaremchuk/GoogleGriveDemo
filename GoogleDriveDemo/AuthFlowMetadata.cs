using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using Microsoft.Ajax.Utilities;

namespace GoogleDriveDemo
{
    public class AuthFlowMetadata: FlowMetadata
    {
        private static readonly IAuthorizationCodeFlow flow =
           new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
           {
               ClientSecrets = new ClientSecrets
               {
                   ClientId = ConfigurationManager.AppSettings["google-oauth2-clientId"],
                   ClientSecret = ConfigurationManager.AppSettings["google-oauth2-clientSecret"]
               },
               Scopes = new[] { DriveService.Scope.DriveReadonly },
               //note: this is workaraound to not grant additional permissions for demo. RY
               DataStore = new FileDataStore(Path.Combine(Path.GetTempPath(), "GoogleDriveTokenStorage"))
           });

        public override string GetUserId(Controller controller)
        {
            var user = controller.Session["user"];
            if (user == null)
            {
                user = Guid.NewGuid();
                controller.Session["user"] = user;
            }
            return user.ToString();
        }

        public override IAuthorizationCodeFlow Flow => flow;

        public override string AuthCallback
        {
            get { return "/GoogleDriveDemo/AuthCallback/IndexAsync"; }
        }
    }
}