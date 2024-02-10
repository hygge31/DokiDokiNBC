using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class DunGoenManager : MonoBehaviour
{
    public static DunGoenManager Instance;


    [Header("Dungoen Generator")]
    public DungoenGenerator dungoenGenerator;
    public List<RoomData> dungoenRoomDataList = new List<RoomData>(); //

    [Header("Draw Tilemap")]
    public TileDraw tileDrawer;



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
