using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Fetch : MonoBehaviour
{
    public bool doFetch = false;

    public MouseClickMovement player;
    public NetWorkId id;
    public RefreshData refreshdata;

    public static Fetch instance;

    private void Awake()
    {
        instance= this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(doFetch)
        {
            //Debug.Log(JsonUtility.ToJson(new Vector3(0, 10, 15)));
            //StartCoroutine(Get("http://127.0.0.1:8125/positions/" + id._name, refreshdata));      
            doFetch = false;
        }
    }

    public IEnumerator Get(string url, RefreshData refreshData)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(url))
        {
            yield return req.SendWebRequest();

            switch (req.result)
            {
                case UnityWebRequest.Result.Success:
                    refreshData.GotData(req.downloadHandler.text);                    
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log("HTTP error:" + req.error);
                    break;
            }
        }
    }

    public IEnumerator Post(string url, string data)
    {
        using (UnityWebRequest req = UnityWebRequest.Post(url, data))
        {
            yield return req.SendWebRequest();

            switch (req.result)
            {
                case UnityWebRequest.Result.Success:
                   
                    Debug.Log("success!");                    
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log("HTTP error:" + req.error);
                    break;
            }
        }
    }
}
