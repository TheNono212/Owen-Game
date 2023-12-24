using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HO 
{
    public class EnemyAnimationManager : AnimationManager
    {

        EnemyLocomotionManager enemyLocomotionManager;
        private void Awake() {
            anim = GetComponent<Animator>();
            enemyLocomotionManager = GetComponentInParent<EnemyLocomotionManager>();
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyLocomotionManager.enemyRigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyLocomotionManager.enemyRigidbody.velocity = velocity;
        }
    }
}