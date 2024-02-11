using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class DunGoenManager : MonoBehaviour
{
    public static DunGoenManager Instance;
    public Transform container;

    [Header("Dungoen Generator")]
    public DungoenGenerator dungoenGenerator;
    public List<RoomData> dungoenRoomDataList = new List<RoomData>(); //
    public List<GameObject> colliderList = new List<GameObject>();

    [Header("Draw Tilemap")]
    public TileDraw tileDrawer;


    [Header("Game State")]
    public int curDungoenRoomNumber;

    [Header("Room Move Panel")]
    public GameObject panel;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        dungoenGenerator = GetComponent<DungoenGenerator>();
        tileDrawer = transform.Find("TileDrawer").GetComponent<TileDraw>();

        dungoenGenerator.ProcedurealDungoenGenerator();
    }
}
