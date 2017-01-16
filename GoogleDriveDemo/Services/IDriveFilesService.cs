using System.Collections.Generic;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;

namespace GoogleDriveDemo.Services
{
    public interface IDriveFilesService
    {
        List<File> GetAllFiles(DriveService service);
        List<File> GetAllSharedWithMeFiles(DriveService service);
    }
}
