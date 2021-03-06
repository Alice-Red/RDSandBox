using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RDSandboxCUI
{
    public class HttpResponseObject
    {

        public string HttpVersion { get; set; }
        public int ResponseCode { get; set; }
        public Dictionary<string, string> Header { get; private set; }

        private byte[] ingredients;
        public byte[] Ingredients {
            get => ingredients;
            set {
                ingredients = value;
                StoreHeader("Content-Length", ingredients.Length.ToString());
            }
        }

        public HttpResponseObject(string httpVersion) {
            HttpVersion = httpVersion;
            ResponseCode = 418;
            Header = new Dictionary<string, string>();
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

        public byte[] ToByteArray() {
            var head = Encoding.UTF8.GetBytes(HeaderToString());
            return head.Concat(Ingredients).ToArray();
        }

        private string HeaderToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"HTTP/{HttpVersion} {ResponseCode} {HttpResponseCode.ResponseCode[ResponseCode]}");
            foreach (var item in Header) {
                sb.AppendLine($"{item.Key}: {item.Value}");
            }

            sb.AppendLine();
            return sb.ToString();
        }
    }
}