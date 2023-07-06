using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Combat : MonoBehaviour
{
    [SerializeField] protected CharacterState state;
    [SerializeField] protected BoxCollider KatanaBoxCollider;
    [SerializeField] protected float flashDistance = 2f;
    [SerializeField] protected Transform characterBody;
    protected float fixedDistance;  // ������ Flash ���� �Ÿ�, ���� �ִٸ� ���� �ʰ� �ϱ� ����

    [SerializeField] Renderer render;

    protected Animator animator;
    protected bool fullChargingFlash;
    protected Slider hpBar;
    [SerializeField]protected bool isStun;  // ������ ������ ��� ������ �ɸ���. (�÷��̾�� �ٸ� ������� Ȱ���� ����)
    protected bool isDeath;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }
    

    /** �����ϼ� �غ� �ִϸ��̼� ��� */
    protected void Charging()
    {
        animator.SetBool("walk", false);
        animator.SetBool("heavyCharging", true);
    }

    /** ��¡ �� ���� �� ���� ��� */
    protected void ChargingCancle()
    {
        fullChargingFlash = false;
        SoundManager.instance.StopSE(SoundManager.SoundOrder.CHARGING);
        animator.SetBool("heavyCharging", false);
    }

    /** ���� �ִϸ��̼� ��� ���*/
    protected void GuardCancle()
    {
        animator.SetBool("guard", false);
    }
    
    /** ���� �ִϸ��̼� ��� */
    protected void Guard()
    {
        animator.SetBool("guard", true);
    }

    /** �����ϼ� ���� �� �ִϸ��̼� ��� */
    protected virtual void Flash()
    {
        // �ִϸ��̼� ���
        animator.SetTrigger("flash");

        // ������ ������ �̵� ���� ���� ���ϱ�
        Vector3 flashDirection = new Vector3(characterBody.forward.x, 0, characterBody.forward.z).normalized;

        // �̵��ϱ� ���� ���� ���� ���̿� �´� �� hit �ִϸ��̼� ����ϰ� �ϱ�
        ProcessRay();

        // ������ �̵��ϱ�
        transform.Translate(flashDirection * fixedDistance);

        // ��¡�ϰ� �ִٸ� ��ҽ�Ű�� 
        fullChargingFlash = false;
    }

    /** ���̸� ���� �´� �� �����ؼ� Hit �ִϸ��̼� �����Ű�� */
    protected virtual void ProcessRay()
    {}

    /** �⺻ ���� -> �޺��� �̾��� */
    protected void DefaultAttack()
    {
        animator.SetBool("walk", false);        // �Ȱ� �ִٰ� ���� �� �ٷ� ������ ���� �ǵ���
        animator.SetBool("guard", false);       // ���� �߿� ���� �� �ٷ� ���� ���� 
        animator.SetTrigger("attack");          // ���� �ִϸ��̼� ����
        animator.SetBool("attackCheck", true);  // ���� �� �̵��� �Ұ��ϰ� �ϱ� ���� Playermovement ���� ���� �� bool ���� �������ش�.
    }

    /** ���� ���� ���� ���ϱ� */
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            KatanaBoxCollider.enabled = false;
        }
    }
    #region Anim notify

    /** �޺� ���� 
    * ���� �̹� ������ ���� ���¶�� �ݶ��̴��� Ű�ڸ��� ����Ű�� �Է��� �޴´�.
    * ���� ������ ���ؼ� ����Ű�� �Է��� �޾ƾ� �Ѵ�. */
    protected void AttackBoxColliderOn()
    {
        KatanaBoxCollider.enabled = true;
    }

    /** ��¡ ���� - ��¡ */
    protected void ChargingEnd()
    {
        fullChargingFlash = true;
    }

    protected  void EndAttack()
    {
        // ������ ������ �� ����Ű�� �ٽ� �޴´�.
        animator.SetBool("attackCheck", false);
        KatanaBoxCollider.enabled = false;
    }

    protected  void ComboOneSound()
    {
        SoundManager.instance.PlaySE(SoundManager.SoundOrder.COMBO_1);
    }
    protected  void ComboTwoSound()
    {
        SoundManager.instance.PlaySE(SoundManager.SoundOrder.COMBO_2);
    }
    protected  void ComboThreeSound()
    {
        SoundManager.instance.PlaySE(SoundManager.SoundOrder.COMBO_3);
    }
    protected  void ComboFourSound()
    {
        SoundManager.instance.PlaySE(SoundManager.SoundOrder.COMBO_4);
    }

    protected  void ChargingSound()
    {
        SoundManager.instance.PlaySE(SoundManager.SoundOrder.CHARGING);
    }

    protected  void FlashSound()
    {
        SoundManager.instance.PlaySE(SoundManager.SoundOrder.FLASH);
    }

    #endregion

    public virtual void HitDamage(float damage)
    {
        animator.SetBool("attackCheck", false);

        // ���带 �� ���� ���
        if(!animator.GetBool("guard"))
        {
            isStun = true;
            StartCoroutine("Stun");
            animator.SetTrigger("hit");
            SoundManager.instance.PlaySE(SoundManager.SoundOrder.HIT);
            StartCoroutine("HitProcessing");

            // ü�� ���
            state.currentHealth -= damage;
            // UI �̹��� �ٿ��ֱ�
            state.hpBar.fillAmount -= damage/state.maxHealth;

        }
        // ���� ���� ��� �������� 0�� ���´�.
        else
        {
            ShieldBlock();
        }

    }

    protected virtual IEnumerator Stun()
    {
        // Enermy Controller  ���� ���� ����
        yield return null;
        isStun = true;
    }

    protected virtual void EnemyDeath()
    {
        isDeath = true;
        animator.SetTrigger("Edeath");
    }
    protected virtual void PlayerDeath()
    {
        isDeath = true;
        animator.SetTrigger("death");
    }
     
       


    private void ShieldBlock()
    {
        animator.SetTrigger("guardHit");
        SoundManager.instance.PlaySE(SoundManager.SoundOrder.SHIELD);
    }

    IEnumerator HitProcessing()
    {
        Color originColor = render.material.color;
        render.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        render.material.color = originColor;

    }

}
