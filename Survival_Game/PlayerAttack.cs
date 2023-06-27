using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] BoxCollider KatanaBoxCollider;
    Animator animator;
    bool attacked;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
       
    }

    private void Attack()
    {
        animator.SetBool("walk", false);
        animator.SetTrigger("attack");
        animator.SetBool("attackCheck", true);
        KatanaBoxCollider.enabled = true;
    }
    
    public void EndAttack()
    {
        animator.SetBool("attackCheck", false);
        KatanaBoxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            KatanaBoxCollider.enabled = false;
            Debug.Log("Attack!!!");
        }
    }
}
