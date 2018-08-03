﻿/*
The MIT License (MIT)

Copyright (c) 2016 Iulian Grosu

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
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

        public bool IsConsumed { get; private set; }
        public string RequestURI { get; set; }

        /// <summary>
        /// We don't want a parameterless constructor
        /// </summary>
        private WGRequest()
        {
            IsConsumed = false;
        }

        public WGRequest(string requestURI)
            : this()
        {
            RequestURI = requestURI;
        }

        public string GetResponse()
        {
            string output = string.Empty;
            if (!IsConsumed)
            {
                _webrequest = WebRequest.Create(RequestURI);
                IsConsumed = true;
                using (var response = _webrequest.GetResponse())
                {

                    var responseStream = response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        output = reader.ReadToEnd();
                    }
                    response.Close();
                }
            }

            return output;
        }

        public static IWGRequest GetRequest()
        {
            return new WGRequest();
        }
    }
}
