using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
using System;

/// <summary>
/// ゲーム画面
/// ゲーム中に表示するUIです
/// あくまで例として実装してあります
/// ボタンなどは適宜編集してください
/// </summary>
public class GameCanvasManager : BaseCanvasManager
{
    [SerializeField] Text pointText;
    [SerializeField] Text timerText;
    [SerializeField] ObstacleStatusController obstacleStatusPrefab;
    List<ObstacleStatusController> obstacleStatuses;
    public static GameCanvasManager i;
    float timer;
    float timeLimit = 120;

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
        CountDown();
    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);
    }

    protected override void OnClose()
    {
        // gameObject.SetActive(false);
    }

    public override void OnInitialize()
    {
        timer = timeLimit;
    }

    void CountDown()
    {
        timer -= Time.deltaTime;
        SetTimeCountText(timer);
        if (timer > 0) { return; }
        Variables.screenState = ScreenState.Clear;
    }

    /// <summary>
    /// 【C#】秒数をhh:mm:ss変換するには TimeSpan を使う
    /// https://qiita.com/Nossa/items/70487b765ec9332e0db0
    /// </summary>
    /// <param name="timer"></param>
    void SetTimeCountText(float timer)
    {
        // TimeSpanのインスタンスを生成。時分は0でOK
        TimeSpan span = new TimeSpan(0, 0, (int)timer);
        // フォーマットする
        string mmss = span.ToString(@"mm\:ss");
        timerText.text = mmss;
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
