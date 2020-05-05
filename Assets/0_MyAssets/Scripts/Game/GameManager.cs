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
    public static GameManager i;

    void Start()
    {
        if (i == null) i = this;
    }
}
