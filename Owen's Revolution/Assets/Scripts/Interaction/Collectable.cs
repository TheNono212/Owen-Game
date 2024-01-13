using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public string[] collectableTypes = {"Berries"};
    //public Item[] itemToPickup;
    public bool isTriggered = false;
    public bool canCollect = false;
    public string customTag;
    public int berries = 0;
    public HO.InputHandler inputHandler;
    public HO.AnimationHandler animationHandler;
    public HO.UIManager uiManager;
    private void Start() {
        inputHandler = GetComponent<HO.InputHandler>();
        animationHandler = GetComponentInChildren<HO.AnimationHandler>();
        uiManager = GetComponent<HO.UIManager>();
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
                    if(inputHandler.interact_Input && isTriggered == true)
                    {
                        //if(playerManager.isInteracting) => return; ????
                        animationHandler.PlayTargetAnimation("Collect", true);
                        berries += 1;
                        //pass the item to the function
                        //inventoryManager.AddItem(itemToPickup[0]);
                        uiManager.NewItem(1, "Berries");
                    }
                    break;
                default:
                    return;
                    break;
            }
    }
}
