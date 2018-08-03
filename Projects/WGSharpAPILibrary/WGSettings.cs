/*
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
using WGSharpAPI.Enums;

namespace WGSharpAPI
{
    public class WGSettings
    {
        public WGRequestMethod RequsetMethod { get; }
        public WGRequestProtocol RequestProtocol { get; }
        public WGRequestTarget RequestTarget { get; }
        public int MaxRequestsPerSecond { get; } // not used

        public string BaseApiUri
        {
            get
            {
                switch (RequestTarget)
                {
                    case WGRequestTarget.WorldOfTanks:
                        return "api.worldoftanks.eu/wot";
                    case WGRequestTarget.WorldOfWarships:
                        return "api.worldofwarships.eu/wows";
                    case WGRequestTarget.WorldOfPlanes:
                        return "api.worldofwarplanes.eu/wowp";
                    default:
                        return null;
                }
            }
        }

        public WGSettings() : this(WGRequestMethod.GET, WGRequestProtocol.HTTP, 10)
        {
        }

        public WGSettings(WGRequestMethod requestMethod, WGRequestProtocol requestProtocol, int maxRequestsPerSecond) : this(requestMethod, requestProtocol, maxRequestsPerSecond, WGRequestTarget.WorldOfTanks)
        {
        }

        public WGSettings(WGRequestMethod requestMethod, WGRequestProtocol requestProtocol, int maxRequestsPerSecond, WGRequestTarget requestTarget)
        {
            RequsetMethod = requestMethod;
            RequestProtocol = requestProtocol;
            MaxRequestsPerSecond = maxRequestsPerSecond;
            RequestTarget = requestTarget;
        }
    }
}
