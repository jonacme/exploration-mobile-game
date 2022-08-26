using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Kristofer.exploration
{
    public class SwitchToggle : MonoBehaviour
    {
        public RectTransform uiHandle;
        
        public Toggle toggle;

        private Vector2 handlePosition;

        private void Awake()
        {
            toggle = GetComponent<Toggle>();

            handlePosition = uiHandle.anchoredPosition;

            toggle.onValueChanged.AddListener(OnSwitch);
        }


        void OnSwitch(bool isToggled)
        {
            if(isToggled)
            {
                uiHandle.anchoredPosition = handlePosition * -1;
                
            }
            else
            {
                uiHandle.anchoredPosition = handlePosition;
            }


        }


        private void OnDestroy()
        {
            toggle.onValueChanged.AddListener(OnSwitch);
        }



    }
    
}
