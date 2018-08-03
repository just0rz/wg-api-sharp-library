using System.IO;

namespace WGSharpAPITests
{
    public static class TestHelper
    {
        public static string LoadJson(string filename)
        {
            var jsonResponse = string.Empty;
            using (var fileStream = File.OpenRead(filename))
            {
                jsonResponse = File.ReadAllText(filename);
            }
            return jsonResponse;
        }
    }
}
