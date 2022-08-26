using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kristofer.exploration
{
    public class ChasePlayer : MonoBehaviour, IState
    {

        public void Tick(EnemyScript e)
        {
            
        }

        public IState Transition(EnemyScript e)
        {
            return null;
        }
    }


}
