using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathSound : MonoBehaviour
{
    BossMonster bossMonster;
    // Start is called before the first frame update
    void Start()
    {
        bossMonster = GetComponentInParent<BossMonster>();
    }
    public void PlayBossDeathSound()
    {
        SoundManager.Instance.PlayClip(bossMonster.deathClip);
    }
}
