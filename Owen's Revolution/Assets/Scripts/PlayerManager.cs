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
    public bool isSprinting;
    public bool isInteracting;
    public bool isInAir;
    public bool isGrounded;

    private void Awake() => this.cameraHandler = CameraHandler.singleton;

    private void Start()
    {
      this.inputHandler = this.GetComponent<InputHandler>();
      this.anim = this.GetComponentInChildren<Animator>();
      this.playerLocomotion = this.GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
      this.isInteracting = this.anim.GetBool("isInteracting");
      float deltaTime = Time.deltaTime;
      this.inputHandler.TickInput(deltaTime);
      this.playerLocomotion.HandleMovement(deltaTime);
      this.playerLocomotion.HandleRollingAndSprinting(deltaTime);
      this.playerLocomotion.HandleFalling(deltaTime, this.playerLocomotion.moveDirection);
    }

    private void FixedUpdate()
    {
      float deltaTime = Time.deltaTime;
      if (!((Object) this.cameraHandler != (Object) null))
        return;
      this.cameraHandler.FollowTarget(deltaTime);
      this.cameraHandler.HandleCameraRotation(deltaTime, this.inputHandler.mouseX, this.inputHandler.mouseY);
    }

    private void LateUpdate()
    {
      this.inputHandler.rollFlag = false;
      this.inputHandler.sprintFlag = false;
      this.isSprinting = this.inputHandler.leftShift;
      if (this.isInAir)
        this.playerLocomotion.inAirTimer += Time.deltaTime;
      else
        this.playerLocomotion.inAirTimer = 0.0f;
    }
  }
}
