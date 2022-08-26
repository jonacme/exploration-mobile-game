using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum TurnState
//{
//    // see progress bar fill to get player turn or enemies turn.
//    none,
//    playerTurn,
//    enemyTurn,
//}

public class CombetScript : MonoBehaviour
{
//    public TurnState turnState;    

//    public bool pTurn;
//    public bool enTurn;



    // Start is called before the first frame update
    void Start()
    {
        //BattleTime.instance.fightButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {    
        //BattleTime.instance.turnTime += Time.deltaTime;
        //BattleTime.instance.progressBar.value = BattleTime.instance.turnTime;
        //SelectionState();

        ////switch case / if / else state
        //switch (turnState)
        //{
        //    case TurnState.none:
        //        BattleTime.instance.progressBar.value = BattleTime.instance.turnTimer;
        //        BattleTime.instance.fightButton.enabled = false;
        //        break;
        //    case TurnState.playerTurn:
        //        BattleTime.instance.progressBar.value = BattleTime.instance.turnTime;
        //        BattleTime.Fight();
        //        break;
        //    case TurnState.enemyTurn:
        //        BattleTime.instance.progressBar.value = BattleTime.instance.turnTimer;
        //        BattleTime.instance.fightButton.enabled = false;
        //        break;
        //    // ...
        //    default:
        //        break;
        //}
    }

    public void SelectionState()
    {
        // can us method for each turnstate and maybe check in while loop using boolen.
        // can try use while loop between each turn and maybe use bool and if and else statment

        //IsPlayerTurn();

        //while (turnState == TurnState.none)
        //{
        //    turnState = TurnState.none;
        //    pTurn = false;
        //    BattleTime.instance.progressBar.value = BattleTime.instance.turnTimer;
        //    BattleTime.instance.fightButton.enabled = false;
        //}
        //while (turnState == TurnState.playerTurn)
        //{
        //    turnState = TurnState.playerTurn;
        //    BattleTime.instance.progressBar.value = BattleTime.instance.turnTime;
        //    BattleTime.Fight();
        //}
        //while (turnState == TurnState.playerTurn)
        //{
        //    turnState = TurnState.playerTurn;
        //    pTurn = false;
        //    BattleTime.instance.progressBar.value = BattleTime.instance.turnTimer;
        //    BattleTime.instance.fightButton.enabled = false;
        //}
    }

    //void IsPlayerTurn()
    //{
    //    pTurn = true;
        
    //        turnState = TurnState.playerTurn;
    //        BattleTime.instance.progressBar.value = BattleTime.instance.turnTime;
    //        Debug.Log("PlayerTurn: " + BattleTime.instance.turnTime);
            
    //        BattleTime.Fight();
    //}
    //void IsEnemyTurn()
    //{        
    //    enTurn = true;
    //    pTurn = false;
    //    turnState = TurnState.enemyTurn;
    //    BattleTime.instance.progressBar.value = BattleTime.instance.turnTimer;
    //    Debug.Log("EnemyTurn: " + BattleTime.instance.turnTimer);
    //    BattleTime.instance.fightButton.enabled = false;
     
    //}
}
