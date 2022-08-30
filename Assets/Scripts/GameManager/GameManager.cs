using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI LoadScrene")]
    public string lastScene;
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
    
    public void battleScene(Enemies enemy)
    {
        StartCoroutine(LoadBattleScene(enemy));
    }

    IEnumerator LoadBattleScene(Enemies enemy)
    {
        sceneLoading = false;
        float loadingTime = 1.2f;
        
        while (true)
        {
            loadingScrene.SetActive(true);
            yield return new WaitForSeconds(loadingTime);
            loadingScrene.SetActive(false);

            lastScene = SceneManager.GetActiveScene().name;
            // need to load the enemy type method too
            DataBase.InitFightScene(enemy);
            SceneManager.LoadScene("Juma-BattleScene");
            yield break;
        }   
    }

    IEnumerator ExitFightSceneRoutine()            // not yet complete
    {
        yield return SceneManager.LoadSceneAsync(lastScene);
        yield return SceneManager.SetActiveScene(SceneManager.GetSceneByName(lastScene));
        yield break;
    }

    public static void ExitFightScene()
    {
        //StartCoroutine(ExitFightScene());
    }
}
