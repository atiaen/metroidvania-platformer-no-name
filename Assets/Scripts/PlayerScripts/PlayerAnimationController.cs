using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    public Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        PlayerMovement.walkEvent += WalkAnimation;
        PlayerMovement.jumpEvent += JumpAnimation;
        PlayerMovement.attackEvent += AttackAnimation;
        Enemy.attackEvent += HitAnimation;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WalkAnimation(float run)
    {

        playerAnimator.SetFloat("running", run);
    }

    public void JumpAnimation(bool isGrounded)
    {
        if (!isGrounded)
        {
            playerAnimator.SetBool("isGrounded", false);
        }
        if (isGrounded)
        {
            playerAnimator.SetBool("isGrounded", true);
        }

    }

    public void AttackAnimation()
    {
        playerAnimator.SetTrigger("isAttacking");
    }

    public void HitAnimation()
    {
        playerAnimator.SetTrigger("isHit");
    }
}
