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
    public CameraShakeController cameraShakeController;
    PlayerController playerController;
    Vector3 vecFromPlayer;
    int maxSize;
    float nFocalLength;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        vecFromPlayer = transform.position - playerController.transform.position;
        nFocalLength = vecFromPlayer.magnitude;
    }

    void Update()
    {
        transform.position = playerController.transform.position + vecFromPlayer.normalized * nFocalLength;
    }

    public void Shake()
    {
        cameraShakeController.Shake();
    }

    public void SizeUp(float scale)
    {
        //if (size > maxSize) { return; }
        float aperture = 25;
        float f = focalLength(Camera.main.fieldOfView, aperture * scale);
        DOTween.To(() => nFocalLength, (x) => nFocalLength = x, f, 0.5f);
    }

    /*! 
 @brief 焦点距離(FocalLength)を求める
 @param[in]		fov			視野角(FieldOfView)
 @param[in]		aperture	画面幅いっぱいに表示したいオブジェクトの幅
 @return        焦点距離(FocalLength)
*/
    float focalLength(float fov, float aperture)
    {
        // FieldOfViewを2で割り、三角関数用にラジアンに変換しておく
        float nHalfTheFOV = fov / 2.0f * Mathf.Deg2Rad;

        // FocalLengthを求める
        float nFocalLength = (0.5f / (Mathf.Tan(nHalfTheFOV) / aperture));

        // Unityちゃんは画面高さ(Vertical)なFOVなので画面アスペクト比(縦/横)を掛けとく
        nFocalLength *= ((float)Screen.height / (float)Screen.width);

        return nFocalLength;
    }
}
