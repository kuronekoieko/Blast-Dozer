using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 3D空間の処理の管理
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] ExplosionManager _explosionManager;
    public ExplosionManager explosionManager { get { return _explosionManager; } }
    [SerializeField] CameraController _cameraController;
    public CameraController cameraController { get { return _cameraController; } }
    public static GameManager i;

    void Awake()
    {
    }

    void Start()
    {
        if (i == null) i = this;
        FirebaseAnalyticsManager.i.LogScreen("Game");
    }
}
