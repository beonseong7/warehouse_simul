using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_move : MonoBehaviour
{
    public float rotspeed = 300f;
    public float speed = 5;
    float rx, ry;
    public bool mouse = true;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (mouse == true)
            {
                mouse = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                mouse = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        if(mouse== false)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");
            rx += rotspeed * my * Time.deltaTime;
            ry += rotspeed * mx * Time.deltaTime;
            transform.eulerAngles = new Vector3(-rx, ry, 0);
            Vector3 dir = Vector3.right * h + Vector3.forward * v;
            transform.Translate(dir * speed * Time.deltaTime);
        }

    }
}
