using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ObstacleController : MonoBehaviour
{
    Collider col;
    MeshRenderer meshRenderer;
    float volume;
    void Start()
    {
        col = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
        Vector3 size = meshRenderer.bounds.size;
        volume = size.x * size.y * size.z;
    }


    void Update()
    {

    }


    public bool Broken(float atk)
    {
        if (volume > atk) { return false; }
        transform.DOMoveY(10, 1).SetRelative().SetEase(Ease.OutBack);
        transform.DOLocalRotate(new Vector3(180, 0, 180), 0.5f).SetRelative().SetLoops(-1).SetEase(Ease.Linear);
        col.enabled = false;
        DOVirtual.DelayedCall(1, () =>
        {
            gameObject.SetActive(false);
        });
        GameManager.i.explosionManager.Explosion(transform);

        Debug.Log(volume);
        return true;
    }
}
