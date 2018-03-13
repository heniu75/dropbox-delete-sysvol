using System;
using System.Configuration;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace DropBoxUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var currentUser = WindowsIdentity.GetCurrent().Name;
                Console.WriteLine("Running as user '{0}'", currentUser);
                var folder = ConfigurationManager.AppSettings["DropBoxSystemVolPath"];
                if (Directory.Exists(folder))
                {
                    IdentityReference owner = new NTAccount(currentUser);
                    DirectoryInfo directory = new DirectoryInfo(folder);
                    DirectorySecurity directorySecurity = directory.GetAccessControl();
                    directorySecurity.AddAccessRule(new FileSystemAccessRule(currentUser,
                        FileSystemRights.DeleteSubdirectoriesAndFiles,
                        AccessControlType.Allow));
                    directorySecurity.AddAccessRule(new FileSystemAccessRule(currentUser,
                        FileSystemRights.FullControl,
                        AccessControlType.Allow));
                    UnmanagedCode.GiveRestorePrivilege();
                    directorySecurity.SetOwner(owner);
                    directory.SetAccessControl(directorySecurity);
                    Console.WriteLine("Deleting path '{0}'", folder);
                    Directory.Delete(folder, true);
                    Console.WriteLine("Done.");
                }
                else
                {
                    Console.WriteLine("Could not find path '{0}'", folder);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
    }
}
