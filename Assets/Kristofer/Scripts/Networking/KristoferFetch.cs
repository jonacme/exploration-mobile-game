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
                        Debug.Log("Parsing Vector3...Success! ");   
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
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        break;


                }

            }


        }


        public void Post(string uri, string data)
        {

            instance.StartCoroutine(instance.InnerPost(uri, data));


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
