using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    public Server client;
    public string urlRequest;
    public byte[] clientIP; 
    public int clientPort; 
    public bool isRelay = false;
    public byte[] relayIP; 
    public int relayPort; 

    public bool isPower = true;
    public bool isDNsRequest = true;
    public bool isGet;
    public string data;

    public Message()
    {

    }

    public virtual void Pepito(params object[] objects){}
}
