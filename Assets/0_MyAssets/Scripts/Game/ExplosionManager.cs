using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] ParticleSystem particlePrefab;
    ParticleSystem[] expPSs;
    int index;
    void Start()
    {
        expPSs = new ParticleSystem[FindObjectsOfType<ObstacleController>().Length];
        for (int i = 0; i < expPSs.Length; i++)
        {
            expPSs[i] = Instantiate(particlePrefab, Vector3.zero, Quaternion.identity, transform);
            expPSs[i].gameObject.SetActive(false);
        }
        index = 0;
    }

    void Update()
    {

    }


    public void Explosion(Transform obstacleTF)
    {
        expPSs[index].transform.position = obstacleTF.position;
        expPSs[index].gameObject.SetActive(true);
        index++;
    }
}
