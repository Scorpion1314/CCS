using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowList : MonoBehaviour
{
    //private TMP_Text buildingList;

    // Variable for dropdown
    public TMP_Dropdown mydropdown;

    private string buildingname;

    [SerializeField] private GameObject buildingObjectTofocus;

    private void Awake()
    {
        //buildingList = GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    public void Start()
    {
        // Empty the list
        // buildingList.text = string.Empty;

        // Print out every building name   
        //for(int i = 0; i < GetList.getlist.BuildingList.Length; i++)
        //{
            // buildingList.text += "Building " + (i+1) + ". " + BuildingList[i].name + "\n";
            // Debug.Log("Building " + (i+1) + " is " + BuildingList[i].name);
            // Debug.Log("Building " + (i+1) + "'s position is " + BuildingList[i].transform.position);
        //}

        PopulateDropDown(mydropdown, GetList.getlist.BuildingList);
    }

    // Update is called once per frame
    private void Update()
    {
        mydropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(mydropdown);
        });
    }

    // Create options for the dropdown
    public void PopulateDropDown(TMP_Dropdown dropdown, GameObject[] optionsArray)
    {
        // List for Dropdown selections
        List<string> options = new List<string>();
        foreach (var option in optionsArray)
        {
            options.Add(option.name);
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    public void DropdownValueChanged(TMP_Dropdown change)
    {
        buildingname = mydropdown.options[mydropdown.value].text;
        buildingObjectTofocus = GameObject.Find(buildingname);
        CameraController.instance.followTransform = buildingObjectTofocus.transform;
    }
    
}
