using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] BoxCollider KatanaBoxCollider;
    [SerializeField] float flashLenfth=2f;
    [SerializeField] Transform characterBody;
    [SerializeField] AudioSource combo1;
    [SerializeField] AudioSource combo2;
    [SerializeField] AudioSource combo3;
    [SerializeField] AudioSource combo4;
    [SerializeField] AudioSource charging;
    [SerializeField] AudioSource flash;
    Animator animator;

    bool fullChargingFlash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(!fullChargingFlash && Input.GetMouseButtonDown(0))
        {
            DefaultAttack();
        }
        else if(fullChargingFlash && Input.GetMouseButton(0))
        {
            Flash();
        }
        if(Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("walk", false);
            animator.SetBool("heavyCharging", true);
            
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            // ��¡ �� ���� �� ���� ���
            fullChargingFlash = false;
            charging.Stop();
            animator.SetBool("heavyCharging", false);
        }
        // ����
        if(Input.GetMouseButton(1))
        {
            animator.SetBool("guard",true);
        }
        if(Input.GetMouseButtonUp(1))
        {
            animator.SetBool("guard", false);
        }

    }

    private void Flash()
    {
        animator.SetTrigger("flash");
        Vector3 flashDirection = new Vector3(characterBody.forward.x,0, characterBody.forward.z).normalized;
        ProcessRay();
        transform.Translate(flashDirection* Time.deltaTime * flashLenfth);
        fullChargingFlash = false;
    }

    private void ProcessRay()
    {
        Vector3 rayDirection = new Vector3(characterBody.forward.x, 0, characterBody.forward.z).normalized;
        Vector3 rayStartPos = new Vector3(characterBody.transform.position.x, characterBody.transform.position.y + 0.5f, characterBody.transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(rayStartPos, rayDirection, out hit, flashLenfth))
        {
            EnemyController enemy = hit.transform.GetComponent<EnemyController>();
            if (enemy)
            {
                enemy.HitDamage();
            }
        }
    }

    private void DefaultAttack()
    {
        animator.SetBool("walk", false);
        animator.SetBool("guard", false);
        animator.SetTrigger("attack");
        animator.SetBool("attackCheck", true);
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            KatanaBoxCollider.enabled = false;
        }
    }
    #region Anim notify
    /** �޺� ���� */
    // ���� �̹� ������ ���� ���¶�� �ݶ��̴��� Ű���ʰ� ����Ű�� �Է��� �޴´�.
    // ���� ������ ���ؼ� ����Ű�� �Է��� �޾ƾ� �Ѵ�.
    public void AttackBoxColliderOn()
    {
        KatanaBoxCollider.enabled = true;
    }

    /** ��¡ ���� - ��¡ */
    public void ChargingEnd()
    {
        fullChargingFlash = true;
       
    }
    
    public void EndAttack()
    {
        animator.SetBool("attackCheck", false);
        KatanaBoxCollider.enabled = false;
        // ������ ������ �� ����Ű�� �ٽ� �޴´�.
    }

    public void ComboOneSound()
    {
        combo1.Play();
    }
    public void ComboTwoSound()
    {
        combo2.Play();
    }
    public void ComboThreeSound()
    {
        combo3.Play();
    }
    public void ComboFourSound()
    {
        combo4.Play();
    }

    public void ChargingSound()
    {
        charging.Play();
    }

    public void FlashSound()
    {
        flash.Play();
    }

    #endregion
}
