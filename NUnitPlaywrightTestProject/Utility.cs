using System.IO;

namespace NUnitPlaywrightTestProject
{
    public class Utility
    {
        public static string GetProjectRooDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            return currentDirectory.Split("bin")[0];
        }
    }
}