using UnityEngine;

namespace HO
{
  public class PlayerLocomotion : MonoBehaviour
  {
    [Header("Dependencies")]
    private PlayerManager playerManager;
    private Transform cameraObject;
    private InputHandler inputHandler;
    public Vector3 moveDirection;
    public Rigidbody rigidbody;
    public GameObject normalCamera;
    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public AnimationHandler animatorHandler;
    [Header("Movement Stats")]
    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    private float sprintSpeed = 8f;
    [SerializeField]
    private float fallingSpeed = 4f;
    [Header("Ground & Air Detection Stats")]
    [SerializeField]
    private float groundDetectionRayStartPoint = 0.5f;
    [SerializeField]
    private float minimumDistanceNeededToBeginFall = 0.1f;
    [SerializeField]
    private float groundDirectionRayDistance = -0.2f;
    private LayerMask ignoreForGroundCheck;
    public float inAirTimer;
    private Vector3 normalVector;
    private Vector3 targetPosition;

    private void Start()
    {
      this.playerManager = this.GetComponent<PlayerManager>();
      this.rigidbody = this.GetComponent<Rigidbody>();
      this.inputHandler = this.GetComponent<InputHandler>();
      this.animatorHandler = this.GetComponentInChildren<AnimationHandler>();
      this.cameraObject = Camera.main.transform;
      this.myTransform = this.transform;
      this.animatorHandler.Initialize();
      this.playerManager.isGrounded = true;
      this.ignoreForGroundCheck = (LayerMask) -2305;
    }

    private void HandleRotation(float delta)
    {
      Vector3 zero = Vector3.zero;
      double moveAmount = (double) this.inputHandler.moveAmount;
      Vector3 forward = this.cameraObject.forward * this.inputHandler.vertical + this.cameraObject.right * this.inputHandler.horizontal;
      forward.Normalize();
      forward.y = 0.0f;
      if (forward == Vector3.zero)
        forward = this.myTransform.forward;
      float rotationSpeed = this.rotationSpeed;
      this.myTransform.rotation = Quaternion.Slerp(this.myTransform.rotation, Quaternion.LookRotation(forward), rotationSpeed * delta);
    }

    public void HandleMovement(float delta)
    {
      if (this.inputHandler.rollFlag)
        return;
      this.moveDirection = this.cameraObject.forward * this.inputHandler.vertical;
      this.moveDirection += this.cameraObject.right * this.inputHandler.horizontal;
      this.moveDirection.Normalize();
      this.moveDirection.y = 0.0f;
      float movementSpeed = this.movementSpeed;
      if (this.inputHandler.sprintFlag)
        this.moveDirection *= this.sprintSpeed;
      else
        this.moveDirection *= movementSpeed;
      this.rigidbody.velocity = Vector3.ProjectOnPlane(this.moveDirection, this.normalVector);
      this.animatorHandler.UpdateAnimatorValues(this.inputHandler.moveAmount, 0.0f, this.playerManager.isSprinting);
      if (!this.animatorHandler.canRotate)
        return;
      this.HandleRotation(delta);
    }

    public void HandleRollingAndSprinting(float delta)
    {
      if (this.animatorHandler.anim.GetBool("isInteracting") || !this.inputHandler.rollFlag)
        return;
      this.moveDirection = this.cameraObject.forward * this.inputHandler.vertical;
      this.moveDirection += this.cameraObject.right * this.inputHandler.horizontal;
      if ((double) this.inputHandler.moveAmount > 0.0)
      {
        this.animatorHandler.PlayTargetAnimation("Rolling", true);
        this.moveDirection.y = 0.0f;
        this.myTransform.rotation = Quaternion.LookRotation(this.moveDirection);
      }
      else
        this.animatorHandler.PlayTargetAnimation("Backstep", true);
    }

    public void HandleFalling(float delta, Vector3 moveDirection)
    {
      this.playerManager.isGrounded = false;
      Vector3 position = this.myTransform.position;
      position.y += this.groundDetectionRayStartPoint;
      RaycastHit hitInfo;
      if (Physics.Raycast(position, this.myTransform.forward, out hitInfo, 0.4f))
        moveDirection = Vector3.zero;
      if (this.playerManager.isInAir)
      {
        this.rigidbody.AddForce(-Vector3.up * this.fallingSpeed);
        this.rigidbody.AddForce(moveDirection * this.fallingSpeed / 5f);
      }
      Vector3 vector3_1 = moveDirection;
      vector3_1.Normalize();
      Vector3 vector3_2 = position + vector3_1 / this.groundDirectionRayDistance;
      this.targetPosition = this.myTransform.position;
      Debug.DrawRay(vector3_2, -Vector3.up * this.minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
      if (Physics.Raycast(vector3_2, -Vector3.up, out hitInfo, this.minimumDistanceNeededToBeginFall, (int) this.ignoreForGroundCheck))
      {
        this.normalVector = hitInfo.normal;
        Vector3 point = hitInfo.point;
        this.playerManager.isGrounded = true;
        this.targetPosition.y = point.y;
        if (this.playerManager.isInAir)
        {
          if ((double) this.inAirTimer > 0.5)
          {
            Debug.Log((object) ("You were in the air for " + this.inAirTimer.ToString()));
            this.animatorHandler.PlayTargetAnimation("Land", true);
          }
          else
          {
            this.animatorHandler.PlayTargetAnimation("Locomotion", false);
            this.inAirTimer = 0.0f;
          }
          this.playerManager.isInAir = false;
        }
      }
      else
      {
        if (this.playerManager.isGrounded)
          this.playerManager.isGrounded = false;
        if (!this.playerManager.isInAir)
        {
          if (!this.playerManager.isInteracting)
            this.animatorHandler.PlayTargetAnimation("Falling", true);
          Vector3 velocity = this.rigidbody.velocity;
          velocity.Normalize();
          this.rigidbody.velocity = velocity / (this.movementSpeed / 2f);
          this.playerManager.isInAir = true;
        }
      }
      if (!this.playerManager.isGrounded)
        return;
      if (this.playerManager.isInteracting || (double) this.inputHandler.moveAmount > 0.0)
        this.myTransform.position = Vector3.Lerp(this.myTransform.position, this.targetPosition, Time.deltaTime);
      else
        this.myTransform.position = this.targetPosition;
    }
  }
}
