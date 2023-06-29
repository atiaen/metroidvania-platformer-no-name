using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEnemy : Enemy
{

    float distanceFrom;
    public float distanceToFollow;
    public float distanceToStop;
    // Start is called before the first frame update
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        // animator.SetFloat("speed", mMovementSpeed);
        distanceFrom = Vector2.Distance(target.transform.position, transform.position);
        var directionToPlayer = target.transform.position - transform.position;
        var direction = Vector3.Dot(directionToPlayer, transform.right);
        if (distanceFrom < distanceToFollow)
        {
            if (distanceFrom > distanceToStop)
            {
                MoveToPlayer(direction);
                AttackPlayer();
                
            }
        }
        else
        {
            CheckForWalls();
            Vector3 directionTranslation = (bIsGoingRight) ? transform.right : -transform.right;
            body.velocity = new Vector2(directionTranslation.x * mMovementSpeed, body.velocity.y);
            animator.SetFloat("speed", body.velocity.magnitude);
            _mSpriteRenderer.flipX = !bIsGoingRight;
        }
    }
    protected override void FixedUpdate()
    {

    }

    private void AttackPlayer(){
        EnemyMeleeDamageScript damageScript = GetComponentInParent<EnemyMeleeDamageScript>();
        damageScript.Damage();
        animator.SetTrigger("isAttacking");
    }

    private void MoveToPlayer(float direction)
    {
        if (direction > 0)
        {
            _mSpriteRenderer.flipX = false;
            body.velocity = new Vector2(direction * mMovementSpeed, body.velocity.y);
            animator.SetFloat("speed", body.velocity.magnitude);
        }
        else if (direction < 0)
        {
            _mSpriteRenderer.flipX = true;
            body.velocity = new Vector2(direction * mMovementSpeed, body.velocity.y);
            animator.SetFloat("speed", body.velocity.magnitude);

        }
        else
        {
            animator.SetFloat("speed", 0);
            body.velocity = new Vector2(0 * mMovementSpeed, body.velocity.y);
        }

    }
}
