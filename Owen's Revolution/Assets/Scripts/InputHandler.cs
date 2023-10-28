using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HO
{
  public class InputHandler : MonoBehaviour
  {
    [Header("Dependencies")]
    private PlayerControls inputActions;
    private CameraHandler cameraHandler;
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    public bool leftShift;
    public bool rollFlag;
    public bool sprintFlag;

    public bool isInteracting;


    public float rollInputTimer;
    private Vector2 movementInput;
    private Vector2 cameraInput;

    private void Awake()
    {
      cameraHandler = CameraHandler.singleton;
    }

    private void FixedUpdate()
    {
      float delta = Time.deltaTime;

      if (cameraHandler != null)
      {
        cameraHandler.FollowTarget(delta);
        cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
      }
    }
    public void OnEnable()
    {
      if (inputActions == null)
      {
        inputActions = new PlayerControls();
        inputActions.PlayerMovement.Movement.performed += (Action<InputAction.CallbackContext>) (inputActions => movementInput = inputActions.ReadValue<Vector2>());
        inputActions.PlayerMovement.Camera.performed += (Action<InputAction.CallbackContext>) (i => cameraInput = i.ReadValue<Vector2>());
      }
      inputActions.Enable();
    }

    private void OnDisable() => inputActions.Disable();

    public void TickInput(float delta)
    {
      MoveInput(delta);
      HandleRollInput(delta);
    }

    private void MoveInput(float delta)
    {
      horizontal = movementInput.x;
      vertical = movementInput.y;
      moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
      mouseX = cameraInput.x;
      mouseY = cameraInput.y;
    }

    private void HandleRollInput(float delta)
    {
      leftShift = inputActions.PlayerActions.Roll.phase == InputActionPhase.Performed;
      if (leftShift)
      {
        rollInputTimer += delta;
        sprintFlag = true;
                //serait ce mieux si on attendait genre 0.2 sec avant de pouvoir courir (comme ça ça ne cours et roulade en meme tant)
      }
      else
      {
        if (rollInputTimer > 0.0 && rollInputTimer < 0.5)
        {
          sprintFlag = false;
          rollFlag = true;
        }
        rollInputTimer = 0.0f;
                sprintFlag = false;
      }
    }
  }
}
