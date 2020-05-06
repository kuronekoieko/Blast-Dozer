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
    [SerializeField] ObstacleStatusController obstacleStatusPrefab;
    List<ObstacleStatusController> obstacleStatuses;
    public static GameCanvasManager i;

    public override void OnStart()
    {
        if (i == null) i = this;
        base.SetScreenAction(thisScreen: ScreenState.Game);

        this.ObserveEveryValueChanged(point => Variables.status.point)
            .Subscribe(point => { pointText.text = "★ " + point; })
            .AddTo(this.gameObject);

        gameObject.SetActive(true);
        obstacleStatuses = new List<ObstacleStatusController>();

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

    public void ShowPoint(Vector3 worldPos, int point)
    {
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(GameManager.i.cameraController.cameraShakeController.mainCamera, worldPos);
        var obstacleStatus = GetObstacleStatus();
        obstacleStatus.ShowPoint(screenPos, point);
    }

    public void ShowHp(Vector3 worldPos, float maxHp, float hp)
    {
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(GameManager.i.cameraController.cameraShakeController.mainCamera, worldPos);
        var obstacleStatus = GetObstacleStatus();
        obstacleStatus.ShowHp(screenPos, maxHp, hp);
    }


    ObstacleStatusController GetObstacleStatus()
    {
        var obstacleStatus = obstacleStatuses.Where(b => !b.gameObject.activeSelf).FirstOrDefault();
        if (obstacleStatus != null) return obstacleStatus;
        obstacleStatus = Instantiate(obstacleStatusPrefab, Vector3.zero, Quaternion.identity, transform);
        obstacleStatus.OnStart();
        obstacleStatuses.Add(obstacleStatus);
        return obstacleStatus;
    }
}
