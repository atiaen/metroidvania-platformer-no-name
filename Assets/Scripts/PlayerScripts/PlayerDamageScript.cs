using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YounGenTech.HealthScript;

public class PlayerDamageScript : MonoBehaviour
{
    public LayerMask damageLayers;
    public float damageAmount;
    public Transform damageArea;
    public float damageAreaSize;

    public float knockDistance;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.attackEvent += Damage;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage()
    {
        Collider2D colliders = Physics2D.OverlapCircle(damageArea.position, damageAreaSize, damageLayers);
        if (colliders)
        {
            Vector2 direction = transform.position - colliders.transform.position;
            Health health = colliders.gameObject.GetComponentInParent<Health>();
            if (health)
            {
                health.Damage(new HealthEvent(colliders.gameObject, damageAmount));
            }


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(damageArea.position, damageAreaSize);
    }
}
