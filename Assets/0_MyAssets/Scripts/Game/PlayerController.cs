using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 mouseDownPos;
    Rigidbody rb;
    float speed = 30;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        Vector3 direction = Vector3.zero;
        if (Input.GetMouseButton(0))
        {
            direction = Input.mousePosition - mouseDownPos;
            if (direction == Vector3.zero) { return; }
            direction.z = direction.y;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);

        }
        rb.velocity = direction.normalized * speed;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        var obstacle = collisionInfo.gameObject.GetComponent<ObstacleController>();
        if (obstacle == null) { return; }
        obstacle.Broken();
    }
}
