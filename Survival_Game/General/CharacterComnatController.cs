using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComnatController : Combat
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
            ChrgingCancle();
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

        // �� �κ��� �� �Լ����� ���� ����
        RaycastHit hit;
        if (Physics.Raycast(rayStartPos, rayDirection, out hit, flashDistance))
        {
            EnemyController enemy = hit.transform.GetComponent<EnemyController>();
            if (enemy)
            {
                enemy.HitDamage();
            }
        }
    }


    /** ���� ���� ���� ���ϱ� (������)*/
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            KatanaBoxCollider.enabled = false;
        }
        if(other.tag == "EnemyKatana")
        {
            HitDamage();
        }
    }

}
