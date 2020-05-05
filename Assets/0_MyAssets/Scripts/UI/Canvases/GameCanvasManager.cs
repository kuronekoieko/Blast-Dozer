using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// ゲーム画面
/// ゲーム中に表示するUIです
/// あくまで例として実装してあります
/// ボタンなどは適宜編集してください
/// </summary>
public class GameCanvasManager : BaseCanvasManager
{
    [SerializeField] Text pointText;
    [SerializeField] BrokenPointTextController brokenPointTextPrefab;
    public Canvas canvas;
    public static GameCanvasManager i;

    public override void OnStart()
    {
        if (i == null) i = this;
        base.SetScreenAction(thisScreen: ScreenState.Game);

        this.ObserveEveryValueChanged(point => Variables.status.point)
            .Subscribe(point => { pointText.text = "★ " + point; })
            .AddTo(this.gameObject);

        gameObject.SetActive(true);

    }

    public override void OnUpdate()
    {
        if (!base.IsThisScreen()) { return; }

    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);
    }

    protected override void OnClose()
    {
        // gameObject.SetActive(false);
    }

    public void ShowAddPoint(int point, Vector3 worldPos)
    {

        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(GameManager.i.cameraController.cameraShakeController.mainCamera, worldPos);
        var bptc = Instantiate(brokenPointTextPrefab, Vector3.zero, Quaternion.identity, transform);
        bptc.Show(screenPos, point);
    }
}
