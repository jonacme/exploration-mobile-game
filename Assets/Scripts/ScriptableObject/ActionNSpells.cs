using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action & Spells", menuName = "Enemy/create new Action & Spells")]
public class ActionNSpells : ScriptableObject
{
    [Header("Enemy Names || Enemy Descriptions")]
    [SerializeField] private string _name;                         // adding name of the skill
    [TextArea]
    [SerializeField] private string description;                   // adding description of the skill

    [Header("Enemy Types")]
    [SerializeField] private EnemyType enemyType;                  // the enemy type which can use this skill

    [Header("Enemy Stats")]                                        // might not need this since enemy stats exits in enemyBase scripts
    [SerializeField] int power;                                    // and how much damage it should do to the player
    [SerializeField] int accuracy;
    [SerializeField] int haste;                                    // might change it

    public string Name
    {
        get { return _name; }
    }
    public string Description
    {
        get { return description; }
    }
    public EnemyType EnemyType
    {
        get { return enemyType; }
    }

    public int Power
    {
        get { return power; }
    }
    public int Accuracy
    {
        get { return accuracy; }
    }
    public int Haste
    {
        get { return haste; } 
    }

}
