using UnityEngine;

namespace HO
{
  public class PlayerManager : MonoBehaviour
  {
    [Header("Dependencies")]
    private InputHandler inputHandler;
    private PlayerLocomotion playerLocomotion;
    private Animator anim;
    private CameraHandler cameraHandler;


        [Header("Player Flags")]
        public bool isRolling;
        public bool isSprinting;
    //public bool isInAir;
    //public bool isGrounded;

    private void Awake() => cameraHandler = CameraHandler.singleton;

    private void Start()
    {
      inputHandler = GetComponent<InputHandler>();
      anim = GetComponentInChildren<Animator>();
      playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
            if (inputHandler.moveAmount < 1)
            {
                isSprinting = false;
            }
            inputHandler.isInteracting = anim.GetBool("isInteracting");
      float deltaTime = Time.deltaTime;
      inputHandler.TickInput(deltaTime);

      playerLocomotion.HandleMovement(deltaTime);
      playerLocomotion.HandleRollingAndSprinting(deltaTime);
            //playerLocomotion.HandleFalling(deltaTime, playerLocomotion.moveDirection);

            isSprinting = inputHandler.sprintFlag;
            isRolling = inputHandler.rollFlag;
    }

    private void FixedUpdate()
    {
      float deltaTime = Time.deltaTime;
      if (cameraHandler == null)
        return;
      cameraHandler.FollowTarget(deltaTime);
      cameraHandler.HandleCameraRotation(deltaTime, inputHandler.mouseX, inputHandler.mouseY);
    }

        private void LateUpdate()
        {
            //inputHandler.rollFlag = false;
            //inputHandler.sprintFlag = false;


      //if (isInAir)
      //  playerLocomotion.inAirTimer += Time.deltaTime;
      //else
      //  playerLocomotion.inAirTimer = 0.0f;
    }
  }
}
