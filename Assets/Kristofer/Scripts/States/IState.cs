using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kristofer.exploration
{
    public interface IState
    {

        public void Start() { }

        //public void Chase(EnemyScript e);

        public void Tick(EnemyScript e);

        IState Transition(EnemyScript e);

        public void Exit() { }

    }


}
