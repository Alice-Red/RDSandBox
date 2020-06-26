using System;
using System.Collections.Generic;
using System.IO;

namespace RDSandboxCUI
{
    public class HttpResponseObject
    {

        public string HttpVersion { get; set; }
        public int ResponseCode { get; set; }
        public Dictionary<string, string> Header { get; private set; }

        private string ingredients;
        public string Ingredients {
            get => ingredients;
            set {
                ingredients = value;
                StoreHeader("Content-Length", ingredients.Length.ToString());
            }
        }

        public HttpResponseObject(string httpVersion) {
            HttpVersion = httpVersion;
            ResponseCode = 418;
        }
        
        public HttpResponseObject(string httpVersion, int code) {
            HttpVersion = httpVersion;
            ResponseCode = code;
            Header = new Dictionary<string, string>();
        }

        public void StoreHeader(string key, string value) {
            if (!Header.ContainsKey(key)) {
                Header.Add(key,value);
            } else {
                Header[key] = value;
            }
        }
    }
}