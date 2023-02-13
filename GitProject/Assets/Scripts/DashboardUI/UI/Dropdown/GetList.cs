using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetList : MonoBehaviour
{
    public static GetList getlist;
    
    // Array for list of buildings
    public GameObject[] BuildingList;

    private void Awake()
    {
        getlist = this;
        
        // Find buildings (3d objects) with the tag "Building"
        // BuildingList = GameObject.FindGameObjectsWithTag("Building");
    }
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateList();
    }

    private void UpdateList()
    {
        // Find buildings (3d objects) with the tag "Building"
        BuildingList = GameObject.FindGameObjectsWithTag("Building");
    }
}
