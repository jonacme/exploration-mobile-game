using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI LoadScrene")]
    public GameObject loadingScrene;
    public bool sceneLoading;
    

    
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
        sceneLoading = false;
        float loadingTime = 1.5f;
        while (true)
        {
            loadingScrene.SetActive(true);
            yield return new WaitForSeconds(loadingTime);
            loadingScrene.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("Encountered random enemy");

        }   
    }
}
