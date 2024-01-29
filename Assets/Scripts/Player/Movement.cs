using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject Player;
    public float MoveSpeed = 5f;
    public float JumpForce = 20f;

    private CustomInput input;
    private Rigidbody2D playerRigidbody2D;
    private Vector2 moveVector = Vector2.zero;
    private Animator animator;
    private SpriteRenderer playerSprite;

    public bool isGrounded = false;

    void Awake()
    {
        input = new CustomInput();
        playerRigidbody2D = Player.GetComponent<Rigidbody2D>();
        animator = Player.GetComponent<Animator>();
        playerSprite = Player.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += Movement_performed;
        input.Player.Movement.canceled += Movement_canceled;
        input.Player.Jumping.performed += Jump;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= Movement_performed;
        input.Player.Movement.canceled -= Movement_canceled;
    }

    private void Update()
    {
        RotatePlayerToMouseCursor();
        ChangePlayerAnimation();
    }

    private void FixedUpdate()
    {
        playerRigidbody2D.AddRelativeForce(moveVector * MoveSpeed);
    }
    private void ChangePlayerAnimation()
    {
        float playerSpeed = Math.Abs(playerRigidbody2D.velocity.x);

        if (playerSpeed < 0.1f && moveVector == Vector2.zero)
        {
            SetAnimation(AnimationState.Idle);
        }
        else
        {
            //if (Time.time - timeSinceLastFootstep >= UnityEngine.Random.Range(minTimeBetweenFootsteps, maxTimeBetweenFootsteps) && isGrounded)
            //{

            //    AudioManager.Instance.PlayOneShot(AudioManager.Instance.moveSound, transform.position);

            //    timeSinceLastFootstep = Time.time; 
            //}
            if (moveVector == Vector2.right)
            {
                if (playerSprite.flipX)
                {
                    SetAnimation(AnimationState.MovingForward, playerSpeed);
                }
                else
                {
                    SetAnimation(AnimationState.MovingBackward, playerSpeed);
                }
            }
            else if (moveVector == Vector2.left)
            {
                if (!playerSprite.flipX)
                {
                    SetAnimation(AnimationState.MovingForward, playerSpeed);
                }
                else
                {
                    SetAnimation(AnimationState.MovingBackward, playerSpeed);
                }
            }
        }
    }

    private void SetAnimation(AnimationState state, float animSpeed = 1)
    {
        string animName = AnimationsList[state];
        if (animName == animator.GetCurrentAnimatorClipInfo(0).FirstOrDefault().clip.name)
        {
            return;
        }

        animator.Play(animName);

        double newAnimSpeed = animSpeed;
        if (newAnimSpeed >= 2)
        {
            newAnimSpeed = 2;
        }
        else if (newAnimSpeed < .7f)
        {
            newAnimSpeed = .7f;
        }
        animator.speed = (float)newAnimSpeed;
    }

    private void Movement_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveVector = obj.ReadValue<Vector2>();
    }

    private void Movement_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveVector = Vector2.zero;
    }

    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        playerRigidbody2D.AddForce(
            Vector2.up * JumpForce,
            ForceMode2D.Force);
        AudioManager.Instance.PlayOneShot(AudioManager.Instance.jumpTeeth, transform.position);
        isGrounded = false;
    }


    private void RotatePlayerToMouseCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPosition = Player.transform.position;

        playerSprite.flipX = mousePosition.x > playerPosition.x;
    }

    private readonly Dictionary<AnimationState, string> AnimationsList = new() {
        { AnimationState.Idle, "PlayerStanding" },
        { AnimationState.MovingForward, "PlayerMovingForward" },
        { AnimationState.MovingBackward, "PlayerMovingBackward" }
    };

    private enum AnimationState
    {
        Idle = 0,
        MovingForward = 1,
        MovingBackward = 2
    }
}
