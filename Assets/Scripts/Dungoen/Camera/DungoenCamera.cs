using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DungoenCamera : MonoBehaviour
{
    public Transform playerTrnasform;

    public Vector2 center;
    public Vector2 minCamLimit;
    public Vector2 maxCamLimit;

    public Vector3 offset;

    private void Awake()
    {
        DunGoenManager.Instance.OnMoveToDungoenRoom += SetCamLimit;
    }

    private void Start()
    {
        
        playerTrnasform = GameObject.Find("Player(Clone)").transform;
    
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

    public void SetCamLimit(RoomDataSO roomData)
    {

        center = Vector2.zero;
        int width = roomData.width;
        int height = roomData.height;

        Vector2 minCamLimit = new Vector2(center.x - width / 2, center.y - height / 2) + new Vector2(DunGoenManager.Instance.cameraWidth, DunGoenManager.Instance.cameraHeight);
        Vector2 maxCamLimit = new Vector2(center.x + width / 2, center.y + height / 2) - new Vector2(DunGoenManager.Instance.cameraWidth, DunGoenManager.Instance.cameraHeight);


        this.minCamLimit = minCamLimit;
        this.maxCamLimit = maxCamLimit;

    }









}
