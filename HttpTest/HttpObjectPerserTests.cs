﻿using System;
using NUnit.Framework;
using RDSandboxCUI;

namespace HttpTest
{
    [TestFixture]
    public class HttpObjectPerserTests
    {
        // private HttpRequestObject hro;


        [SetUp]
        public void Init() {
            // hro = new HttpRequestObject("");
        }

        [Test]
        public void Test1() {
            HttpRequestObjectReceive ex1 = new HttpRequestObjectReceive(
                @"GET / HTTP/1.1 \r\n" +
                "Host: 127.0.0.1:12345 \r\n" +
                "Connection: keep-alive \r\n" +
                "Cache-Control: max-age=0 \r\n" +
                "Upgrade-Insecure-Requests: 1 \r\n" +
                "User-Agent: Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.106 Safari/537.36 \r\n" +
                "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9 \r\n" +
                "Sec-Fetch-Site: none \r\n" +
                "Sec-Fetch-Mode: navigate \r\n" +
                "Sec-Fetch-User: ?1 \r\n" +
                "Sec-Fetch-Dest: document \r\n" +
                "Accept-Encoding: gzip, deflate, br \r\n" +
                "Accept-Language: ja,en-US;q=0.9,en;q=0.8"
            );
            Assert.Equals(ex1.RqType, RequestType.Get);
            Assert.Equals(ex1.Path, "/");
            Assert.Equals(ex1.HttpVersion, "1.1");
            
            Assert.True(ex1.Header.ContainsKey("Host"));
            // Assert.False(ex1.Validate()); 
        }
    }
}