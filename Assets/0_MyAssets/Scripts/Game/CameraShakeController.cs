using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class CameraShakeController : MonoBehaviour
{

    [NonSerialized] public Camera mainCamera;
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {

    }

    public void Shake()
    {
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOShakePosition(duration: 1, strength: 0.5f))
            .Append(transform.DOLocalMove(Vector3.zero, 0.5f));
    }
}
