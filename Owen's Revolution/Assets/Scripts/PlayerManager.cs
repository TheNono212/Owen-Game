using System.Collections;
using System.Collections.Generic;
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
    private PlayerStats playerStats;
    private EnemyStats enemyStats;

    public bool isInteracting;

    public Collectable collectable;

    [Header("Player Flags")]
    public bool isRolling;
    public bool isSprinting;

    //public bool isInAir;
    //public bool isGrounded;

    private void Awake()
    { 
      cameraHandler = FindObjectOfType<CameraHandler>();
      // need to put that in start if a LOT of object in scene and cause lag
    }

    private void Start()
    {
      inputHandler = GetComponent<InputHandler>();
      anim = GetComponentInChildren<Animator>();
      playerLocomotion = GetComponent<PlayerLocomotion>();
      playerStats = GetComponent<PlayerStats>();
      collectable = GetComponent<Collectable>();
    }

    private void Update()
    {
      float delta = Time.deltaTime;

      isInteracting = anim.GetBool("isInteracting");
      
      inputHandler.TickInput(delta);
      playerLocomotion.HandleMovement(delta);
      playerLocomotion.HandleRollingAndSprinting(delta);
      //playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
      playerLocomotion.HandleJumping();
      collectable.IsContact();


      isSprinting = inputHandler.sprintFlag;
      isRolling = inputHandler.rollFlag;
    }

    public void SavePlayer()
    {
      SaveSystem.SavePlayer(playerStats, playerLocomotion, enemyStats);
    }
    public void LoadPlayer()
    {
      PlayerData data = SaveSystem.LoadPlayer();

      playerStats.currentHealth = data.health;

      Vector3 position;
      position.x = data.position[0];
      position.y = data.position[1];
      position.z = data.position[2];
      playerLocomotion.transform.position = position;

      //enemyStats currentHea lth = data.enemyHealth;
      //enemyStats isDead = data.enemyIsDead;

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
      inputHandler.rb_Input = false;
      inputHandler.rt_Input = false;
      inputHandler.jump_Input = false;
      inputHandler.interact_Input = false;


      if(inputHandler.moveAmount > 1)
      {
        isSprinting = inputHandler.b_Input;
      }
      
      //if (isInAir)
      //  playerLocomotion.inAirTimer += Time.deltaTime;
      //else
      //  playerLocomotion.inAirTimer = 0.0f;
    }
  }
}
