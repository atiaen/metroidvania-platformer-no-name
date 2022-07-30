using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YounGenTech.HealthScript;

public class EnemyMeleeDamageScript : MonoBehaviour
{
    public LayerMask damageLayers;
    public float damageAmount;
    public Transform damageArea;
    public Vector2 damageAreaSize;

    public float knockDistance;
    // Start is called before the first frame update
    void Start()
    {
        Enemy.attackEvent += Damage;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage()
    {
        Collider2D colliders = Physics2D.OverlapBox(damageArea.position, damageAreaSize, 0f, damageLayers);
        PlayerMovement player;
        //other = collider;
        if (colliders)
        {
            Vector2 direction = transform.position - colliders.transform.position;
            if (colliders.tag == "Player")
            {
                player = colliders.gameObject.GetComponentInParent<PlayerMovement>();
                Rigidbody2D body = colliders.GetComponentInParent<Rigidbody2D>();
                StartCoroutine(PlayerMovement.KnockBack(-direction,knockDistance,body,player));
                Health health = colliders.gameObject.GetComponentInParent<Health>();
                if (health)
                {
                    health.Damage(new HealthEvent(colliders.gameObject, damageAmount));
                }

            }
            else
            {
                Health health = colliders.gameObject.GetComponentInParent<Health>();
                if (health)
                {
                    health.Damage(new HealthEvent(colliders.gameObject, damageAmount));
                }
            }


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(damageArea.position, damageAreaSize);
    }
}
