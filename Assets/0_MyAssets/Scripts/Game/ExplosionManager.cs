using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] ParticleSystem particlePrefab;
    ParticleSystem[] particles;
    void Start()
    {
        particles = new ParticleSystem[1000];
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i] = Instantiate(particlePrefab, Vector3.zero, Quaternion.identity, transform);
            particles[i].gameObject.SetActive(false);
        }
        index = 0;
    }

    void Update()
    {

    }

    int index;
    public void Explosion(Transform obstacleTF)
    {

        int a = index;
        index++;
        particles[a].gameObject.SetActive(true);
        particles[a].Play();
        particles[a].transform.position = obstacleTF.position;
        //ps.transform.localScale = obstacleTF.localScale;
        DOVirtual.DelayedCall(2, () =>
        {
            particles[a].gameObject.SetActive(false);

        });
    }
}
