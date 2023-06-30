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
    [SerializeField] protected AudioSource combo1;
    [SerializeField] protected AudioSource combo2;
    [SerializeField] protected AudioSource combo3;
    [SerializeField] protected AudioSource combo4;
    [SerializeField] protected AudioSource charging;
    [SerializeField] protected AudioSource flash;
    [SerializeField] protected AudioSource hitSound;
    [SerializeField] protected AudioSource shieldSound;

    [SerializeField] Renderer render;

    protected Animator animator;
    protected bool fullChargingFlash;
    protected Slider hpBar;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }
    protected void Update()
    {
        if(state.currentHealth <= 0)
        {
            Death();
        }
    }

    /** 벽력일섬 준비 애니메이션 재생 */
    protected void Charging()
    {
        animator.SetBool("walk", false);
        animator.SetBool("heavyCharging", true);
    }

    /** 차징 후 공격 안 했을 경우 */
    protected void ChrgingCancle()
    {
        fullChargingFlash = false;
        charging.Stop();
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
        transform.Translate(flashDirection * Time.deltaTime * flashDistance);

        // 차징하고 있다면 취소시키기 
        fullChargingFlash = false;
    }

    /** 레이를 쏴서 맞는 적 감지해서 Hit 애니메이션 재생시키기 */
    protected virtual void ProcessRay()
    {
        // 레이 쏠 방향
        Vector3 rayDirection = new Vector3(characterBody.forward.x, 0, characterBody.forward.z).normalized;

        // 레이 시작 점과 끝 점 벡터 구하기
        Vector3 rayStartPos = new Vector3(characterBody.transform.position.x, characterBody.transform.position.y + 0.5f, characterBody.transform.position.z);
    }

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
        combo1.Play();
    }
    protected  void ComboTwoSound()
    {
        combo2.Play();
    }
    protected  void ComboThreeSound()
    {
        combo3.Play();
    }
    protected  void ComboFourSound()
    {
        combo4.Play();
    }

    protected  void ChargingSound()
    {
        charging.Play();
    }

    protected  void FlashSound()
    {
        flash.Play();
    }

    #endregion

    public virtual void HitDamage()
    {
        animator.SetBool("attackCheck", false);
        // 가드를 안 했을 경우
        if(!animator.GetBool("guard"))
        {
            animator.SetTrigger("hit");
            hitSound.Play();
            StartCoroutine("HitProcessing");

            // 체력 깎기
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
        shieldSound.Play();
    }

    IEnumerator HitProcessing()
    {
        Color originColor = render.material.color;
        render.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        render.material.color = originColor;

    }

}
