using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class BrokenPointTextController : MonoBehaviour
{
    RectTransform rectTransform;
    Text text;
    public void OnStart()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<Text>();
    }

    public void Show(Vector3 screenPos, int point)
    {
        gameObject.SetActive(true);
        rectTransform.position = screenPos;
        text.text = "+" + point;

        rectTransform
        .DOLocalMoveY(300, 1)
        .SetRelative()
        .OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
