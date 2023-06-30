using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyController : Combat
{


    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 2f;

    NavMeshAgent navMeshAgent;
    Slider hpBar;
    float distanceToTarget;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        hpBar = FindObjectOfType<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if(distanceToTarget <= chaseRange)
        {
            FaceToTarget();
            ChaseTarget();
        }
        else
        {
            StopChasing();
        }
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            DefaultAttack();
        }
        if(hpBar == null)
        {
            hpBar = FindObjectOfType<Slider>();
        }
        if(state.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void StopChasing()
    {
        animator.SetBool("walk", false);
    }

    private void ChaseTarget()
    {
        animator.SetBool("walk", true);
        navMeshAgent.SetDestination(target.position);
    }

    private void FaceToTarget()
    {
        // tranform.rotation = 타겟이 있는곳 그리고 일정한 속도로 회전한다.
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Katana")
        {
            HitDamage();
            hpBar.value -= 0.2f;
        }
        if (other.tag == "Player")
        {
            KatanaBoxCollider.enabled = false;
        }
    }
  


}
