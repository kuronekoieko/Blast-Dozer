using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ObstacleController : MonoBehaviour
{
    Collider col;
    MeshRenderer meshRenderer;
    float volume;
    int point;
    void Start()
    {
        col = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
        Vector3 size = meshRenderer.bounds.size;
        volume = size.x * size.y * size.z;
        point = Mathf.CeilToInt(volume / 100f);
    }


    void Update()
    {

    }


    public bool Broken(float atk, out int point)
    {
        point = 0;
        volume -= atk;
        if (volume > 0) { return false; }
        transform.DOMoveY(10, 1).SetRelative().SetEase(Ease.OutBack);
        transform.DOLocalRotate(new Vector3(180, 0, 180), 0.5f).SetRelative().SetLoops(-1).SetEase(Ease.Linear);
        col.enabled = false;
        DOVirtual.DelayedCall(1, () =>
        {
            gameObject.SetActive(false);
        });
        GameManager.i.explosionManager.Explosion(transform);

        point = this.point;
        return true;
    }
}
