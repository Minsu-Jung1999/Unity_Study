using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] CameraController cameraController;
    [SerializeField] Transform characterBody;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!animator.GetBool("attackCheck") && !animator.GetBool("heavyCharging"))
        {
            Move();
        }
    }

    private void Move()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        if (Mathf.Abs(xAxis) <= 0.9f && Mathf.Abs(yAxis) <= 0.9f)
        {
            animator.SetBool("walk", false);
        }
        // ī�޶� �ٶ󺸴� �������� �̵�
        if (cameraController && (xAxis != 0 || yAxis != 0))
        {
            Vector3 forwardVector = new Vector3(cameraController.transform.forward.x, 0f, cameraController.transform.forward.z).normalized;
            Vector3 rightVector = new Vector3(cameraController.transform.right.x, 0, cameraController.transform.right.z).normalized;
            Vector3 moveDir = forwardVector * yAxis + rightVector * xAxis;
            transform.Translate(moveDir * Time.deltaTime * moveSpeed);
            Turn(moveDir);
            animator.SetBool("walk", true);
        }

    }

    void Turn(Vector3 forwardRotation)
    {
        characterBody.rotation = Quaternion.Slerp(characterBody.rotation, Quaternion.LookRotation(forwardRotation), Time.deltaTime * rotateSpeed);
    }

}
