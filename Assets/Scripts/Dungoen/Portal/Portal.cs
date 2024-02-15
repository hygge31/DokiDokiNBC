using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class Portal : MonoBehaviour
{
    BoxCollider2D _collider;
    public ParticleSystem[] portalEffect;
    public SpriteRenderer mainSprite;

    public AudioClip portalOpenClip;

    Animator animator;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        DunGoenManager.Instance.OnActivePortal += PotalOn;
    }



    public void PotalOn()
    {
        _collider.enabled = true;
        SoundManager.Instance.PlayClip(portalOpenClip);
        mainSprite.color = Color.blue;
        for (int i = 0; i < portalEffect.Length; i++)
        {
            portalEffect[i].Play();
        }

    }


    IEnumerator ClosePotalCo()
    {
        Managers.GameManager.day++;
        Debug.Log(Managers.GameManager.day);
        SoundManager.Instance.PlayClip(portalOpenClip);
        animator.SetTrigger("Close");
        
        yield return new WaitForSeconds(1f);
        portalEffect[0].Stop();
        portalEffect[1].Stop();
        DunGoenManager.Instance.PanelFadeOut();
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.ChangeBackGroundMusic(SoundManager.Instance.clip, 0.5f);
        Managers.Scene.LoadScene(Define.Scenes.Main);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(ClosePotalCo());
        }
    }

}
