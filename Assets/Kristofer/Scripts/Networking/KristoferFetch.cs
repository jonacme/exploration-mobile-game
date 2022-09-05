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

        public IEnumerator Fetch(string url)
        {


            using (UnityWebRequest req = UnityWebRequest.Get(url))
            {
                yield return req.SendWebRequest();

                switch (req.result)
                {

                    case UnityWebRequest.Result.Success:
                        //Getting the Vector3 position from the server
                        var newPos = JsonUtility.FromJson<Vector3>(req.downloadHandler.text);
                        Debug.Log("Parsing Vector3:");
                        player.SetPos(newPos);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.Log("HTTP error" + req.error);
                        break;


                }

            }




        }


        private void Update()
        {
            if (doFetch)
            {

                StartCoroutine(Fetch("http://127.0.0.1:8125/position/" + id.name));
                doFetch = false;
            }
        }

    }

}
