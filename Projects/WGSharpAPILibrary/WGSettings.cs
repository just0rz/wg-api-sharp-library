using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WGSharpAPI.Enums;

namespace WGSharpAPI
{
    public class WGSettings
    {
        private WGRequestMethod _requestMethod;
        public WGRequestMethod RequsetMethod { get { return _requestMethod; } }

        private WGRequestProtocol _requestProtocol;
        public WGRequestProtocol RequestProtocol { get { return _requestProtocol; } }

        private int _maxRequestsPerSecond;
        public int MaxRequestsPerSecond { get { return _maxRequestsPerSecond; } }

        public WGSettings()
        {
            _requestMethod = WGRequestMethod.GET;
            _requestProtocol = WGRequestProtocol.HTTP;
            _maxRequestsPerSecond = 10;
        }

        public WGSettings(WGRequestMethod requestMethod, WGRequestProtocol requestProtocol, int maxRequestsPerSecond)
        {
            _requestMethod = requestMethod;
            _requestProtocol = requestProtocol;
            _maxRequestsPerSecond = maxRequestsPerSecond;
        }
    }
}
