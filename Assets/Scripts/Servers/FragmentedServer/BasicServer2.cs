using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Add material functions
/// </summary>
public abstract class BasicServer2 : BasicServer1
{
    public void PowerOn()
    {gameObject.GetComponent<MeshRenderer>().material = materialMap["on"];}
    public void PowerOff()
    {gameObject.GetComponent<MeshRenderer>().material = materialMap["off"];}

    public void FlipPower()
    {
        if (gameObject.GetComponent<MeshRenderer>().material == materialMap["on"])
            gameObject.GetComponent<MeshRenderer>().material = materialMap["off"];
        
        else 
            gameObject.GetComponent<MeshRenderer>().material = materialMap["on"];
    }

    public async void TemporaryMaterial(int ms, Material material)
    {
        gameObject.GetComponent<MeshRenderer>().material = material;
        await Task.Delay(ms);
        gameObject.GetComponent<MeshRenderer>().material = original;
    }
}
