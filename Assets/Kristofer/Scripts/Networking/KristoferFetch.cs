using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Kristofer.exploration
{
    public class KristoferFetch : MonoBehaviour
    {
        [SerializeField] private bool doFetch = false;
        
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
        public IEnumerator InnerGet(string uri, RefreshingData refreshData)
        {


            using (UnityWebRequest req = UnityWebRequest.Get(uri))
            {
                yield return req.SendWebRequest();

                switch (req.result)
                {

                    case UnityWebRequest.Result.Success:
                        //Getting the Vector3 position from the server
                        refreshData.GotData(req.downloadHandler.text);
                        //Debug.Log("Parsing Vector3...Success! ");   
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError("HTTP error: " + req.error);
                        break;


                }

            }


        }

        //Posting data
        IEnumerator InnerPost(string uri, string data)
        {
            Debug.Log("Innerpost: " + uri + data);

            using (UnityWebRequest req = UnityWebRequest.Post(uri, data))
            {
                yield return req.SendWebRequest();

                switch (req.result)
                {

                    case UnityWebRequest.Result.Success:
                        Debug.Log("Result success: " + req.result);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.Log("Protocol error: " + req.error);
                        break;
                    default:
                        Debug.Log("Unhandled error");
                        break;


                }

            }


        }


        public static void Post(string uri, string data)
        {
            Debug.Log("Post" + data);
            instance.StartCoroutine(instance.InnerPost(uri, data));
            Debug.Log("Starting Post");


        }

        public static void Get(string uri, RefreshingData refreshData)
        {

            instance.StartCoroutine(instance.InnerGet(uri, refreshData));


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
