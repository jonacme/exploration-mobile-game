using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshData : MonoBehaviour
{
    public bool isRefreshing;
    public NetworkID id;
    public ControlCharacter character;

    void Init()
    {
        isRefreshing = false;
        id = GetComponent<NetworkID>();
        character = GetComponent<ControlCharacter>();
    }

    void Start()
    {
        Init();
    }

    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        foreach (var o in Object.FindObjectsOfType<RefreshData>())
        {
            o.Init();
        }
    }

    public void GotData(string jsonData)
    {
        var newpos = JsonUtility.FromJson<Vector3>(jsonData);
        character.SetPos(newpos);
    }

    IEnumerator Refresh()
    {
        isRefreshing = true;

        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            HttpFetch.Get("http://127.0.0.1:8125/position/" + id.name, this);
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
        }
    }
}
