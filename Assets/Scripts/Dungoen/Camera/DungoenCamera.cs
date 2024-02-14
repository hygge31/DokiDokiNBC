using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungoenCamera : MonoBehaviour
{
    public Transform playerTrnasform;

    public Vector2 center;
    public Vector2 minCamLimit;
    public Vector2 maxCamLimit;


    private void Awake()
    {
        DunGoenManager.Instance.OnMoveToDungoenRoom += SetCamLimit;
    }

    private void Start()
    {
        playerTrnasform = DunGoenManager.Instance.playerTransform;
    }


    private void Update()
    {
        Vector3 pot = Vector3.Lerp(transform.position, playerTrnasform.position, 0.1f);

        transform.position = new Vector3(Mathf.Clamp(pot.x, minCamLimit.x, maxCamLimit.x),
                                         Mathf.Clamp(pot.y, minCamLimit.y, maxCamLimit.y)) + (Vector3)center;
    }

    public void SetCamLimit(RoomData roomData)
    {
        center = roomData.center;
        minCamLimit = roomData.minCamLimit;
        maxCamLimit = roomData.maxCamLimit;

    }










}
