using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IProjectile
{
    public float speed {get; set;} = 10;
    public float lifespan {get; set;} = 3;
    Message message = new();
    byte[] ip;
    int port;

    Vector3 direction;

    public GameObject SetDirection(Vector3 vector)
    {
        direction = vector; 
        return gameObject;
    } 

    void FixedUpdate()
    {
        lifespan -= Time.deltaTime;
        if (lifespan < 0) Destroy(gameObject);
        
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Port(byte[] ip, int port)
    {
        this.ip = ip;
        this.port = port;
    }

    public void Load(Message message) 
    {
        this.message = message;
        ip = message.clientIP;
        port = message.clientPort;
    }

    void OnTriggerEnter(Collider collider)
    {
        switch(collider.tag)
        {
            case "DNS":
                DNS dns = collider.GetComponent<DNS>();
                dns.HandleHTTP(ip, port, message);
                break;
            
            case "DB":
                Database db = collider.GetComponent<Database>();
                db.HandleHTTP(ip, port, message);
                break;
            
            case "Client":
                Client client = collider.GetComponent<Client>();
                client.HandleHTTP(ip, port, message);
                break;
        }
        if (gameObject.name != "Cylinder"){
            Debug.Log(gameObject);
            Destroy(gameObject);
        }
    }

}