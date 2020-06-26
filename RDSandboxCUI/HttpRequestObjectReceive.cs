using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Text.RegularExpressions;

namespace RDSandboxCUI
{
    public struct HttpRequestObjectReceive
    {
        public static Dictionary<string, RequestType> RequestDictionary = new Dictionary<string, RequestType>() {
            {"GET", RequestType.Get},
            {"PUT", RequestType.Put},
            {"POST", RequestType.Post},
            {"HEAD", RequestType.Head},
            {"OPTIONS", RequestType.Options}
        };

        public RequestType RqType { get; private set; }
        public string Path { get; private set; }
        public string HttpVersion { get; private set; }

        public Dictionary<string, string> Header { get; private set; }

        public string Ingredients { get; private set; }

        public HttpRequestObjectReceive(string request) {
            RqType = RequestType.Err;
            Path = @"/";
            HttpVersion = "1.1";
            Header = new Dictionary<string, string>();
            Ingredients = "";
            Parse(request);
        }

        private void Parse(string request) {
            var RquestPerLine = request.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');

            bool isHeader = false;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < RquestPerLine.Length; i++) {
                if (i == 0) {
                    // GET / HTTP/1.1
                    // to
                    // [GET], [/], [HTTP/1.1]
                    var firstline = RquestPerLine[i].Split(' ');

                    if (firstline.Length != 3) // Bad Request
                        return;

                    RqType = RequestDictionary.ContainsKey(firstline[0])
                        ? RequestDictionary[firstline[0]]
                        : RequestType.Err;
                    Path = firstline[1];
                    if (Path.Last() == '/')
                        Path += "index.html";

                    HttpVersion = firstline[2].Replace("HTTP/", "");
                    isHeader = true;
                } else if (isHeader) {
                    if (!RquestPerLine[i].Contains(':')) {
                        sb.Append(RquestPerLine[i]);
                        isHeader = false;
                    } else {
                        Regex headerRegex = new Regex(@"(?<key>[\w-]*?):\s(?<value>.*?)$");
                        var headerMatches= headerRegex.Match(RquestPerLine[i]);
                        Header.Add(headerMatches.Groups["key"].Value, headerMatches.Groups["value"].Value);
                    }
                } else {
                    sb.Append(RquestPerLine[i]);
                }
            }


        }


        public bool Validate() { return RqType != RequestType.Err; }

        public static bool Validate(string value) {
            Regex rg = new Regex(".*");
            return rg.IsMatch(value);
            // return true;
        }
    }
}