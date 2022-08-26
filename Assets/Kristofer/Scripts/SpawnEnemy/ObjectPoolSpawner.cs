using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kristofer.exploration
{
    public class ObjectPoolSpawner : MonoBehaviour
    {
        public float timeToSpawn = 5f;
        private float timeSinceSpawned;
        private Objectpool pool;

        private void Start()
        {
            pool = FindObjectOfType<Objectpool>();
        }

        private void Update()
        {
            timeSinceSpawned += Time.deltaTime;
            if(timeSinceSpawned >= timeToSpawn)
            {

                GameObject spawnNewEnemy = pool.SpawnEnemy();
                spawnNewEnemy.transform.position = this.transform.position;
                timeSinceSpawned = 0f;
            }


        }
    }

}
