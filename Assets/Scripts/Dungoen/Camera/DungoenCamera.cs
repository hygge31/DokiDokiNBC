using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DungoenCamera : MonoBehaviour
{
    Camera _camera;

    public Transform playerTrnasform;

    public Vector2 center;
    public Vector2 minCamLimit;
    public Vector2 maxCamLimit;

    public Vector3 offset;

    private void Awake()
    {
      
    }

    private void Start()
    {
        playerTrnasform = GameObject.Find("Player(Clone)").transform;
        DunGoenManager.Instance.OnMoveToDungoenRoom += SetCamLimit;
    
    }


    private void Update()
    {

        transform.position = new Vector3(Mathf.Clamp(playerTrnasform.position.x, minCamLimit.x, maxCamLimit.x),
                                         Mathf.Clamp(playerTrnasform.position.y, minCamLimit.y, maxCamLimit.y)) + offset;
      
    }

    public void SetCamLimit(RoomData roomData)
    {
        Debug.Log(roomData.center);
        center = roomData.center;
        minCamLimit = roomData.minCamLimit;
        maxCamLimit = roomData.maxCamLimit;

    }










}
