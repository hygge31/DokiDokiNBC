using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class TileDraw : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap frontTilemap;


    [Header("TileBase")]
    [SerializeField] private List<TileBase> floorTileList = new List<TileBase>(); 

    [SerializeField] private TileBase wallTop1;
    [SerializeField] private TileBase wallTop2;
    [SerializeField] private TileBase wallBottom1;
    [SerializeField] private TileBase wallBottom2;
    [SerializeField] private TileBase wallLeft;
    [SerializeField] private TileBase wallRight;
    

    [SerializeField] private TileBase wallTRCurve;
    [SerializeField] private TileBase wallTLCurve;
    [SerializeField] private TileBase wallBRCurve;
    [SerializeField] private TileBase wallBLCurve;
    


    public void DrawAllTile()
    {
        Clear();
        DrawFloorTiles();
        DrawWallTile();
    }


    void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        frontTilemap.ClearAllTiles();
    }



    void DrawFloorTiles()
    {
        List<Vector2Int> drawPoint = GetDrawFloorPoint();
        foreach (Vector2Int point in drawPoint)
        {
            DrawTile(floorTilemap, point, floorTileList[Random.Range(0, floorTileList.Count)]);
        }
    }

    List<Vector2Int> GetDrawFloorPoint()
    {
        List<BoundsInt> bounds = DunGoenManager.Instance.dungoenRoomDataList.Select(c => c.bounds).ToList();
        List<Vector2Int> drawFloorList = new List<Vector2Int>();

        foreach (BoundsInt bound in bounds)
        {
            for (int x = 0; x < bound.size.x; x++)
            { 
                for (int y = 0; y < bound.size.y; y++)
                {
                    Vector2Int newPot = (Vector2Int)bound.min + new Vector2Int(x, y);
                    drawFloorList.Add(newPot);
                }
            }
        }

        return drawFloorList;
    }




    void DrawWallTile()
    {
        List<BoundsInt> bounds = DunGoenManager.Instance.dungoenRoomDataList.Select(c => c.bounds).ToList();
        foreach (BoundsInt bound in bounds)
        {
            Vector2Int min = (Vector2Int)bound.min;
            Vector2Int max = (Vector2Int)bound.max;

            for (int x = 0; x < bound.size.x; x++) //Bottom
            {
                Vector2Int newPot = (Vector2Int)bound.min + new Vector2Int(x, 0);
                Vector2Int newPot1 = (Vector2Int)bound.min + new Vector2Int(x, -1);
                DrawTile(frontTilemap, newPot, wallBottom1);
                DrawTile(wallTilemap, newPot1, wallBottom2);
            }
            
            for (int x = 0; x < bound.size.x; x++) //Top
            {
                Vector2Int newPot = (Vector2Int)bound.min + new Vector2Int(x, bound.size.y);
                Vector2Int newPot1 = (Vector2Int)bound.min + new Vector2Int(x, bound.size.y-1);
                DrawTile(wallTilemap, newPot, wallTop1);
                DrawTile(wallTilemap, newPot1, wallTop2);
            }
            
            for (int y = 0; y < bound.size.y; y++) //Left
            {
                Vector2Int newPot = (Vector2Int)bound.min + new Vector2Int(-1, y);
                DrawTile(wallTilemap, newPot, wallLeft);
            }
            
            for (int y = 0; y < bound.size.y; y++) //Right
            {
                Vector2Int newPot = (Vector2Int)bound.min + new Vector2Int(bound.size.x, y);
                DrawTile(wallTilemap, newPot, wallRight);
            }
            
            DrawTile(wallTilemap,(Vector2Int)bound.min+new Vector2Int(-1,0),wallBLCurve);
            DrawTile(wallTilemap, (Vector2Int)bound.min+new Vector2Int(-1,-1), wallBottom2);
            DrawTile(wallTilemap, (Vector2Int)bound.min + new Vector2Int(bound.size.x, 0), wallBRCurve);
            DrawTile(wallTilemap, (Vector2Int)bound.min + new Vector2Int(bound.size.x, -1), wallBottom2);
            DrawTile(wallTilemap, (Vector2Int)bound.min + new Vector2Int(-1, bound.size.y), wallTLCurve);
            DrawTile(wallTilemap, (Vector2Int)bound.min + new Vector2Int(bound.size.x, bound.size.y), wallTRCurve);
        }


    }


    void DrawTile(Tilemap tilemap,Vector2Int drawPoint,TileBase tile)
    {
        Vector3Int position = tilemap.WorldToCell((Vector3Int)drawPoint);
        tilemap.SetTile(position, tile);
    }

    






}
