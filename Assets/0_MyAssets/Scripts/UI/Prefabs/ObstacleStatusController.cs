using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ObstacleStatusController : MonoBehaviour
{
    [SerializeField] Text pointText;
    [SerializeField] Slider hpSlider;
    RectTransform rectTransform;

    public void OnStart()
    {
        rectTransform = GetComponent<RectTransform>();
        pointText.gameObject.SetActive(false);
        hpSlider.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ShowPoint(Vector3 screenPos, int point)
    {
        gameObject.SetActive(true);
        pointText.gameObject.SetActive(true);
        rectTransform.position = screenPos;
        pointText.text = "+" + point;

        rectTransform
        .DOLocalMoveY(300, 1)
        .SetRelative()
        .OnComplete(() =>
        {
            pointText.gameObject.SetActive(false);
            gameObject.SetActive(false);
        });
    }

    public void ShowHp(Vector3 screenPos, float maxHp, float hp)
    {
        hpSlider.maxValue = maxHp;
        hpSlider.minValue = 0;
        hpSlider.value = hp;

        gameObject.SetActive(true);
        hpSlider.gameObject.SetActive(true);
        rectTransform.position = screenPos;
        DOVirtual.DelayedCall(1, () =>
        {
            hpSlider.gameObject.SetActive(false);
            gameObject.SetActive(false);
        });
    }
}
