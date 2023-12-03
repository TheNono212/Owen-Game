using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HO 
{
    public class Fire : MonoBehaviour
    {

        public float remainBurnTime;
        public float setBurnTime = 5f;
        public bool isBurning;
        public bool isCollidingWithFire;
        public PlayerStats playerStats;

        public AnimationHandler animationHandler;


        private void Start() {
            animationHandler = GetComponentInChildren<AnimationHandler>();
            remainBurnTime = setBurnTime;
        }


        public void FireDamage(bool isInFire)
        {
            if(isInFire)
            {
                isCollidingWithFire = true;
            }
            if(isCollidingWithFire)
            {
                StartCoroutine(WaitForCollision());
            }
            if(!isInFire)
            {
                isCollidingWithFire = false;
            }
        }

        public IEnumerator WaitForCollision()
        {
            yield return new WaitForSeconds(1f);
            //Debug.Log("Waited 1sec");
            if(!isCollidingWithFire)
            {
                Debug.Log("isColldingWithFire = false");
                remainBurnTime = setBurnTime;
                StartCoroutine(BurnDamage());
                Debug.Log("Started BurnDamage()");
            }
            else{
                yield return new WaitForSeconds(1f);
            //    Debug.Log("Waited ANOTHER second");
        }
        }

        public IEnumerator BurnDamage()
        {   
            Debug.Log("le remainBurnTine = " + remainBurnTime);
            while(remainBurnTime > 1)
            {
                yield return new WaitForSeconds(2f);
                playerStats.currentHealth -= 10;
                animationHandler.PlayTargetAnimation("Damage_01", true);
                remainBurnTime -= 1;
                //Debug.Log("removed health, waited 1sec and removed burntime");
                Debug.Log("Il reste " + remainBurnTime + " secondes de brulures.");
            }
            Debug.Log("no more burntime");
        }
    }
}