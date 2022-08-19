using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action & Spells", menuName = "Enemy/create new Action & Spells")]
public class ActionNSpells : ScriptableObject
{
    [Header("Enemy Names || Enemy Descriptions")]
    [SerializeField] private string _name;
    [TextArea]
    [SerializeField] private string description;

    [Header("Enemy Types")]
    [SerializeField] private EnemyType enemyType;

    [Header("Enemy Stats")]
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int haste;                  // might change it

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
