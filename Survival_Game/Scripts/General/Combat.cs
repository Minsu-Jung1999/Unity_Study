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
    protected float fixedDistance;  // 수정된 Flash 점멸 거리, 벽이 있다면 넘지 않게 하기 위함

    [SerializeField] Renderer render;

    protected Animator animator;
    protected bool fullChargingFlash;
    protected Slider hpBar;
    [SerializeField]protected bool isStun;  // 적에게 맞으면 잠시 스턴이 걸린다. (플레이어는 다른 방식으로 활용할 예정)
    protected bool isDeath;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }
    

    /** 벽력일섬 준비 애니메이션 재생 */
    protected void Charging()
    {
        animator.SetBool("walk", false);
        animator.SetBool("heavyCharging", true);
    }

    /** 차징 후 공격 안 했을 경우 */
    protected void ChargingCancle()
    {
        fullChargingFlash = false;
        SoundManager.instance.StopSE(SoundManager.SoundOrder.CHARGING);
        animator.SetBool("heavyCharging", false);
    }

    /** 가드 애니메이션 재생 취소*/
    protected void GuardCancle()
    {
        animator.SetBool("guard", false);
    }
    
    /** 가드 애니메이션 재생 */
    protected void Guard()
    {
        animator.SetBool("guard", true);
    }

    /** 벽력일섬 실행 및 애니매이션 재생 */
    protected virtual void Flash()
    {
        // 애니매이션 재생
        animator.SetTrigger("flash");

        // 포지션 앞으로 이동 방향 벡터 구하기
        Vector3 flashDirection = new Vector3(characterBody.forward.x, 0, characterBody.forward.z).normalized;

        // 이동하기 전에 레이 쏴서 레이에 맞는 적 hit 애니매이션 재생하게 하기
        ProcessRay();

        // 포지션 이동하기
        transform.Translate(flashDirection * fixedDistance);

        // 차징하고 있다면 취소시키기 
        fullChargingFlash = false;
    }

    /** 레이를 쏴서 맞는 적 감지해서 Hit 애니메이션 재생시키기 */
    protected virtual void ProcessRay()
    {}

    /** 기본 공격 -> 콤보로 이어짐 */
    protected void DefaultAttack()
    {
        animator.SetBool("walk", false);        // 걷고 있다가 공격 시 바로 공격이 진행 되도록
        animator.SetBool("guard", false);       // 가드 중에 공격 시 바로 공격 진행 
        animator.SetTrigger("attack");          // 공격 애니메이션 실행
        animator.SetBool("attackCheck", true);  // 공격 시 이동이 불가하게 하기 위해 Playermovement 에서 감지 할 bool 변수 조작해준다.
    }

    /** 공격 감지 범위 정하기 */
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            KatanaBoxCollider.enabled = false;
        }
    }
    #region Anim notify

    /** 콤보 공격 
    * 만약 이미 공격을 가한 상태라면 콜라이더를 키자말고 공격키만 입력을 받는다.
    * 연속 공격을 위해서 공겨키를 입력을 받아야 한다. */
    protected void AttackBoxColliderOn()
    {
        KatanaBoxCollider.enabled = true;
    }

    /** 차징 공격 - 차징 */
    protected void ChargingEnd()
    {
        fullChargingFlash = true;
    }

    protected  void EndAttack()
    {
        // 공격이 끝났을 때 공격키를 다시 받는다.
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

        // 가드를 안 했을 경우
        if(!animator.GetBool("guard"))
        {
            isStun = true;
            StartCoroutine("Stun");
            animator.SetTrigger("hit");
            SoundManager.instance.PlaySE(SoundManager.SoundOrder.HIT);
            StartCoroutine("HitProcessing");

            // 체력 깎기
            state.currentHealth -= damage;
            // UI 이미지 줄여주기
            state.hpBar.fillAmount -= damage/state.maxHealth;

        }
        // 가드 했을 경우 데미지가 0이 들어온다.
        else
        {
            ShieldBlock();
        }

    }

    protected virtual IEnumerator Stun()
    {
        // Enermy Controller  에서 구현 예정
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
