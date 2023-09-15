using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

/// <summary>
/// Add UI functions
/// </summary>
public abstract class BasicServer3 : BasicServer2
{
    public void DisplayHover() 
    {UI.ChangeHover(Format.IP(ip) + "<br>" + Format.URL(url));}
    public void OnClick() 
    {Player.CopyServer(gameObject);}
}
