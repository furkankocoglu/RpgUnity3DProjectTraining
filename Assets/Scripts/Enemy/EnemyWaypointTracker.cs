using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaypointTracker : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] walkPoints = default;
    [Header("Movement Settings")]
    public float turnSpeed = 5f;
    public float patrolTime = 10f;
    public float walkDistance = 8f;
    [Header("Attack Settings")]
    public float attacDistance = 1.4f;
    public float attackRate = 1f;

    private Transform playerTarget = default;
    private Animator anim = default;

    private NavMeshAgent agent=default;

    private float currentAttackTime = default;

    private Vector3 nextDestination = default;
    private int index = default;

    //Health
    EnemyHealth enemyHealth = default;
    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
        index = Random.Range(0,walkPoints.Length);
        if (walkPoints.Length > 0)
        {
            InvokeRepeating("Patrol", Random.Range(0,patrolTime),patrolTime);
        }
    }

    // Start is called before the fisrst frame update
    void Start()
    {
        agent.avoidancePriority = Random.Range(1,51);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.currentHealth>0)
        {
            MoveAndAttack();
        }
        else
        {
            anim.ResetTrigger("Hit");
            anim.SetBool("Death", true);
            AudioManager.instance.PlaySfx(5);
            agent.enabled = false;
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsTag("Die")&&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime>0.95f)
            {
                Destroy(this.gameObject,5f);
            }
        }
        
    }
    void MoveAndAttack()
    {
        float distance = Vector3.Distance(transform.position, playerTarget.position);
        if (distance>walkDistance)
        {
            if (agent.remainingDistance>=agent.stoppingDistance)
            {
                agent.isStopped = false;
                agent.speed = 2f;
                anim.SetBool("Walk",true);

                nextDestination = walkPoints[index].position;
                agent.SetDestination(nextDestination);
            }
            else
            {
                agent.isStopped = true;
                agent.speed = 0;
                anim.SetBool("Walk",false);

                nextDestination = walkPoints[index].position;
                agent.SetDestination(nextDestination);
            }
        }
        else
        {
            if (distance > attacDistance + 0.15f && playerTarget.GetComponent<PlayerHealth>().currentHealth>0)
            {
                if (!anim.IsInTransition(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    anim.ResetTrigger("Attack");
                    agent.isStopped = false;
                    agent.speed = 3f;
                    anim.SetBool("Walk", true);
                    agent.SetDestination(playerTarget.position);
                }
                
            }
            else if (distance <= attacDistance && playerTarget.GetComponent<PlayerHealth>().currentHealth > 0)
            {
                agent.isStopped = true;
                anim.SetBool("Walk", false);
                agent.speed = 0;
                Vector3 targetPosition = new Vector3(playerTarget.position.x,transform.position.y,
                    playerTarget.position.z);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(targetPosition-transform.position),turnSpeed*Time.deltaTime);

                if (currentAttackTime >= attackRate)
                {
                    anim.SetTrigger("Attack");
                    AudioManager.instance.PlaySfx(2);
                    currentAttackTime = 0;
                }
                else
                {
                    currentAttackTime += Time.deltaTime;
                }
            }
        }
    }
    void Patrol()
    {
        index = index == walkPoints.Length - 1 ? 0 : index + 1;
    }
}
