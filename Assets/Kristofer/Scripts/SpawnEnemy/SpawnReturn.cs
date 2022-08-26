using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kristofer.exploration
{
    public class SpawnReturn : MonoBehaviour
    {
        private Objectpool objectPool;

        private void Start()
        {
            objectPool = FindObjectOfType<Objectpool>();


        }

        private void OnDisable()
        {
            if(objectPool != null)
            {

                objectPool.ReturnEnemy(this.gameObject);
            }
        }


    }

}
