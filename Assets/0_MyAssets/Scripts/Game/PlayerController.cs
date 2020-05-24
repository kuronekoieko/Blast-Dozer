using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using System;
public class PlayerController : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] ParticleSystem levelUpPS;
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
        }
        else
        {
            Controller();
        }
    }

    void Controller()
    {
        if (Variables.screenState != ScreenState.Game)
        {
            rb.velocity = Vector3.zero;
            // rb.velocity = rb.velocity.normalized * speed;
            return;
        }
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

    }

    void OnTriggerEnter(Collider other)
    {
        var obstacle = other.gameObject.GetComponent<ObstacleController>();
        if (obstacle == null) { return; }

        if (obstacle.Broken(atk, out int point))
        {
            BreakObstacle(point);
        }
        else
        {
            Bound();
        }
    }

    void BreakObstacle(int point)
    {
        SoundManager.i.PlayOneShot(3);

        Variables.status.point += point;

        var growthData = GrowthDataSO.i.growthDatas
        .Where(g => Variables.status.point > g.minPoint)
        .LastOrDefault();

        if (growthData == null) { return; }

        GameManager.i.cameraController.Shake(growthData.scale);

        int index = Array.IndexOf(GrowthDataSO.i.growthDatas.ToArray(), growthData);

        if (index == Variables.status.growthIndex) { return; }

        transform.localScale = Vector3.one * growthData.scale;
        GameManager.i.cameraController.SizeUp(growthData.scale);
        Variables.status.growthIndex++;
        levelUpPS.transform.localScale = new Vector3(5, 10, 5) * growthData.scale;
        levelUpPS.Play();
        SoundManager.i.PlayOneShot(4);
    }

    void Bound()
    {
        SoundManager.i.PlayOneShot(2);
        isBound = true;
        speed = 100;

        DOTween.To(() => speed, (x) => speed = x, 0, 0.5f)
        .OnComplete(() =>
        {
            speed = 30;
            isBound = false;
        });
    }
}
