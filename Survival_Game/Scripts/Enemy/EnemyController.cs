using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyController : Combat
{


    [SerializeField] Transform target;
    [SerializeField] Camera targetCam;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 2f;
    [SerializeField] float stunTime = 1f;
    [SerializeField] Canvas EnermyCanvas;

    NavMeshAgent navMeshAgent;
    float distanceToTarget;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        targetCam = FindObjectOfType<Camera>();
        EnermyCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null && !isStun && !isDeath)
        {
            distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            if (distanceToTarget <= chaseRange)
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
        }
        else
        {
            Debug.Log("Target is disappear");
        }
        if (state.currentHealth <= 0)
        {
            EnemyDeath();
        }
    }
    public void DestroyEnemy()
    {
        StartCoroutine("DestoryOB");
    }
    protected IEnumerator DestoryOB()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void StopChasing()
    {
        animator.SetBool("walk", false);
        EnermyCanvas.enabled = false;
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

        BarRotationADJ();
        EnermyCanvas.enabled = true;
    }

    private void BarRotationADJ()
    {
        EnermyCanvas.transform.LookAt(targetCam.transform);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            KatanaBoxCollider.enabled = false;
            PlayerCombatController player = other.GetComponent<PlayerCombatController>();

            player.HitDamage(state.currentAttackDamage);

        }
    }

    protected override IEnumerator Stun()
    {
        yield return new WaitForSeconds(stunTime);
        isStun = false;
    }

}
