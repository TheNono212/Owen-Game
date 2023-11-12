using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HO
{
    public class PlayerAttack : MonoBehaviour
    {
            AnimationHandler animatorHandler;
            PlayerManager playerManager;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimationHandler>();
            playerManager = GetComponentInChildren<PlayerManager>();
        }
        public void HandleLightAttack(WeaponItem weapon)
        {
            //animatorHandler.canRotate = false;
            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
            //StartCoroutine(WaitForRotate(2.0f));            
        }
        public void HandleHeavyAttack(WeaponItem weapon)
        {
            //animatorHandler.canRotate = false;
            animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
            //StartCoroutine(WaitForRotate(2.0f));
        }
        public IEnumerator WaitForRotate(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            animatorHandler.CanRotate();
        }

    }
}