using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Database : Server
{

    public byte[] GetIp(string url){return database[url];}



    
}
