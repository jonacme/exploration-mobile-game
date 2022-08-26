using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kristofer.exploration
{
    public class CameraFollow : MonoBehaviour
    {

        public float followSpeed = 2f;
        public float offset = 1f;
        public Transform player;


        private void Update()
        {
            Vector3 newPosition = new Vector3(player.position.x, player.position.y + offset, -10f);
            transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
        }


    }

}
