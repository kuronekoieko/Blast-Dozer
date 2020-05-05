using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    Vector3 mouseDownPos;
    Rigidbody rb;
    float speed = 30;
    float atk
    {
        get
        {
            Vector3 size = meshRenderer.bounds.size;
            return size.x * size.y * size.z;
        }
    }
    bool isBound;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        if (isBound)
        {
            rb.velocity = -transform.forward * speed;
            DOVirtual.DelayedCall(0.5f, () => { isBound = false; });
        }
        else
        {
            Controller();
        }

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

        if (obstacle.Broken(atk, out int point))
        {
            BreakObstacle(point);
        }
        else
        {
            isBound = true;
        }
    }

    void BreakObstacle(int point)
    {
        GameManager.i.cameraController.Shake();
        Variables.status.point += point;
        // Debug.Log(Variables.status.point);
        if (Variables.status.point > 5)
        {
            transform.localScale = Vector3.one * 2;
            GameManager.i.cameraController.SizeUp();
        }
    }
}
