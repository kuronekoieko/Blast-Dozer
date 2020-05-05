using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShakeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shake()
    {
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOShakePosition(duration: 1, strength: 0.5f))
            .Append(transform.DOLocalMove(Vector3.zero, 0.5f));


        return;
        transform.DOShakePosition(
            duration: 1,
            strength: 2,
            vibrato: 1,
            randomness: 1,
            snapping: true,
            fadeOut: true)
            .OnComplete(() =>
            {
                transform.localPosition = Vector3.zero;
            });


    }
}
