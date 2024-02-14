using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform playerPosition;

    private void Awake()
    {
        DunGoenManager.Instance.OnChangeMinimap += SetCameraPosition;
    }

    private void Start()
    {
        playerPosition = DunGoenManager.Instance.playerTransform;
    }


    public void SetCameraPosition()
    {
        transform.position = playerPosition.position -Vector3.forward;
    }
}
