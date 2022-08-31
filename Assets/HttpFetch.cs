using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpFetch : MonoBehaviour
{
    public bool doFetch = false;
    public ControlCharacter player;
    public NetworkID id;

    IEnumerator Fetch(string uri)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(uri))
        {
            yield return req.SendWebRequest();

            switch (req.result)
            {
                case UnityWebRequest.Result.Success:
                    var newpos = JsonUtility.FromJson<Vector3>(req.downloadHandler.text);
                    Debug.Log("parsing Vector3, got: " + newpos);
                    player.SetPos(newpos);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP error: " + req.error);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doFetch)
        {
            StartCoroutine(Fetch("http://127.0.0.1:8125/position/" + id.name));
            doFetch = false;
        }
    }
}
