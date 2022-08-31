using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTime : MonoBehaviour
{
    [Header("Player Turn")]
    [Range(0,1)]
    public float progressTimer = 0f;     // not progressed yet
    [Range(1, 0)]
    public float turnTime = 1f;

    [Header("Lists of Enemies")]
    public List<GameObject> enemies;

    [Header("Battle UI")]
    public GameObject battleUI;

    [Header("Battle Menu")]
    public Slider progressBar;
    public Button fightButton;

    [Header("Enemy Layer Mask")]
    public LayerMask enemyLayer;

    [Header("Player Object")]
    public GameObject player;

    [Header("Selection Settings")]
    public GameObject selectedEnemy;
    public GameObject selectionArrow;

    public static void EnemyTurn()
    {
        Debug.Log("Attacked");
    }

    public static void Fight()
    {
        //battle starts  turntimer should be 0 and it should progress

        if (instance.progressTimer >= instance.turnTime)
        {
            instance.turnTime = 0f;
        }
        instance.fightButton.enabled = true;
        Debug.Log("FightButton Pressed" + instance.fightButton);

        instance.StartCoroutine(instance.AttackAnimation(instance.player, instance.selectedEnemy));

    }

    IEnumerator AttackAnimation(GameObject attacker, GameObject defender)
    {
        var startPos = attacker.transform.position;
        var endPos = defender.transform.position;
        float totalTime = 0.5f;

        for (float time = 0f; time < totalTime; time += Time.deltaTime)
        {
            var t = time / totalTime;
            t = t * t * t;
            attacker.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return 0;
        }

        for (float time = 0f; time < totalTime; time += Time.deltaTime)
        {
            var t = time / totalTime;
            t = (1 - t);
            t = t * t * t;
            t = (1 - t);

            attacker.transform.position = Vector3.Lerp(endPos, startPos, t);
            yield return 0;
        }
    }

    private static void OnScriptsReloaded()
    {
        foreach (var bm in Object.FindObjectsOfType<BattleTime>())
        {
            bm.Init();
        }
    }

    private static BattleTime _instance;
    public static BattleTime instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<BattleTime>();
            }
            return _instance;
        }
    }

    void Start()
    {    
        Init();
    }

    void Update()
    {
        //turnTime += Time.deltaTime;
        //progressBar.value = turnTime;


        //if (turnTimer >= turnTime)
        //{
        //    progressBar.value = turnTime;
        //    instance.fightButton.enabled = true;
        //}

        //...........................................................................................//

        if (!selectedEnemy)
        {
            selectedEnemy = enemies[0];
            selectionArrow.transform.position = selectedEnemy.transform.position;
        }

        progressTimer += Time.deltaTime;
        progressBar.value = progressTimer;                

        if (progressTimer >= turnTime)
        {
            instance.fightButton.enabled = true;
        }


        if (Input.GetMouseButtonUp(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var mp = new Vector2(mousePos.x, mousePos.y);
            var hit = Physics2D.Raycast(mp, Vector2.zero, enemyLayer);

            if (hit)
            {
                selectedEnemy = hit.collider.gameObject;
                selectionArrow.transform.position = selectedEnemy.transform.position;
            }
        }
    }

    public void Init()
    {
        if (!Application.isPlaying) { return; }

        progressBar.minValue = progressTimer;
        progressBar.maxValue = turnTime;

        if (progressBar.minValue <= 0)
        {
            progressBar.value = progressTimer;           
        }

        fightButton.enabled = false;

        for (int i = 0; i < DataBase.instance.currentBattle.nofEnemies; i ++)
        {
            enemies[i].SetActive(true);
        }
        for(int i = DataBase.instance.currentBattle.nofEnemies; i < enemies.Count; i++)
        {
            enemies[i].SetActive(false);
        }
    }

}
