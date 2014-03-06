using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WGSharpAPI
{
    public class WGRawResponse
    {
        public string Status { get; set; }

        public int Count { get; set; }

        public object Data { get; set; }
    }
}
