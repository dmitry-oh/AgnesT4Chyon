using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed = 1;
    float rotx;
    float roty;
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        if (Input.GetMouseButton(0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            rotx += Input.GetAxis("Mouse Y") * Time.deltaTime * speed;
            roty += Input.GetAxis("Mouse X") * Time.deltaTime * speed;
        }
        transform.rotation = Quaternion.Euler(rotx, roty, 0);
    }
}
