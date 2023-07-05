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
        if (!fullChargingFlash && Input.GetMouseButtonDown(0))      // �⺻ ����
        {
            DefaultAttack();
        }
        else if (fullChargingFlash && Input.GetMouseButton(0))      // �����ϼ�
        {
            Flash();
        }
        if (Input.GetKey(KeyCode.LeftControl))                      // �����ϼ� ��¡
        {
            Charging();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))                    // �����ϼ� ��¡ ĵ��
        {
            ChargingCancle();
        }
        if (Input.GetMouseButton(1))                                // ����
        {
            Guard();
        }
        if (Input.GetMouseButtonUp(1))                              // ���� ĵ��
        {
            GuardCancle();
        }
        if (state.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    /** �����ϼ� Ÿ������ ���� ���̸� ���� �¾Ҵ��� üũ �ϰ� hit ��� �����ϱ�*/
    protected override void ProcessRay()
    {
        // ���� �� ����
        Vector3 rayDirection = new Vector3(characterBody.forward.x, 0, characterBody.forward.z).normalized;
        // ���� ���� ���� �� �� ���� ���ϱ�
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

    /** ���� ���� ���� ���ϱ� (������)*/
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
