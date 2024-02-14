using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _impactParticleSystem;

    public static ProjectileManager instance;

    private ObjectPool objectPool;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        objectPool = GetComponent<ObjectPool>();
    }


    //public void ShootBullet(Vector2 startPostiion, AttackSO attackData)
    //{
    //    GameObject obj = objectPool.SpawnFromPool(attackData.bulletNameTag);

    //    obj.transform.position = startPostiion;
    //    obj.SetActive(true);
    //}

}
