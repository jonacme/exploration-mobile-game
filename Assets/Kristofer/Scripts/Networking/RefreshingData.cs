using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kristofer.exploration
{
    public class RefreshingData : MonoBehaviour
    {
        public bool isRefreshing;
        public PlayerController player;
        public KristoferNetworkID id;

        void Start()
        {

            Init();
        }

        void Init()
        {
            
            
            isRefreshing = false;
            id = GetComponent<KristoferNetworkID>();
            player = GetComponent<PlayerController>();
        }


        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            foreach (var o in FindObjectsOfType<RefreshingData>())
            {
                o.Init();

            }

        }

        public void GotData(string jsonData)
        {
            var newPos = JsonUtility.FromJson<Vector3>(jsonData);
            player.SetPos(newPos);

        }

        IEnumerator Refreshing()
        {
            isRefreshing = true;
            while (true)
            {
                yield return new WaitForSeconds(2f);

                KristoferFetch.Get("http://127.0.0.1:8125/set-position/" + id.name, this);
            }

            isRefreshing = false;

            yield break;
        }



        void Update()
        {
                if (!isRefreshing)
                {
                    //StartCoroutine(Refreshing());
                }
        }



    }



}
