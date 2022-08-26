using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Kristofer.exploration
{
    public class Loader : MonoBehaviour
    {
     
        public bool useClass;

        private static Loader instance;
        public static Loader Instance
        {
            get
            {
                if (!instance)
                {

                    instance = FindObjectOfType<Loader>();

                }
                return instance;
            }


        }


        private void Start()
        {
            if (Instance != this)
            {

                Destroy(this.gameObject);
            }
            else
            {

                DontDestroyOnLoad(this.gameObject);
            }
        }

    }



}

