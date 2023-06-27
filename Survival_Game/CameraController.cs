using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera camera;

    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float xMinClampAngle = -90;
    [SerializeField] float xMaxClampAngle = 90;
    [SerializeField] float cameraDistance = 5f;

    [SerializeField] Vector3 cameraAngle;
    [SerializeField] Vector3 cameraPosition;


    float xRotation;
    float yRotation;

    void Start()
    {
        camera.transform.position = cameraPosition;
        camera.transform.Rotate(cameraAngle);
    }

    void Update()
    {
        
        Rotation();
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

}
