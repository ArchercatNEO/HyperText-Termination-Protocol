using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using System.Linq;

public class DNS : Server
{
    private Database db;
    private Dictionary<string, byte[]> cache;



    protected override string ResolveGet(string request)
    {
        return string.Join(",", database[request]);
    }
    
}
