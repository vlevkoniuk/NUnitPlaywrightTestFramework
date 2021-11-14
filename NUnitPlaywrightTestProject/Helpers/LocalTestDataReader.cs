
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace NUnitSeleniumTestProjectExample.Helpers
{
    public static class LocalTestDataReader
    {
        private static string path = Path.Combine(Directory.GetParent(
            Directory.GetCurrentDirectory()).Parent.Parent.FullName, 
            "Models", "TestData");
        public static List<T> LoadTestDataList<T>(string filename)
        {
            filename = filename + ".json";
            string path2file = Path.Combine(path, filename);
            List<T> retList = new List<T>();
            using (StreamReader r = new StreamReader(path2file))
            {
                string json = r.ReadToEnd();
                List<T> items = JsonSerializer.Deserialize<List<T>>(json);
                retList = items;
            }
            return retList;
        }

        public static T LoadTestData<T>(string filename)
        {
            filename = filename + ".json";
            string path2file = Path.Combine(path, filename);
            using (StreamReader r = new StreamReader(path2file))
            {
                string json = r.ReadToEnd();
                T item = JsonSerializer.Deserialize<T>(json);
                return item;
            }

        }
        
    }
}