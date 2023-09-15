using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Declare variables
/// </summary>
public abstract class BasicServer1 : MonoBehaviour
{
    protected float connectionSpeed = 1;
    protected int[] openPorts;
    public byte[] ip = new byte[4];
    public string url;
    protected string hoverText;
    protected Dictionary<string, byte[]> database = new();
    protected Dictionary<byte[], Server> servers = new();

    protected Dictionary<string, Material> materialMap;

    protected Dictionary<(byte, byte, byte, byte, int), Message> pending = new();

    protected DNS google;

    protected Material original;

    protected GameObject player;
    protected Player Player;

    public class Async {
        public Coroutine coroutine { get; private set; }
        public object result;
        private IEnumerator target;

        public Async(MonoBehaviour owner, IEnumerator target) {
            this.target = target;
            this.coroutine = owner.StartCoroutine(Run());
        }

        private IEnumerator Run() {
            while(target.MoveNext()) {
                result = target.Current;
                yield return result;
            }
        }
}
}
