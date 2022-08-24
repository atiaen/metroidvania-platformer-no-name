using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public SpriteRenderer player;
    public float movementSpeed;
    public float movementVelocity;
    public float jumpHeight;
    public float jumpVelocity;
    public bool canMove;
    public bool canJump;

    public bool isHit;

    bool jump = false, jumpHeld = false;

    [Range(0, 5f)][SerializeField] private float fallLongMult = 0.85f;
    [Range(0, 5f)][SerializeField] private float fallShortMult = 1.55f;

    public Transform groundCheck;
    public float groundDistance;
    private BoxCollider2D coll;
    public LayerMask groundMask;
    public float knockDistance;

    public static event Action<float> walkEvent;
    public static event Action<bool> jumpEvent;
    public static event Action attackEvent;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        player = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !isHit)
        {
            HandleInput();
        }
    }
    void FixedUpdate()
    {
        if (canJump && !isHit)
        {
            if (jump)
            {

                playerBody.velocity = Vector2.up * jumpHeight;
                jump = false;
            }
            if (jumpHeld && playerBody.velocity.y > 0)
            {
                playerBody.velocity += Vector2.up * Physics2D.gravity.y * (fallLongMult - 1) * Time.fixedDeltaTime;
            }
            // Jumping Low...
            else if (!jumpHeld && playerBody.velocity.y > 0)
            {
                playerBody.velocity += Vector2.up * Physics2D.gravity.y * (fallShortMult - 1) * Time.fixedDeltaTime;
            }
        }

    }

    public static IEnumerator KnockBack(Vector2 direction,float knockDistance,Rigidbody2D playerBody,PlayerMovement player)
    {
        player.isHit = true;
        playerBody.AddForce(direction * knockDistance, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        player.isHit = false;

    }

    public void Reset(){
        canMove = true;
        canJump = true;
        isHit = false;
    }
    public void HandleInput()
    {
        // bool IsGrounded() = Physics2D.Raycast(groundCheck.position, -groundCheck.up, groundDistance, groundMask);
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 movement;
        movementVelocity = 0;
        if (horizontal > 0)
        {
            player.flipX = false;
            movementVelocity = movementSpeed * horizontal;

            if (IsGrounded())
            {
                walkEvent?.Invoke(horizontal);
            }
        }
        else if (horizontal < 0)
        {
            player.flipX = true;
            movementVelocity = -movementSpeed * -horizontal;
            if (IsGrounded())
            {
                walkEvent?.Invoke(-horizontal);
            }
        }

        if (IsGrounded())
        {
            jumpEvent?.Invoke(true);
        }
        else
        {
            jumpEvent?.Invoke(false);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jump = true;
            jumpEvent?.Invoke(IsGrounded());
        }
        jumpHeld = (!IsGrounded() && Input.GetButton("Jump")) ? true : false;


        if (Input.GetButtonDown("Fire1") && IsGrounded())
        {
            attackEvent?.Invoke();
        }
        movement = new Vector2(movementVelocity, playerBody.velocity.y);
        playerBody.velocity = movement;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Ray ray = new Ray(groundCheck.position, -groundCheck.up);
        Gizmos.DrawRay(ray);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundMask);
    }
}
