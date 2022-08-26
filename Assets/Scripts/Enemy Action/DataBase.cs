using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Battle
{
    public EnemyType type;
    public int nofEnemies;    // nummber of Enemies
}

public class DataBase : MonoBehaviour
{
    public static DataBase _instance;
    public static DataBase instance
    {
        get
        {
            if(!_instance)
            {
                _instance = FindObjectOfType<DataBase>();
            }
            return _instance;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if(instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public Battle currentBattle;

    public static void InitFightScene(Enemy enemy)
    {
        instance.currentBattle = new Battle();
        instance.currentBattle.nofEnemies = Random.Range(1, 3);
        //instance.currentBattle.type = enemy.type;              // enemy.type isnt working;
    }
}
