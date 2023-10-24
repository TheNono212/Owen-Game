using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HO
{
  public class AnimationHandler : MonoBehaviour
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
    public float rollInputTimer;
    private Vector2 movementInput;
    private Vector2 cameraInput;

    public void OnEnable()
    {
      if (this.inputActions == null)
      {
        this.inputActions = new PlayerControls();
        this.inputActions.PlayerMovement.Movement.performed += (Action<InputAction.CallbackContext>) (inputActions => this.movementInput = inputActions.ReadValue<Vector2>());
        this.inputActions.PlayerMovement.Camera.performed += (Action<InputAction.CallbackContext>) (i => this.cameraInput = i.ReadValue<Vector2>());
      }
      this.inputActions.Enable();
    }

    private void OnDisable() => this.inputActions.Disable();

    public void TickInput(float delta)
    {
      this.MoveInput(delta);
      this.HandleRollInput(delta);
    }

    private void MoveInput(float delta)
    {
      this.horizontal = this.movementInput.x;
      this.vertical = this.movementInput.y;
      this.moveAmount = Mathf.Clamp01(Mathf.Abs(this.horizontal) + Mathf.Abs(this.vertical));
      this.mouseX = this.cameraInput.x;
      this.mouseY = this.cameraInput.y;
    }

    private void HandleRollInput(float delta)
    {
      this.leftShift = this.inputActions.PlayerActions.Roll.phase == InputActionPhase.Performed;
      if (this.leftShift)
      {
        this.rollInputTimer += delta;
        this.sprintFlag = true;
      }
      else
      {
        if ((double) this.rollInputTimer > 0.0 && (double) this.rollInputTimer < 0.5)
        {
          this.sprintFlag = false;
          this.rollFlag = true;
        }
        this.rollInputTimer = 0.0f;
      }
    }
  }
}
