using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using System.Linq;


namespace Utils
{
    [ExecuteAlways]
    class Getter : MonoBehaviour
    {
       public static T Get<T>() where T: MonoBehaviour
       {return FindObjectOfType<T>();}

       public static GameObject GetObject<T>() where T: MonoBehaviour
       {return FindObjectOfType<T>().gameObject;}
    }

    [ExecuteAlways]
    class UI : MonoBehaviour
    {
        public static TextMeshProUGUI IPtext;
        public static TextMeshProUGUI URLtext;
        public static TextMeshProUGUI Hovertext;

        static UI()
        {
            //Find evert TMP and assign it to a var
            foreach (var text in FindObjectsOfType<TextMeshProUGUI>())
            switch(text.name)
            {
                case "DisplayIP":  IPtext = text; break;
                case "DisplayURL":  URLtext = text; break;
                case "HoverText":  Hovertext = text; break;
            }   
        }

        public static void ChangeIP(string message) => IPtext.text = message;
        public static void ChangeIP(byte[] ip) => IPtext.text = $"IP: ({string.Join(", ", ip)})";
        public static void ChangeUrl(string message) => URLtext.text = message;
        public static void ChangeHover(string message) => Hovertext.text = message;
    }

    /// <summary>
    /// Class <c> GenerateDict </c> is to generate dictionaries to collect many things
    /// </summary>
    [ExecuteAlways]
    class GenerateDict: MonoBehaviour
    {
        /// <summary>
        /// Generate a Dictionary from every file in Resources/ <c>path</c>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Dictionary<string, TValue> FromPath<TValue>(string path) where TValue: UnityEngine.Object
        {
            Dictionary<string, TValue> map = new();
            string basicPath = "Assets/Resources/";
            string fullPath = basicPath + path + '/';

            foreach (string search in Directory.GetFiles(fullPath))
            {
                if (search.EndsWith(".meta")) continue;

                //The file with the path and with no extension
                string trimmed = Path.ChangeExtension(search[basicPath.Length..], null);
                int offByOne = path.Length + 1;
                
                map.Add(trimmed[offByOne..], Resources.Load(trimmed) as TValue);
            }
            return map;
        }
        /// <summary>
        /// Use FindObjectsOfType to generate a dictionary with the name of the object as a key
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>

        public static Dictionary<string, TValue> FromType<TValue>() where TValue: UnityEngine.Object
        {
            Dictionary<string, TValue> map = new();
            foreach(TValue actor in FindObjectsOfType<TValue>())
            {
                map.Add(actor.name, actor);
            }
            return map;
        }
    }
    [ExecuteAlways]
    class Globals 
    {
        public GameObject PlayerObject;
        public Player player;
        public GameObject MainCamera;
        public GameObject bullet;
    }

[ExecuteAlways]
    static class ObjectEquals
    {
        public static bool Array<T>(T[] first, T[] second)
        {
            return Enumerable.SequenceEqual(first, second);
        }
    }

    /// <summary>
    /// Transform different data types to strings
    /// </summary>
     [ExecuteAlways]
    static class Format
    {
        public static string IP(byte[] ip) { return $"IP: ({string.Join(", ", ip)})"; }
        public static string URL(string url) { return $"URL: {url}"; }
        public static string Array(Array array) { return $"[{string.Join(", ", array)}]"; }
        public static string Dictionary<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            string output = "";
            foreach (var i in dict)
                output += $"{i.Key}: {i.Value} <br>";
            return output;
        }
    }

[ExecuteAlways]
    static class Console
    {
        public static void Log(object value)
        {
            Type type = value.GetType();
            
            string output = "";

            if (type.IsPrimitive) output = value as string;
            if (type.IsArray) output = Format.Array(value as Array);
            if (type.IsClass){
                string[] strings = {};
                foreach (var property in type.GetProperties(System.Reflection.BindingFlags.Public))
                    strings.Append(property.Name + ',' + property);
            }
            
            

            Debug.Log(output);
        }



    }

[ExecuteAlways]
    static class Ballistic
    {
        static Dictionary<string, GameObject> ammo = GenerateDict.FromPath<GameObject>("Projectile");
        public static Bullet InstantiateBullet(GameObject cannon, string name, float meters) 
        {   
            GameObject bullet = UnityEngine.Object.Instantiate(ammo[name]);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetDirection(cannon.transform.forward);
            
            bullet.transform.position = cannon.transform.position;
            bullet.transform.position += bullet.transform.forward * meters;
            bullet.transform.rotation = cannon.transform.rotation;

            return bulletScript;
        }

        public static float Distance(GameObject a, GameObject b)
        {return Vector3.Distance(a.transform.position, b.transform.position);}

        public static float ToTarget(Bullet bullet, GameObject target, Message message = null)
        {

            bullet.gameObject.transform.LookAt(target.transform);
            bullet.SetDirection(bullet.transform.forward);
            
            float dist = Distance(bullet.gameObject, target);
            float time = dist / bullet.speed;
            
            bullet.lifespan = time;

            if (message != null)
                bullet.Load(message);
            
            return time;
        }
    }
    
}