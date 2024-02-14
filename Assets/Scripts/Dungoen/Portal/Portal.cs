using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    BoxCollider2D _collider;
    public ParticleSystem portalEffect;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        DunGoenManager.Instance.OnActivePortal += PotalOn;
    }



    public void PotalOn()
    {
        _collider.enabled = true;
        portalEffect.Play();
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Main");
        }
    }

}
