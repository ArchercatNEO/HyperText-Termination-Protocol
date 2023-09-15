using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Utils;

public class Player : MonoBehaviour
{
    byte[] heldIP;
    string heldURL;

    // Start is called before the first frame update
    void Start()
    {
        GenerateDict.FromPath<Material>("Materials");
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void CopyServer(GameObject server)
    {
        if (server.GetComponent<Server>() == null) return;

        heldIP = server.GetComponent<Server>().ip;
        string formatIp = string.Join(", ", heldIP);
        UI.ChangeIP($"IP: ({formatIp})");

        heldURL = server.GetComponent<Server>().url;
        string formatUrl = string.Join(", ", heldURL);
        UI.ChangeUrl($"URL: {formatUrl}");
    }
}
