using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    // To check the placed is placed by other object
    public bool Placed {get; private set; }
    // To check the size of the building (in this case shape)
    public Vector3Int Size {get; private set; }

    // List for the vertices of the object
    private Vector3[] Vertices;

    private void GetColliderVertexPositionsLocal()
    {
        BoxCollider Box = gameObject.GetComponent<BoxCollider>();
        Vertices = new Vector3[4];
        Vertices[0] = Box.center + new Vector3(-Box.size.x, -Box.size.y, -Box.size.z) * 0.5f;
        Vertices[1] = Box.center + new Vector3(Box.size.x, -Box.size.y, -Box.size.z) * 0.5f;
        Vertices[2] = Box.center + new Vector3(Box.size.x, -Box.size.y, Box.size.z) * 0.5f;
        Vertices[3] = Box.center + new Vector3(-Box.size.x, -Box.size.y, Box.size.z) * 0.5f;
    }

    private void CalculateSizeInCells()
    {
        Vector3Int[] vertices = new Vector3Int[Vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(Vertices[i]);
            vertices[i] = BuildingSystem.BSystem.gridLayout.WorldToCell(worldPos);
        }

        Size = new Vector3Int(Math.Abs((vertices[0] - vertices[1]).x), Math.Abs((vertices[0] - vertices[3]).y), 1);
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(Vertices[0]);
    }

    // Start is called before the first frame update
    private void Start()
    {
        GetColliderVertexPositionsLocal();
        CalculateSizeInCells();
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0, 90, 0));
        Size = new Vector3Int(Size.y, Size.x, 1);

        Vector3[] vertices = new Vector3[Vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = Vertices[(i + 1) % Vertices.Length];
        }

        Vertices = vertices;
    }

    public virtual void Place()
    {
        ObjectDrag Drag = gameObject.GetComponent<ObjectDrag>();
        Destroy(Drag);

        BuildingController BController = gameObject.AddComponent<BuildingController>();

        Placed = true;
    }
}
