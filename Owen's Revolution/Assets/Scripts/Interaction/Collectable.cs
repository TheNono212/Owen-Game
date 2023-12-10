using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HO
{
    public class Collectable : MonoBehaviour
    {
        public string[] collectableTypes = {"Berries"};
        public bool isTriggered = false;
        public bool canCollect = false;

        public string customTag;
        public int berries = 0;

        public InputHandler inputHandler;
        public AnimationHandler animationHandler;
        public UIManager uiManager;


        private void Start() {
            inputHandler = GetComponent<InputHandler>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
            uiManager = GetComponent<UIManager>();
        }

        private void OnTriggerStay(Collider other) {
            if(other.tag != "Collectable")
                return;
            
            CustomTag obj = other.GetComponent<CustomTag>();
            
            customTag = obj.customTag;

            isTriggered = true;
            IsContact();
        }

        private void OnTriggerExit(Collider other) {
            if(other.tag != "Collectable")
                return;
            isTriggered = false;
        }

        public void IsContact()
        {
            switch (customTag)
            {
                case "Berries":
                    if(inputHandler.interact_Input)
                    {
                        //if(playerManager.isInteracting) => return; ????
                        animationHandler.PlayTargetAnimation("Collect", true);
                        berries += 1;
                        uiManager.NewItem(1, "Berries");
                    }
                    break;
                default:
                    return;
                    break;
            }
        }
    }
}