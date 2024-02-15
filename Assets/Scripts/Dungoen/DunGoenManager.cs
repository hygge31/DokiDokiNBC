using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class DunGoenManager : MonoBehaviour
{
    public static DunGoenManager Instance;
    public GameObject container;

    [Header("Dungoen Generator")]
    public DungoenGenerator dungoenGenerator;
    public List<RoomData> dungoenRoomDataList = new List<RoomData>(); 
    public List<GameObject> minimapSpriteList = new List<GameObject>();

    [Header("Player")]
    public Transform playerTransform;

    [Header("Game State")]
    public int curDungoenRoomNumber;
    public int count; // init
    [Header("Room Move Panel")]
    public GameObject panel;

    [Header("Minimap part")]
    public GameObject minimapUi;
    public GameObject minimapCamera;

    [Header("Camera")]
    public Camera _camera;
    public float cameraWidth;
    public float cameraHeight;

    [Header("Boss")]
    public Boss boss;

    [Header("UI")]
    public Image panelImg;
    Color orgColor;


    [Header("Audio")]
    public AudioClip dungoenBgm;
    public AudioClip doorAudio;

    public event Action OnChangeMinimap;
    public event Action<RoomData> OnMoveToDungoenRoom;
    public event Action OnActivePortal;

    private void Awake()
    {
        Instance = this;

        _camera = Camera.main;
        

        cameraWidth = _camera.orthographicSize * _camera.aspect - 1;
        cameraHeight = _camera.orthographicSize -2;
        orgColor = panelImg.color;

        Instantiate(playerTransform);
    }

    private void Start()
    {
        StartCoroutine(PanelFadeInCo());

        if(Managers.GameManager.day >= 4)
        {
            Instantiate(boss);
            _camera.GetComponent<DungoenCamera>().SetCamLimit(boss.roomData);
        }
        else
        {
            SoundManager.Instance.ChangeBackGroundMusic(dungoenBgm);

            Instantiate(minimapCamera);
            Instantiate(minimapUi);

            CreateDunGoen();
            DungoenAllDoorAppear();
            CallOnMoveToDungoenRoom(dungoenRoomDataList[0]);
        }

        
        
    }


    private void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.Space)){
            DungoenAllDoorAppear();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            CallOnActivePortal();
        }
        //test
    }


    public void CallOnChangeMinimap()
    {
        OnChangeMinimap?.Invoke();
    }
    public void CallOnMoveToDungoenRoom(RoomData roomData)
    {
        OnMoveToDungoenRoom?.Invoke(roomData);
    }
    public void CallOnActivePortal()
    {
        OnActivePortal?.Invoke();
    }


    public void CreateDunGoen()
    {
        if(container != null)
        {
            DestroyImmediate(container.gameObject);
        }

        GameObject newContainer = new GameObject("Container");
        newContainer.transform.SetParent(gameObject.transform);
        container = newContainer;

        ClearList();

        dungoenGenerator.ProcedurealDungoenGenerator();
        minimapSpriteList[0].GetComponent<MinimapSprite>().CurPosition();
        
    }


    public void DungoenAllDoorExit()
    {
        foreach(RoomData room in dungoenRoomDataList)
        {
            room.ExitAllDoor();
        }
    }

    public void DungoenAllDoorAppear()
    {
        SoundManager.Instance.PlayClip(doorAudio);

        foreach (RoomData room in dungoenRoomDataList)
        {
            room.AppearAllDoor();
        }
    }


    public void ClearList()
    {
        dungoenRoomDataList.Clear();
        minimapSpriteList.Clear();
    }
   

    public void MoveToDungoen(int curRoomNumber ,int nextRoomNumber)
    {
        StartCoroutine(PanelFadeOutCo());
        StartCoroutine(PanelFadeInCo());
        if (!dungoenRoomDataList[nextRoomNumber].clear)
        {
            count++;
        }


        curDungoenRoomNumber = nextRoomNumber;
        dungoenRoomDataList[nextRoomNumber].SpawnMonster();
        minimapSpriteList[curRoomNumber].GetComponent<MinimapSprite>().OutPoisition();
        minimapSpriteList[nextRoomNumber].GetComponent<MinimapSprite>().CurPosition();
        CallOnChangeMinimap();
        CallOnMoveToDungoenRoom(dungoenRoomDataList[nextRoomNumber]);

        if (!dungoenRoomDataList[nextRoomNumber].clear)
        {
            DungoenAllDoorExit();
        }

    }


    public void DieMonster()
    {
        dungoenRoomDataList[curDungoenRoomNumber].DieMonsterAddAndClearCheck();
    }

    public void PanelFadeOut()
    {
        StartCoroutine(PanelFadeOutCo());
    }
    public void PanelFadeIn()
    {
        StartCoroutine(PanelFadeInCo());
    }


    IEnumerator PanelFadeInCo()
    {
        float percent = 0;
        Color targetCol = orgColor;
        targetCol.a = 0;
        while(percent < 1)
        {
            percent += Time.deltaTime;

            panelImg.color = Color.Lerp(orgColor, targetCol, percent);
            yield return null;
        }
    }

    IEnumerator PanelFadeOutCo()
    {
        float percent = 0;
        Color targetCol = orgColor;
        targetCol.a = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime;

            panelImg.color = Color.Lerp(targetCol, orgColor, percent);
            yield return null;
        }
    }

    public void CreateItem(Transform pot)
    {
        System.Random random = new System.Random();
        string[] itemNames = Enum.GetNames(typeof(Define.Weapons));
        Item_SO itemSO = Resources.Load<Item_SO>($"Scriptable/{itemNames[random.Next(1, itemNames.Length)]}");

        Item_Weapon weapon = Managers.RM.Instantiate($"Items/Item_Weapon").GetComponent<Item_Weapon>();
        weapon.Setup(itemSO, pot);
    }

}
