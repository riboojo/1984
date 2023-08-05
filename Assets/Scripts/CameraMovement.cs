using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    private Vector3 originalRotation = new Vector3(8f, 360f, 0.5f);
    
    float xRotation;
    float yRotation;

    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    public float speed = 200f;

    public void MoveCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        yRotation = Mathf.Clamp(yRotation, -45f, 45f);
        
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    public void CenterCamera()
    {
        transform.rotation = Quaternion.Euler(originalRotation);
    }

}
