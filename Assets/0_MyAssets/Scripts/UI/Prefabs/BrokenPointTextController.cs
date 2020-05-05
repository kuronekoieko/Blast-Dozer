using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class BrokenPointTextController : MonoBehaviour
{
    RectTransform rectTransform;
    void Start()
    {

    }

    public void Show(Vector3 screenPos, int point)
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.position = screenPos;

        GetComponent<Text>().text = "+" + point;

        rectTransform.DOLocalMoveY(300, 1).SetRelative().OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
