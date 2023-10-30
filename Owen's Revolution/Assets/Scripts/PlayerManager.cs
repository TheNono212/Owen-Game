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

    public bool isInteracting;

    [Header("Player Flags")]
    public bool isRolling;
    public bool isSprinting;

    //public bool isInAir;
    //public bool isGrounded;

    private void Awake()
    { 
      cameraHandler = CameraHandler.singleton;
    }

    private void Start()
    {
      inputHandler = GetComponent<InputHandler>();
      anim = GetComponentInChildren<Animator>();
      playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
      float delta = Time.deltaTime;

      isInteracting = anim.GetBool("isInteracting");
      
      inputHandler.TickInput(delta);
      playerLocomotion.HandleMovement(delta);
      playerLocomotion.HandleRollingAndSprinting(delta);
      //playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);


      isSprinting = inputHandler.sprintFlag;
      isRolling = inputHandler.rollFlag;
    }

    private void FixedUpdate()
    {
      float delta = Time.deltaTime;

      if (cameraHandler != null)
      {
        cameraHandler.FollowTarget(delta);
        cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
      }
    }

    private void LateUpdate()
    {
      inputHandler.rollFlag = false;
      inputHandler.sprintFlag = false;
      if(inputHandler.moveAmount > 1)
      {
        isSprinting = inputHandler.leftShift;
      }
      
      //if (isInAir)
      //  playerLocomotion.inAirTimer += Time.delta;
      //else
      //  playerLocomotion.inAirTimer = 0.0f;
    }
  }
}
