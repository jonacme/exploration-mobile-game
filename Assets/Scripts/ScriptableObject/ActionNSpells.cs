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

    [SerializeField] int power;
    [SerializeField] int accuracy;
    //[SerializeField] int haste;                  // might change it

}
