using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class TileDraw : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private Tilemap wallTilemap;

    [SerializeField] private List<TileBase> floorTileList = new List<TileBase>(); 

    [SerializeField] private TileBase wallTop;
    [SerializeField] private TileBase wallBottom;
    [SerializeField] private TileBase wallLeft;
    [SerializeField] private TileBase wallRight;

    [SerializeField] private TileBase wallTRCurve;
    [SerializeField] private TileBase wallTLCurve;
    [SerializeField] private TileBase wallDRCurve;
    [SerializeField] private TileBase wallDLCurve;





    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }



    public void DrawFloorTiles()
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




    public void DrawWallTile()
    {
        Queue<Vector2Int> drawWallList = new Queue<Vector2Int>();
        List<BoundsInt> bounds = DunGoenManager.Instance.dungoenRoomDataList.Select(c => c.bounds).ToList();
        foreach (BoundsInt bound in bounds)
        {
            Vector2Int min = (Vector2Int)bound.min;
            Vector2Int max = (Vector2Int)bound.max;

            for (int x = 0; x < bound.size.x; x++) //Bottom
            {
                Vector2Int newPot = (Vector2Int)bound.min + new Vector2Int(x, -1);
                drawWallList.Enqueue(newPot);
            }
            while(drawWallList.Count != 0) { DrawTile(wallTilemap, drawWallList.Dequeue(), wallBottom); }

            for (int x = 0; x < bound.size.x; x++) //Top
            {
                Vector2Int newPot = (Vector2Int)bound.min + new Vector2Int(x, bound.size.y);
                drawWallList.Enqueue(newPot);
            }
            while (drawWallList.Count != 0) { DrawTile(wallTilemap, drawWallList.Dequeue(), wallTop); }

            for (int y = 0; y < bound.size.y; y++) //Left
            {
                Vector2Int newPot = (Vector2Int)bound.min + new Vector2Int(-1, y);
                drawWallList.Enqueue(newPot);
            }
            while (drawWallList.Count != 0) { DrawTile(wallTilemap, drawWallList.Dequeue(), wallLeft); }

            for (int y = 0; y < bound.size.y; y++) //Right
            {
                Vector2Int newPot = (Vector2Int)bound.min + new Vector2Int(bound.size.x, y);
                drawWallList.Enqueue(newPot);
            }
            while (drawWallList.Count != 0) { DrawTile(wallTilemap, drawWallList.Dequeue(), wallRight); }

        }


    }


    public void DrawTile(Tilemap tilemap,Vector2Int drawPoint,TileBase tile)
    {
        Vector3Int position = tilemap.WorldToCell((Vector3Int)drawPoint);
        tilemap.SetTile(position, tile);
    }

    






}
