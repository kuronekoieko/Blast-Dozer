using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
using System;
using DG.Tweening;

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
    [SerializeField] Text levelText;
    [SerializeField] Text levelUpText;
    [SerializeField] ObstacleStatusController obstacleStatusPrefab;
    [SerializeField] RectTransform tutrials;
    [SerializeField] RectTransform fingerPoint;
    List<ObstacleStatusController> obstacleStatuses;
    public static GameCanvasManager i;
    float timer;
    float timeLimit = 60;
    float angularVelocity = 7;

    float animTimer;
    bool isStart;
    Sequence sequence;

    void Awake()
    {
        obstacleStatuses = new List<ObstacleStatusController>();
    }

    public override void OnStart()
    {
        if (i == null) i = this;
        base.SetScreenAction(thisScreen: ScreenState.Game);

        this.ObserveEveryValueChanged(point => Variables.status.point)
            .Subscribe(point => { pointText.text = "★ " + point; })
            .AddTo(this.gameObject);

        this.ObserveEveryValueChanged(level => Variables.status.growthIndex + 1)
            .Subscribe(level =>
            {
                levelUpTextAnim(level);
                levelText.text = "Player\nLv." + level;
            })
            .AddTo(this.gameObject);

        gameObject.SetActive(true);
    }

    public override void OnUpdate()
    {
        if (!base.IsThisScreen()) { return; }


        if (Input.GetMouseButtonDown(0))
        {
            tutrials.gameObject.SetActive(false);
            isStart = true;
        }

        if (isStart)
        {
            CountDown();
        }
        else
        {
            FingerAnim();
        }
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
        levelUpText.gameObject.SetActive(false);
        isStart = false;
        SetTimeCountText(timer);
        tutrials.gameObject.SetActive(true);
        ObstacleStatusesGenerator();
    }

    void ObstacleStatusesGenerator()
    {
        int obsCount = FindObjectsOfType<ObstacleController>().Length;
        for (int i = obstacleStatuses.Count; i < obsCount; i++)
        {
            var obstacleStatus = Instantiate(obstacleStatusPrefab, Vector3.zero, Quaternion.identity, transform);
            obstacleStatus.OnStart();
            obstacleStatuses.Add(obstacleStatus);
        }
    }

    void CountDown()
    {
        timer -= Time.deltaTime;
        SetTimeCountText(timer);
        if (timer > 0) { return; }
        Variables.screenState = ScreenState.Clear;
    }

    void FingerAnim()
    {
        animTimer += Time.deltaTime;
        float y = -330f + 70f * Mathf.Sin(animTimer * angularVelocity);
        float x = 170 * Mathf.Sin(animTimer * angularVelocity / 2);
        fingerPoint.anchoredPosition = new Vector3(x, y, 0);
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
        return obstacleStatus;
    }

    void levelUpTextAnim(int level)
    {
        if (level == 1) { return; }
        sequence.Kill(true);
        levelUpText.gameObject.SetActive(true);
        levelUpText.transform.localScale = Vector3.zero;
        Color c = levelUpText.color;

        sequence = DOTween.Sequence()
        .Append(levelUpText.transform.DOScale(new Vector3(1, 1, 1), 1).SetEase(Ease.OutElastic))
        .Append(DOTween.ToAlpha(() => levelUpText.color, color => levelUpText.color = color, 0f, 1f))
        .OnComplete(() =>
        {
            levelUpText.gameObject.SetActive(false);
            levelUpText.color = c;
        });
    }
}
