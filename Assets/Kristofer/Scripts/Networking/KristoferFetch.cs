using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Kristofer.exploration
{
    public class KristoferFetch : MonoBehaviour
    {
        [SerializeField] private bool doFetch = false;
        public PlayerController player;
        public KristoferNetworkID id;

        private static KristoferFetch _instance;

        public static KristoferFetch instance
        {

            get
            {
                if(_instance == null)
                {
                    
                    _instance = FindObjectOfType<KristoferFetch>();

                }
                return _instance;
                

            }



        }


        //Getting data
        public IEnumerator Fetch(string uri)
        {


            using (UnityWebRequest req = UnityWebRequest.Get(uri))
            {
                yield return req.SendWebRequest();

                switch (req.result)
                {

                    case UnityWebRequest.Result.Success:
                        //Getting the Vector3 position from the server
                        var newPos = JsonUtility.FromJson<Vector3>(req.downloadHandler.text);
                        Debug.Log("Parsing Vector3...Success! ");
                        player.SetPos(newPos);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("HTTP error" + req.error);
                        break;


                }

            }


        }

        //Posting data
        public IEnumerator InnerPost(string uri, string data)
        {


            using (UnityWebRequest req = UnityWebRequest.Post(uri, data))
            {
                yield return req.SendWebRequest();

                switch (req.result)
                {

                    case UnityWebRequest.Result.Success:
                        Debug.Log("Success!");
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("HTTP error" + req.error);
                        break;


                }

            }


        }


        public static void Post(string uri, string data)
        {

            instance.StartCoroutine(instance.InnerPost(uri, data));


        }
    

        private void Update()
        {
            if (doFetch)
            {
               //StartCoroutine(Fetch("http://127.0.0.1:8125/set-position/" + id.name));


                doFetch = false;
            }
        }

    }

}
