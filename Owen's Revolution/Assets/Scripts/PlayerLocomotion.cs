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

    private void Start()
    {
      playerManager = GetComponent<PlayerManager>();
      rigidbody = GetComponent<Rigidbody>();
      inputHandler = GetComponent<InputHandler>();
      animatorHandler = GetComponentInChildren<AnimationHandler>();
      cameraObject = Camera.main.transform;
      myTransform = transform;
      //animatorHandler.Initialize();
      playerManager.isGrounded = true;
      //ignoreForGroundCheck = (LayerMask) -2305;
    }

    public void Update()
    {
      float delta = inAirTimer;

      inputHandler.TickInput(delta);

      moveDirection = cameraObject.forward * inputHandler.vertical;
      moveDirection += cameraObject.right * inputHandler.horizontal;
      moveDirection.Normalize();

      float speed = movementSpeed;
      moveDirection *= speed;
    }
    
    #region Movement
    Vector3 normalVector;
    Vector3 targetPosition;

    private void HandleRotation(float delta)
    {

      Vector3 targetDir = Vector3.zero; 
      float moveOverride = inputHandler.moveAmount;

      targetDir = cameraObject.forward * inputHandler.vertical;
      targetDir += cameraObject.right * inputHandler.horizontal;

      targetDir.Normalize();
      targetDir.y = 0.0f;

      if (targetDir == Vector3.zero)
      {
         targetDir = myTransform.targetDir;
      }
      
      float rs = rotationSpeed;

      Quaternion tr = Quaternion.LookRotation(targetDir);
      Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);
      
      myTransform.rotation = targetRotation;
    }
    
    public void HandleMovement(float delta)
    {
      if (inputHandler.rollFlag)
        return;
      moveDirection = cameraObject.forward * this.inputHandler.vertical;
      moveDirection += cameraObject.right * this.inputHandler.horizontal;
      moveDirection.Normalize();
      moveDirection.y = 0.0f;
      float movementSpeed = thisovementSpeed;
      if (inputHandler.sprintFlag)
        moveDirection *= this.sprintSpeed;
      else
        moveDirection *= movementSpeed;
      rigidbody.velocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
      animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0.0f, playerManager.isSprinting);
      if (!animatorHandler.canRotate)
        return;
      HandleRotation(delta);
    }

    public void HandleRollingAndSprinting(float delta)
    {
      if (animatorHandler.anim.GetBool("isInteracting") || !inputHandler.rollFlag)
        return;
      moveDirection = cameraObject.forward * inputHandler.vertical;
      moveDirection += cameraObject.right * inputHandler.horizontal;
      if (inputHandler.moveAmount > 0.0)
      {
        animatorHandler.PlayTargetAnimation("Rolling", true);
        moveDirection.y = 0.0f;
        myTransform.rotation = Quaternion.LookRotation(moveDirection);
      }
      else
        animatorHandler.PlayTargetAnimation("Backstep", true);
    }
    #endregion
  }
}