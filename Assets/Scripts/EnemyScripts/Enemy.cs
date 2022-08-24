using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YounGenTech.HealthScript;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public static event Action attackEvent;
    public float mMovementSpeed = 3.0f;
    public bool bIsGoingRight = true;

    public Transform wallCheck;
    public Transform groundCheck;

    public bool isHit = false;
    public bool isDead = false;

    public float mRaycastingDistance = 1f;

    public float groundDistance = 1f;
    public Rigidbody2D body;
    public Animator animator;
    public Health health;

    public Transform target;

    public SpriteRenderer _mSpriteRenderer;

    float distanceBtw;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _mSpriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        body = GetComponent<Rigidbody2D>();
        _mSpriteRenderer.flipX = bIsGoingRight;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (health.Value != 0 && !isDead)
        {
            // transform.Translate(directionTranslation);
            animator.SetFloat("speed", mMovementSpeed);
            distanceBtw = Vector2.Distance(target.transform.position, transform.position);
            Vector2 direction = transform.position - target.transform.position;
            if (distanceBtw <= 0.35)
            {
                attackEvent?.Invoke();
            }

            CheckForWalls();
        }else{
            isDead = true;
            animator.SetBool("isDead", isDead);
        }

    }

    protected virtual void FixedUpdate()
    {
        Vector3 directionTranslation = (bIsGoingRight) ? transform.right : -transform.right;
        body.velocity = new Vector2(directionTranslation.x * mMovementSpeed, body.velocity.y);
    }

    public void CheckForWalls()
    {
        Vector3 raycastDirection = (bIsGoingRight) ? Vector3.right : Vector3.left;
        RaycastHit2D wallhit = Physics2D.Raycast(wallCheck.position + raycastDirection * mRaycastingDistance, raycastDirection, mRaycastingDistance);
        RaycastHit2D groundhit = Physics2D.Raycast(groundCheck.position + Vector3.down * groundDistance, Vector2.down, groundDistance);
        Debug.DrawRay(wallCheck.position + raycastDirection, raycastDirection, Color.red);
        Debug.DrawRay(groundCheck.position + Vector3.down * groundDistance, Vector2.down, Color.red);
        if (wallhit.collider != null && groundhit.collider != null)
        {
            if (wallhit.transform.tag == "Ground" && groundhit.transform.tag == "Ground")
            {
                bIsGoingRight = !bIsGoingRight;
                _mSpriteRenderer.flipX = bIsGoingRight;

            }
            // else if (wallhit.transform.tag == "Ground" && groundhit.transform.tag != "Ground")
            // {
            //     bIsGoingRight = !bIsGoingRight;
            //     _mSpriteRenderer.flipX = bIsGoingRight;
            // }
            // else if (wallhit.transform.tag == "Ground" && groundhit.transform.tag != "Ground")
            // {
            //     bIsGoingRight = !bIsGoingRight;
            //     _mSpriteRenderer.flipX = bIsGoingRight;
            // }

        }
        else if (groundhit.collider == null)
        {
            bIsGoingRight = !bIsGoingRight;
            _mSpriteRenderer.flipX = bIsGoingRight;
        }
    }

    public void HitAnimator()
    {
        animator.SetTrigger("isHit");
    }

    //   public void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Ray ray = new Ray(groundCheck.position, -groundCheck.up);
    //     Gizmos.DrawRay(ray);
    // }
}
