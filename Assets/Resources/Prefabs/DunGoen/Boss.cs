using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public RoomDataSO roomData;
    public GameObject boosObj;
    public GameObject playerObj;

    public Transform spawnPot;
    public Transform playerSpawnPot;

    public Image fill;


    public AudioClip bossMusic;
    
    private void Start()
    {
        SoundManager.Instance.ChangeBackGroundMusic(bossMusic,0.2f);
        StartCoroutine(BossAppearCo());
    }


  
  

    IEnumerator BossAppearCo()
    {
        yield return new WaitForSeconds(1f);
        BossMonster boss = Instantiate(boosObj, spawnPot.position, Quaternion.identity).GetComponent<BossMonster>();
        float curhealth = boss.bossHealth;

        while(boss.bossHealth > 0)
        {
            if(boss.bossHealth != curhealth)
            {
                curhealth = boss.bossHealth;
                fill.fillAmount = curhealth / 100;
            }

            yield return new WaitForSeconds(0.1f);
        }
        fill.fillAmount = 0;
    }

}
