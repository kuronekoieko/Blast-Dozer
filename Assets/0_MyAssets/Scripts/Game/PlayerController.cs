using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 mouseDownPos;
    void Start()
    {

    }


    void FixedUpdate()
    {
        Controller();
    }

    void Controller()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = Input.mousePosition - mouseDownPos;
            direction.z = direction.y;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
