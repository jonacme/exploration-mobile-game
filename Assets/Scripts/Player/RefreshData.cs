using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RefreshData : MonoBehaviour
{
   

    public bool isRefreshing;
    public NetWorkId id;
    public PlayerMovement player;

    private void Init()
    {
        id = GetComponent<NetWorkId>();
        player = GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isRefreshing = false;
    }
      

    [UnityEditor.Callbacks.DidReloadScripts]
    static void OnScriptsReload()
    {
        foreach(var o in Object.FindObjectsOfType<RefreshData>())
        {
            o.Init();
        }
    }   

    public void GotData(string jsonData)
    {
        var newPos = JsonUtility.FromJson<Vector3>(jsonData);
        Debug.Log("Parsing vector 3" + newPos);
        player.SetPos(newPos);
    }

    IEnumerator Refresh()
    {
        isRefreshing = true;
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            StartCoroutine(Fetch.instance.Get("http://127.0.0.1:8125/set-positions/" + id._name, this));  
        }

        isRefreshing = false;
        yield break;
    }

    // Update is called once per frame
    void Update()
    {        
        if (!isRefreshing)
        {
            StartCoroutine(Refresh());
            Debug.Log("Move?");
        }   
    }        
}
