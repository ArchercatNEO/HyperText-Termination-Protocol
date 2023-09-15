using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    public GameObject gameObject {get;}
    public float speed {get;}
    public float lifespan {get; set;}
    public GameObject SetDirection(Vector3 vector);
}
