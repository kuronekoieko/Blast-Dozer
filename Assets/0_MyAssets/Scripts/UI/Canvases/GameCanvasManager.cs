using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;

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
    List<BrokenPointTextController> brokenPoints;
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
        brokenPoints = new List<BrokenPointTextController>();

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
        var brokenPoint = GetBrokenPoint();
        brokenPoint.Show(screenPos, point);
    }


    BrokenPointTextController GetBrokenPoint()
    {
        var brokenPoint = brokenPoints.Where(b => !b.gameObject.activeSelf).FirstOrDefault();
        if (brokenPoint != null) return brokenPoint;
        brokenPoint = Instantiate(brokenPointTextPrefab, Vector3.zero, Quaternion.identity, transform);
        brokenPoint.OnStart();
        brokenPoints.Add(brokenPoint);
        return brokenPoint;
    }
}
