using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    // Create static variable for singleton
    public static BuildingSystem BSystem;

    // Variable for grid
    public GridLayout gridLayout;
    private Grid grid;

    // Variable for tilemap
    [SerializeField] private Tilemap MainTilemap;
    [SerializeField] private TileBase whiteTile;

    // Variable for building (in this case 3D objects)
    public GameObject cube;
    public GameObject sphere;

    // Variable for building (in this case Houses)
    public GameObject House1;
    public GameObject House2;

    private PlaceableObject objectToPlace;

    private void Awake()
    {
        BSystem = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            InitializeWithObject(cube);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            InitializeWithObject(sphere);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            InitializeWithObject(House1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            InitializeWithObject(House2);
        }

        if(!objectToPlace)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            objectToPlace.Rotate();
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            if(CanBePlaced(objectToPlace))
            {
                objectToPlace.Place();
                Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                TakeArea(start, objectToPlace.Size);
            }
            else
            {
                Destroy(objectToPlace.gameObject);
            }
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;
        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++; 
        }

        return array;
    }

    public void InitializeWithObject(GameObject shape)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject obj = Instantiate(shape, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
    }

    private bool CanBePlaced(PlaceableObject placeableObject)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        area.size = placeableObject.Size;
        area.size = new Vector3Int(area.size.x + 1, area.size.y + 1, area.size.z);

        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);

        foreach (var b in baseArray)
        {
            if(b == whiteTile)
            {
                return false;
            }
        }

        return true;
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        MainTilemap.BoxFill(start, whiteTile, start.x, start.y, start.x + size.x, start.y + size.y);
    }
}
