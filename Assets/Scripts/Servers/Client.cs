using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Client : Server
{
    // Start is called before the first frame update
    public override void Start() 
    {
        base.Start();
    }


    public override IEnumerator PassiveProcess()
    {
        while (true)
        {
            
            StartCoroutine(Request());
            yield return new WaitForSeconds(1);
            
        }
    }

    private IEnumerator Request()
    {
        Send(google, ip, 60, new BrowserRequest("www.amazon", this));

        Async async = new(this, AwaitHTTP(google.ip, 60));
        while(async.result is WaitForSeconds) {yield return new WaitForSeconds(0.01f);}
        
        Message raw = (Message) async.result;
        string data = raw.data;

        if (data  == string.Join(',', new int[] {67, 87, 54, 34})) PowerOn();
    }
    
}