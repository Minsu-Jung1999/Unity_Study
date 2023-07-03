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

    //[SerializeField] protected AudioSource combo1;
    //[SerializeField] protected AudioSource combo2;
    //[SerializeField] protected AudioSource combo3;
    //[SerializeField] protected AudioSource combo4;
    //[SerializeField] protected AudioSource charging;
    //[SerializeField] protected AudioSource flash;
    //[SerializeField] protected AudioSource hitSound;
    //[SerializeField] protected AudioSource shieldSound;
    

    [SerializeField] Renderer render;

    protected Animator animator;
    protected bool fullChargingFlash;
    protected Slider hpBar;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }
    protected virtual void Update()
    {
        if(state.currentHealth <= 0)
        {
            Death();
        }
    }

    /** �����ϼ� �غ� �ִϸ��̼� ��� */
    protected void Charging()
    {
        animator.SetBool("walk", false);
        animator.SetBool("heavyCharging", true);
    }

    /** ��¡ �� ���� �� ���� ��� */
    protected void ChrgingCancle()
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
        transform.Translate(flashDirection * Time.deltaTime * flashDistance);

        // ��¡�ϰ� �ִٸ� ��ҽ�Ű�� 
        fullChargingFlash = false;
    }

    /** ���̸� ���� �´� �� �����ؼ� Hit �ִϸ��̼� �����Ű�� */
    protected virtual void ProcessRay()
    {
        // ���� �� ����
        Vector3 rayDirection = new Vector3(characterBody.forward.x, 0, characterBody.forward.z).normalized;

        // ���� ���� ���� �� �� ���� ���ϱ�
        Vector3 rayStartPos = new Vector3(characterBody.transform.position.x, characterBody.transform.position.y + 0.5f, characterBody.transform.position.z);
    }

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

    public virtual void HitDamage()
    {
        animator.SetBool("attackCheck", false);
        // ���带 �� ���� ���
        if(!animator.GetBool("guard"))
        {
            animator.SetTrigger("hit");
            SoundManager.instance.PlaySE(SoundManager.SoundOrder.HIT);
            StartCoroutine("HitProcessing");

            // ü�� ���
            state.currentHealth -= state.currentAttackDamage;

        }
        else
        {
            ShieldBlock();
        }

    }

    protected void Death()
    {
        Destroy(gameObject);
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
