using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ObstacleController : MonoBehaviour
{
    Collider col;
    MeshRenderer meshRenderer;
    float hp;
    float maxHp;
    int point;
    void Start()
    {
        col = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
        Vector3 size = meshRenderer.bounds.size;
        maxHp = size.x * size.y * size.z;
        point = Mathf.CeilToInt(maxHp / 100f);
        hp = maxHp;
    }


    void Update()
    {

    }


    public bool Broken(float atk, out int point)
    {
        point = 0;
        hp -= atk;
        if (hp > 0)
        {
            GameCanvasManager.i.ShowHp(transform.position, maxHp, hp);
            return false;
        }
        transform.DOMoveY(10, 1).SetRelative().SetEase(Ease.OutBack);
        transform.DOLocalRotate(new Vector3(180, 0, 180), 0.5f).SetRelative().SetLoops(-1).SetEase(Ease.Linear);
        col.enabled = false;
        DOVirtual.DelayedCall(1, () =>
        {
            gameObject.SetActive(false);
        });
        GameManager.i.explosionManager.Explosion(transform);

        point = this.point;
        GameCanvasManager.i.ShowPoint(transform.position, point);
        return true;
    }
}
