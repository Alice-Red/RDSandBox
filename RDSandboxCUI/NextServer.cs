using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RDSandboxCUI
{
    public class NextServer
    {
        public NextServer() { Body(); }

        public async void Body() {
            var tcpListener = new TcpListener(IPAddress.Loopback, 12345);
            tcpListener.Start();

            while (true) {
                var tcpClient = await tcpListener.AcceptTcpClientAsync();

                // 接続を受け入れたら後続の処理をTaskに放り込む
                await Task.Run(async () => {
                    using (var stream = tcpClient.GetStream())
                    using (var reader = new StreamReader(stream))
                    using (var writer = new StreamWriter(stream)) {
                        // ヘッダー部分を全部読んでおく
                        var requestHeaders = new List<string>();
                        while (true) {
                            var line = await reader.ReadLineAsync();
                            if (String.IsNullOrWhiteSpace(line)) {
                                break;
                            }

                            requestHeaders.Add(line);
                        }

                        // 一行目(リクエストライン)は [Method] [Path] HTTP/[HTTP Version] となっている
                        var requestLine = requestHeaders.FirstOrDefault();
                        var requestParts = requestLine?.Split(new[] {' '}, 3);
                        if (requestParts != null && (!requestHeaders.Any() || requestParts.Length != 3)) {
                            await writer.WriteLineAsync("HTTP/1.0 400 Bad Request");
                            await writer.WriteLineAsync("Content-Type: text/plain; charset=UTF-8");
                            await writer.WriteLineAsync();
                            await writer.WriteLineAsync("Bad Request");
                            return;
                        }

                        // 接続元を出力しておく
                        Console.WriteLine("{0} {1}", tcpClient.Client.RemoteEndPoint, requestLine);

                        // パス
                        if (requestParts != null) {
                            var path = requestParts[1];
                            if (path == "/") {
                                // / のレスポンスを返す
                                await writer.WriteLineAsync("HTTP/1.0 200 OK");
                                await writer.WriteLineAsync("Content-Type: text/plain; charset=UTF-8");
                                await writer.WriteLineAsync();
                                await writer.WriteLineAsync("Hello! Konnichiwa! @ " + DateTime.Now); // 動的感を出す
                            } else if (path == "/hauhau") {
                                // 遅い処理をシミュレートするマン
                                await Task.Delay(5000); // 5秒

                                // /hauhau のレスポンスを返す
                                await writer.WriteLineAsync("HTTP/1.0 200 OK");
                                await writer.WriteLineAsync("Content-Type: text/plain; charset=UTF-8");
                                await writer.WriteLineAsync();
                                await writer.WriteLineAsync("Hauhau!!");
                            }
                        }
                    }
                });
            }
        }
    }
}