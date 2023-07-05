using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]  Camera camera;

    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float xMinClampAngle = -90;
    [SerializeField] float xMaxClampAngle = 90;


    [SerializeField] float cameraCenter = 0f;
    [SerializeField] float cameraDistance = 5f;
    [SerializeField] float cameraHeight = 5f;

    [SerializeField] Vector3 cameraAngle;
    [SerializeField] Vector3 cameraPosition;


    //float fixedDistance;
    float xRotation;
    float yRotation;

    void Start()
    {
        //fixedDistance = cameraDistance;
        //CamPositionSetting();
        
        camera.transform.Rotate(cameraAngle);
    }

   

    void Update()
    {
        
        Rotation();
        //CamPositionSetting();
    }

    private void Rotation()
    {
        float xAxis = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
        float yAxis = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;

        xRotation += xAxis;
        yRotation = transform.eulerAngles.y + yAxis;
        xRotation = Mathf.Clamp(xRotation, xMinClampAngle, xMaxClampAngle);

        transform.eulerAngles = new Vector3(xRotation, yRotation, 0);
    }

    //private void CamPositionSetting()
    //{
    //    WallChecking();
    //    cameraPosition = new Vector3(cameraCenter, cameraHeight, fixedDistance);
    //    camera.transform.localPosition = cameraPosition;
    //}

    //private void WallChecking()
    //{
    //    // 레이 쏠 방향
    //    Vector3 rayDirection = camera.transform.position- transform.position;
    //    // 레이 시작 점
    //    Vector3 rayStartPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    //    RaycastHit hit;
    //    Debug.DrawRay(rayStartPos, rayDirection, Color.red, cameraDistance);
    //    if (Physics.Raycast(rayStartPos, rayDirection, out hit, -cameraDistance))
    //    {
    //        float distance = Vector3.Distance(transform.position, hit.point);
    //        if (hit.transform.tag == "Wall")
    //        {
    //            Debug.Log("Wall is dectacted");
    //            fixedDistance = Vector3.Distance(transform.position, hit.point);
    //            fixedDistance = distance - 1.0f;
    //        }
           
    //    }
        
    //}
}
