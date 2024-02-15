using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    BoxCollider2D _collider;
    public ParticleSystem[] portalEffect;

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
        for (int i = 0; i < portalEffect.Length; i++)
        {
            portalEffect[i].Play();
        }

    }


    IEnumerator ClosePotalCo()
    {
        Managers.GameManager.day++;
        Debug.Log(Managers.GameManager.day);
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("Main");
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
