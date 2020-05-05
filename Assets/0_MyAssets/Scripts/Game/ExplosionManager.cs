﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] ParticleSystem particlePrefab;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Explosion(Transform obstacleTF)
    {
        ParticleSystem ps = Instantiate(particlePrefab, obstacleTF.position, Quaternion.identity, transform);
        ps.transform.localScale = obstacleTF.localScale;
    }
}