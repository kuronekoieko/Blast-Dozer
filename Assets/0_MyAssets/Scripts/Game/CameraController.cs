using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Unityで解像度に合わせて画面のサイズを自動調整する
/// http://www.project-unknown.jp/entry/2017/01/05/212837
/// </summary>
public class CameraController : MonoBehaviour
{
    public static CameraController i;
    PlayerController playerController;
    Vector3 vecFromPlayer;
    void Start()
    {
        if (i == null) i = this;
        playerController = FindObjectOfType<PlayerController>();
        vecFromPlayer = transform.position - playerController.transform.position;
    }

    void Update()
    {
        transform.position = playerController.transform.position + vecFromPlayer;
    }
}
