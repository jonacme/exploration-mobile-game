using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Enemy", menuName = "Enemy/create new Enemy")]
public class EnemyBase : ScriptableObject
{
    [Header("Enemy Names || Enemy Descriptions")]
    [SerializeField] private string enemyName;
    [TextArea]
    [SerializeField] private string description;
    [Header("Enemy Types")]
    [SerializeField] private EnemyType enemyType;
    [Header("Enemy Sprite")]
    [SerializeField] Sprite sprite;


    [Header("Enemy Stats")]
    [SerializeField] int level;
    [SerializeField] int maxHp;
    [SerializeField] int specialAttack;
    [SerializeField] int attack;
    [SerializeField] int magicAttack;
    [SerializeField] int defense;
    [SerializeField] int magicDefense;

    [SerializeField] List<specialEnemyMove> specialEnemyMove;


    // properties and not Methods
    public string Name
    {
        get{ return enemyName; }
    }                                             

    public string Description
    {
        get { return description; }
    }

    public int Level
    {
        get { return level; }
    }

    public int MaxHP
    {
        get { return maxHp; }
    }

    public int SpecialAttack
    {
        get { return specialAttack; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int MagicAttack
    {
        get { return magicAttack; }
    }

    public int Defense
    {
        get { return defense; }
    }
    public int MagicDefense
    {
        get { return magicDefense; }
    }    

    public List<specialEnemyMove> SpecialEnemyMove 
    {
        get { return specialEnemyMove; }
    }
}
[System.Serializable]
public class specialEnemyMove
{
    [SerializeField] ActionNSpells actionNSpells;   
    [SerializeField] int level;                           // use the move if low enough HP

    public ActionNSpells ActionNSpells
    {
        get { return actionNSpells; }
    }
    public int Level
    {
        get { return level; }
    }
}

public enum EnemyType
{
    FloatingEnemy,
    GroundEnemy,
    Dragon,
    Robots,
    Ghost
}
