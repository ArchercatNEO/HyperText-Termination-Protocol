using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrowserRequest : Message
{
    public BrowserRequest(string url, Server server)
    {
        client = server;
        clientIP = server.ip;
        clientPort = 30;
        urlRequest = url;
        isGet = true;
    }

    public override void Pepito(params object[] objects) {}

    public BrowserRequest() {}
}
