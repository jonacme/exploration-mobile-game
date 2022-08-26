using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTime : MonoBehaviour
{
    [Header("Player Turn")]
    public float turnTimer = 0f;     // not progressed yet
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

        if (instance.turnTimer >= instance.turnTime)
        {
            instance.turnTime = 0f;
        }
        instance.fightButton.enabled = true;
        Debug.Log("FightButton Pressed");

        instance.StartCoroutine(instance.AttackAnimation());

    }

    IEnumerator AttackAnimation()
    {
        Debug.Log("Attack Button");
        yield break;
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
        turnTime += Time.deltaTime;
        progressBar.value = turnTime;


        if (turnTimer >= turnTime)
        {
            progressBar.value = turnTime;
            instance.fightButton.enabled = true;
        }
    }

    public void Init()
    {
        if (!Application.isPlaying) { return; }

        progressBar.value = 0;

        if (progressBar.minValue <= 0)
        {
            progressBar.value = turnTimer;
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
