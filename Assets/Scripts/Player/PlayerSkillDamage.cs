using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDamage : MonoBehaviour
{
    public LayerMask enemyLayer=default;
    public float radius = 0.5f;
    public float damageCount = 10f;

    private EnemyHealth enemyHealth = default;
    private PlayerHealth playerHealth = default;
    protected bool collided = default;

    // Update is called once per frame
    internal virtual void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position,radius,enemyLayer);
        foreach (Collider hit in hits)
        {
            if (enemyLayer == (1<<LayerMask.NameToLayer("Enemy")))
            {
                enemyHealth = hit.gameObject.GetComponent<EnemyHealth>();
                collided = true;
            }
            else if (enemyLayer == (1 << LayerMask.NameToLayer("Player")))
            {
                playerHealth = hit.gameObject.GetComponent<PlayerHealth>();
                collided = true;
            }
            if (collided)
            {
                if (enemyLayer == (1 << LayerMask.NameToLayer("Enemy")))
                {
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damageCount);
                        enabled = false;
                    }
                    
                }
                else if (enemyLayer == (1 << LayerMask.NameToLayer("Player")))
                {
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(damageCount);
                        enabled = false;
                    }
                    
                }

            }

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
    
}
