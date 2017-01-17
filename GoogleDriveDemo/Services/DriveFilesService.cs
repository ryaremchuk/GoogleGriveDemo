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
            request.Fields = "files(id, name, sharingUser)";

            return ExtractAllFiles(request);
        }

        public List<File> GetAllSharedWithMeFiles(DriveService service)
        {
            var request = service.Files.List();
            request.Q = @"sharedWithMe";
            request.Fields = "files(id, name, sharingUser)";

            return ExtractAllFiles(request);
        }

        private List<File> ExtractAllFiles(FilesResource.ListRequest request)
        {
            var result = new List<File>();
            request.Fields = string.Join(",", "nextPageToken", request.Fields).TrimEnd(',');
            request.PageSize = 500;

            do
            {
                var response = request.Execute();
                result.AddRange(response.Files.ToList());

                request.PageToken = response.NextPageToken;
            } while (!string.IsNullOrEmpty(request.PageToken));

            return result;
        }
    }
}