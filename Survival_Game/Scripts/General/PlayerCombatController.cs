using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCombatController : Combat
{

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (!fullChargingFlash && Input.GetMouseButtonDown(0))      // 기본 공격
        {
            DefaultAttack();
        }
        else if (fullChargingFlash && Input.GetMouseButton(0))      // 벽력일섬
        {
            Flash();
        }
        if (Input.GetKey(KeyCode.LeftControl))                      // 벽력일섬 차징
        {
            Charging();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))                    // 벽력일섬 차징 캔슬
        {
            ChargingCancle();
        }
        if (Input.GetMouseButton(1))                                // 가드
        {
            Guard();
        }
        if (Input.GetMouseButtonUp(1))                              // 가드 캔슬
        {
            GuardCancle();
        }
        if (state.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    /** 벽력일섬 타겟팅을 위해 레이를 쏴서 맞았는지 체크 하고 hit 모션 진행하기*/
    protected override void ProcessRay()
    {
        // 레이 쏠 방향
        Vector3 rayDirection = new Vector3(characterBody.forward.x, 0, characterBody.forward.z).normalized;
        // 레이 시작 점과 끝 점 벡터 구하기
        Vector3 rayStartPos = new Vector3(characterBody.transform.position.x, characterBody.transform.position.y + 0.5f, characterBody.transform.position.z);

        RaycastHit hit;
        if (Physics.Raycast(rayStartPos, rayDirection, out hit, flashDistance))
        {
            float distance = Vector3.Distance(transform.position, hit.point);
            if (hit.transform.tag == "Enemy")
            {
                HitEnemy(hit.transform);
                fixedDistance = distance + 1.0f;
            }
            if (hit.transform.tag == "Wall")
            {
                fixedDistance = distance - 1.0f;

                if (fixedDistance < 0)
                    fixedDistance = 0;
            }
        }
        else
        {
            fixedDistance = flashDistance;
        }
    }

    private void HitEnemy(Transform enemyObject)
    {
        EnemyController enemy = enemyObject.transform.GetComponent<EnemyController>();
        enemy.HitDamage(state.currentAttackDamage);
    }

    /** 공격 감지 범위 정하기 (재정의)*/
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            KatanaBoxCollider.enabled = false;
            Debug.Log("Hit");
            HitEnemy(other.transform);
        }
    }



}
