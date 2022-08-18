using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI LoadScrene")]
    public GameObject loadingScrene;
    

    
    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    public void battleScene()
    {
        StartCoroutine(LoadBattleScene());
    }

    IEnumerator LoadBattleScene()
    {    
        float loadingTime = 3f;
        yield return new WaitForSeconds(loadingTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
        Debug.Log("Encountered random enemy");

    }

    //void LoadScrene(bool loading)
    //{
    //    float waitTime = 3f;

    //    Debug.Log("Loading" + loading);     // need to make game manager for loading and not destorying the gameObject.
    //    switch (loading)
    //    {
    //        case true:
    //            Time.timeScale = 0f;
    //            loadingScrene.SetActive(true);
    //            yield return new WaitForSeconds(waitTime);
    //            break;
    //        case false:
    //            Time.timeScale = 1f;
    //            loadingScrene.SetActive(false);
    //            break;
    //    }
    //}

}
