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
    [SerializeField] int physicalAttack;
    [SerializeField] int magicalAttack;
    [SerializeField] int physicalDefense;
    [SerializeField] int magicalDefense;

    public string GetName()
    {
        return enemyName;
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
