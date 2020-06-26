using System;
using System.Net;
using System.Text;

namespace RDSandboxCUI
{
    public class HttpPrac
    {
        public HttpPrac() {
            try {
                HttpListener listener = new HttpListener();
                listener.Prefixes.Add("http://127.0.0.1:12345/");
                listener.Start();
                while (true) {
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerResponse res = context.Response;
                    res.StatusCode = 200;
                    byte[] content = Encoding.UTF8.GetBytes("Copyright (C) Redkun. 2020");
                    res.OutputStream.Write(content, 0, content.Length);
                    res.Close();
                }
            } catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);

            }
        }
    }
}