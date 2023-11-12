using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HO
{
  public class InputHandler : MonoBehaviour
  {
    [Header("Dependencies")]
    PlayerControls inputActions;
    PlayerAttack playerAttack;

    PlayerInventory playerInventory;


    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    public bool b_Input;
    public bool rb_Input;
    public bool rt_Input;
    public bool jump_Input;


    public bool rollFlag;
    public bool sprintFlag;


    public float rollInputTimer;
    private Vector2 movementInput;
    private Vector2 cameraInput;

    private void Awake()
    {
      playerAttack = GetComponent<PlayerAttack>();
      playerInventory = GetComponent<PlayerInventory>();
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

    private void OnDisable()
    {
      inputActions.Disable();
    }

    public void TickInput(float delta)
    {
      MoveInput(delta);
      HandleRollInput(delta);
      HandleAttackInput(delta);
      HandleJumpInput(delta);
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
      b_Input = inputActions.PlayerActions.Roll.phase == InputActionPhase.Performed;
      if (b_Input)
      {
        rollInputTimer += delta;
        if(moveAmount < 1)
          return;
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

    private void HandleAttackInput(float delta)
    {
      inputActions.PlayerActions.RB.performed += i => rb_Input = true;
      inputActions.PlayerActions.RT.performed += i => rt_Input = true;

      if(rb_Input)
      {
        playerAttack.HandleLightAttack(playerInventory.rightWeapon);
      }
      if(rt_Input)
      {
        playerAttack.HandleHeavyAttack(playerInventory.rightWeapon);
      }
    }

    private void HandleJumpInput(float delta)
    {
      inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
    }
  }
}
