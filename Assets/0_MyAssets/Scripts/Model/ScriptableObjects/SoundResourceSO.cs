﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create GrowthDataSO", fileName = "GrowthDataSO")]
public class GrowthDataSO : ScriptableObject
{
    public GrowthData[] growthDatas;

    private static GrowthDataSO _i;
    public static GrowthDataSO i
    {
        get
        {
            string PATH = "ScriptableObjects/" + nameof(GrowthDataSO);
            //初アクセス時にロードする
            if (_i == null)
            {
                _i = Resources.Load<GrowthDataSO>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_i == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _i;
        }
    }
}

[System.Serializable]
public class GrowthData
{
    public int minPoint;
    public float scale;
}