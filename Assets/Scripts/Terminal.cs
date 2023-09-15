using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    [SerializeField] private int priviledge;
    [SerializeField] private int SecReq;
    GameObject player;

    void Start(){
        //player = GetComponent<Health>().gameObject;
    }

    public void PopLore(){
        if (player.GetComponent<Account>().priviledge > SecReq)
        return;
    }

    public void EscalatePriviledge(){
        player.GetComponent<Account>().priviledge += priviledge;
    }

    
}
