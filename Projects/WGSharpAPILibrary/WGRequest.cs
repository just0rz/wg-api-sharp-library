using System.IO;
using System.Net;
using WGSharpAPI.Interfaces;

namespace WGSharpAPI
{
    /// <summary>
    /// Wrapper over WebRequest in order to properly mock the object
    /// </summary>
    public class WGRequest : IWGRequest
    {
        private WebRequest _webrequest;

        /// <summary>
        /// We don't want a parameterless constructor
        /// </summary>
        private WGRequest() { }

        public WGRequest(string requestURI)
        {
            _webrequest = HttpWebRequest.Create(requestURI);
        }

        public string GetResponse()
        {
            var response = _webrequest.GetResponse();

            var responseStream = response.GetResponseStream();
            string output = string.Empty;
            using (StreamReader reader = new StreamReader(responseStream))
            {
                output = reader.ReadToEnd();
            }
            response.Close();

            return output;
        }
    }
}
