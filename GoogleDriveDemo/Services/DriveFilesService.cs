using System.Collections.Generic;
using System.Linq;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;

namespace GoogleDriveDemo.Services
{
    public class DriveFilesService : IDriveFilesService
    {
        public List<File> GetAllFiles(DriveService service)
        {
            var request = service.Files.List();
            //todo: implement functionality to get all files with while loop. remove this hardcoded value RY 
            request.PageSize = 1000;
            request.Fields = "files(id, name, ownedByMe, sharingUser, fileExtension)";

            var list = request.Execute();

            return list.Files.ToList();
        }

        public List<File> GetAllSharedWithMeFiles(DriveService service)
        {
            var request = service.Files.List();
            request.Q = @"sharedWithMe";
            //todo: implement functionality to get all files with while loop. remove this hardcoded value RY 
            request.PageSize = 1000;
            request.Fields = "files(id, name, ownedByMe, sharingUser, fileExtension)";

            var list = request.Execute();

            return list.Files.ToList();
        }
    }
}