using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy 
{
    public EnemyBase enemyBase;
    int level;

    public int HP { get; set; }

    public List<Move>Moves { get; set; }


    public Enemy(EnemyBase eBase, int eLevel)         // eBase "enemyBase"   eLevel " enemy Level"
    {
        enemyBase = eBase;
        level = eLevel;
        HP = enemyBase.MaxHP;

        Moves = new List<Move>();                      // most like is for player
        foreach ( var move in enemyBase.SpecialEnemyMove)
        {
            if(move.Level <= level)
            {
                Moves.Add(new Move(move.ActionNSpells));
            }
            //if(Moves.Count >= 4)               // not useable for this game
            //{
            //    break;
            //}
        }
    }

    public int MaxHP
    {
        get { return Mathf.FloorToInt((enemyBase.MaxHP * level) / 100f) + 10; }
    }
    public int SpecialAttack
    {
        get { return Mathf.FloorToInt((enemyBase.SpecialAttack * level) / 100f) + 5; }
    }
    public int Attack
    {
        get { return Mathf.FloorToInt((enemyBase.Attack * level) / 100f) + 5; }
    }
    public int MagicAttack
    {
        get { return Mathf.FloorToInt((enemyBase.MagicAttack * level) / 100f) + 5; }
    }
    public int Defense
    {
        get { return Mathf.FloorToInt((enemyBase.Defense * level) / 100f) + 5; }
    }
    public int MagicDefense
    {
        get { return Mathf.FloorToInt((enemyBase.MagicDefense * level) / 100f) + 5; }
    }
}
