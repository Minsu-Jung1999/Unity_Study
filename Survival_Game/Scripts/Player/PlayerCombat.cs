/**
 * Character Combat���� �����Ͽ� �� �̻� ������� �ʴ� �ڵ�. ��, ���߿� ������ �������� ����� ���� �ڵ��̱� ������ ������ ���� ���� ����
 * */
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerCombat : Combat
//{
   
//    // Start is called before the first frame update
//    void Start()
//    {
//        //animator = GetComponent<Animator>();    
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if(!fullChargingFlash && Input.GetMouseButtonDown(0))
//        {
//            DefaultAttack();
//        }
//        else if(fullChargingFlash && Input.GetMouseButton(0))
//        {
//            Flash();
//        }
//        if(Input.GetKey(KeyCode.LeftControl))
//        {
//            Vector3 rayDirection = new Vector3(characterBody.forward.x, 0, characterBody.forward.z).normalized;
//            Vector3 rayStartPos = new Vector3(characterBody.transform.position.x, characterBody.transform.position.y + 0.5f, characterBody.transform.position.z);
//            RaycastHit hit;
//            Ray ray = Camera.main.ScreenPointToRay(transform.position);
//            Debug.DrawRay(rayStartPos, rayDirection * flashDistance, Color.blue);
//            Debug.Log("is Charing");
//            Charging();
//        }
//        if(Input.GetKeyUp(KeyCode.LeftControl))
//        {
            
//            ChargingCancle();
//        }
//        // ����
//        if(Input.GetMouseButton(1))
//        {
//            Guard();
//        }
//        if(Input.GetMouseButtonUp(1))
//        {
//            GuardCancle();
//        }
//    }

//    protected override void ProcessRay()
//    {
//        Vector3 rayDirection = new Vector3(characterBody.forward.x, 0, characterBody.forward.z).normalized;
//        Vector3 rayStartPos = new Vector3(characterBody.transform.position.x, characterBody.transform.position.y + 0.5f, characterBody.transform.position.z);
//        RaycastHit hit;
//        Ray ray = Camera.main.ScreenPointToRay(transform.position);
//        if (Physics.Raycast(rayStartPos, rayDirection, out hit, flashDistance))
//        {
//            EnemyController enemy = hit.transform.GetComponent<EnemyController>();
//            if (enemy)
//            {
//                enemy.HitDamage();
//            }
//            if(hit.transform.tag == "Wall")
//            {
//                Debug.Log("wall detected");
//            }
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if(other.tag=="Enemy")
//        {
//            KatanaBoxCollider.enabled = false;
//        }
//    }
//}
