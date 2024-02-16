using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private float _atk;
    private float _fireRate;
    private float _range;

    private float _curTime;

    private void Update()
    {
        _curTime += Time.deltaTime;
        if (_curTime > _fireRate)
        {
            _curTime = 0;
            ShootBullet();
        }
    }

    public void Setup()
    {

    }

    private void ShootBullet()
    {
        Bullet_Turret beam = Managers.RM.Instantiate("Projectiles/Bullet_Turret").GetComponent<Bullet_Turret>();
        
    }
}
