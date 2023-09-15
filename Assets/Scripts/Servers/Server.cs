using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

/// <summary>
/// The "full" Server class with HTTP functions, Start and Update
/// </summary>
public class Server : BasicServer3
{
    public async Task Tick() 
    { 
        await Task.Delay((int) Mathf.Floor(Time.deltaTime * 1000)); 
    }

    
    // Start is called before the first frame update
    public virtual void Start()
    {
        foreach(Server server in FindObjectsOfType<Server>())
        {
            database.Add(server.url, server.ip);
            servers.Add(server.ip, server); 
        }
        
        materialMap = GenerateDict.FromPath<Material>("Materials");

        original = gameObject.GetComponent<MeshRenderer>().material;
        Player = Getter.Get<Player>();
        google = Getter.Get<DNS>();

        StartCoroutine(PassiveProcess());
    }

    protected virtual string ResolveGet(string request)
    {return "";}

    public virtual IEnumerator PassiveProcess() {yield return new WaitForSeconds(0);}

    protected IEnumerator ProcessMessage(Message message)
    {
        PowerOn();

        if (message.isGet)
        {
            string response = ResolveGet(message.urlRequest);
            Send(message.client, ip, 60, new DataPacket(response));
        }
        else if (message.isRelay)
        {
            message.isRelay = false;
            Send(servers[message.relayIP], message.clientIP, message.clientPort, message);
            yield return StartCoroutine(AwaitHTTP(message.clientIP, message.clientPort));
        }
    }

    public void Send(Server server, byte[] ip, int port, Message message)
    {
        Bullet bullet = Ballistic.InstantiateBullet(gameObject, "packet", 1);
        Ballistic.ToTarget(bullet, server.gameObject, message);
        bullet.Port(ip, port);
    }

    public IEnumerator AwaitHTTP(byte[] ip, int port, int timeout = 60000)
    {
        int tracker = 0;
        var key = (ip[0], ip[1], ip[2], ip[3], port);

        while (tracker < timeout)
        {
            if (pending.ContainsKey(key)) {
                Debug.Log(pending[key]);
                yield return pending[key];
            } // Exit the coroutine if the message is received
            
            yield return new WaitForSeconds(0.01f); // Adjust this delay as needed
            tracker += 10;
        }
        yield return null; // Handle the case when the timeout is reached
    }

    public void HandleHTTP(byte[] ip, int port, Message message)
    {
        var key = (ip[0], ip[1], ip[2], ip[3], port);
        pending[key] = message;
        StartCoroutine(ProcessMessage(message));
    }
    
}
