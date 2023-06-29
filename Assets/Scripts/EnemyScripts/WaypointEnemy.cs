using System;
using System.Collections;
using UnityEngine;
using YounGenTech.HealthScript;

public class WaypointEnemy : MonoBehaviour
{
    public Transform[] waypoints;
    public float movementSpeed;
    public Health health;
    public Transform target;
    public bool canChaseTarget;
    public float distanceToStartChasingTarget;
    public float distanceToStop;
    public Rigidbody2D body;
    public bool inReverse = true;
    public Transform currentWaypoint;
    public int currentIndex = 0;
    public bool isWaiting = false;
    public float wayPointWaitTime;
    public Animator animator;
    public bool isDead;
    public static event Action attackEvent;
    public float attackWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        //All initialized variables
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();

        //set initial waypoint to first waypoint
        if (waypoints.Length > 0)
        {
            currentWaypoint = waypoints[0];
        }
    }

    void Update()
    {
        if (health.Value != 0 && !isDead)
        {
            var currentDistanceToTarget = Vector2.Distance(transform.position, target.position);
            if (canChaseTarget)
            {
                // Distance to target

                //Check if distance to target is less than distance to start chasing the target
                if (currentDistanceToTarget <= distanceToStartChasingTarget)
                {
                    //Move towards target
                    MoveTowardsTarget();
                    //set our current waypoint to null
                    currentWaypoint = null;
                }

                //Check if distance to target is less than our distance to stop in front of the target
                if (currentDistanceToTarget <= distanceToStop && !isWaiting)
                {
                    //Call the attack event and play appropriate animation
                    // attackEvent?.Invoke();
                    // PlayMoveAnim(0);
                    // PlayAttackAnim();
                    StartCoroutine(AttackSequenceAnim());

                }

                //Check distance to target and check if current waypoint is null
                if (currentDistanceToTarget > distanceToStartChasingTarget && currentWaypoint == null)
                {
                    //Reset current waypoint to first waypoint
                    currentWaypoint = waypoints[0];
                }

            }

            if (!canChaseTarget)
            {
                if (currentDistanceToTarget < 0.5f)
                {
                    StartCoroutine(AttackSequenceNoAnim());

                }
            }

            //Start waypoint loop
            if (currentWaypoint != null && !isWaiting)
            {
                MoveTowardsWaypoint();
            }

        }
        else
        {
            isDead = true;
            animator.SetBool("isDead", isDead);
        }

    }

    void Pause()
    {
        isWaiting = !isWaiting;
    }

    IEnumerator AttackSequenceAnim()
    {
        attackEvent?.Invoke();
        PlayMoveAnim(0);
        PlayAttackAnim();
        isWaiting = true;
        yield return new WaitForSeconds(attackWaitTime);
        isWaiting = false;
    }

    IEnumerator AttackSequenceNoAnim()
    {
        attackEvent?.Invoke();
        yield return new WaitForSeconds(attackWaitTime);
    }

    void MoveTowardsWaypoint()
    {
        Vector2 currentPos = transform.position;
        Vector2 targetPos = currentWaypoint.position;

        if (Vector2.Distance(currentPos, targetPos) > .1f)
        {

            // Get the direction and normalize
            Vector3 directionOfTravel = targetPos - currentPos;
            directionOfTravel.Normalize();
            PlayMoveAnim(body.velocity.sqrMagnitude);
            //scale the movement on each axis by the directionOfTravel vector components
            body.velocity = new Vector2(directionOfTravel.x * movementSpeed, body.velocity.y);
            if (directionOfTravel.x < 0f)
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if (wayPointWaitTime > 0)
            {
                PlayMoveAnim(0);
                Pause();
                Invoke("Pause", wayPointWaitTime);
            }
            NextWayPoint();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector2 currentPos = transform.position;
        Vector2 targetPos = target.position;

        if (Vector2.Distance(currentPos, targetPos) > distanceToStop)
        {
            Vector3 directionOfTravel = targetPos - currentPos;
            directionOfTravel.Normalize();
            PlayMoveAnim(body.velocity.sqrMagnitude);
            //scale the movement on each axis by the directionOfTravel vector components
            body.velocity = new Vector2(directionOfTravel.x * movementSpeed, body.velocity.y);
            if (directionOfTravel.x < 0f)
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            PlayMoveAnim(0);

        }



    }

    private void PlayMoveAnim(float speed)
    {
        animator.SetFloat("speed", speed);
    }

    private void PlayAttackAnim()
    {
        animator.SetTrigger("isAttacking");
    }

    public void PlayHitAnim()
    {
        animator.SetTrigger("isHit");
    }

    void NextWayPoint()
    {

        if ((!inReverse && currentIndex + 1 >= waypoints.Length) || (inReverse && currentIndex == 0))
        {
            inReverse = !inReverse;
        }
        currentIndex = (!inReverse) ? currentIndex + 1 : currentIndex - 1;
        currentWaypoint = waypoints[currentIndex];
    }
}
