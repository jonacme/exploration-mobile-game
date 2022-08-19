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

    [SerializeField] List<learnableMove> learnableMoves;
    

    public string Name
    {
        get{ return enemyName; }
    }                       // properties and not Methods

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

    public List<learnableMove> LearnableMoves 
    {
        get { return learnableMoves; }
    }
}
[System.Serializable]
public class learnableMove
{
    [SerializeField] ActionNSpells actionNSpells;
    [SerializeField] int level;

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
