using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ObstacleController : MonoBehaviour
{
    Collider col;
    void Start()
    {
        col = GetComponent<Collider>();
    }


    void Update()
    {

    }


    public void Broken()
    {
        transform.DOMoveY(10, 1).SetRelative().SetEase(Ease.OutBack);
        transform.DOLocalRotate(new Vector3(180, 0, 180), 0.5f).SetRelative().SetLoops(-1).SetEase(Ease.Linear);
        col.enabled = false;
        DOVirtual.DelayedCall(1, () =>
        {
            gameObject.SetActive(false);
        });
        GameManager.i.explosionManager.Explosion(transform);
    }
}
