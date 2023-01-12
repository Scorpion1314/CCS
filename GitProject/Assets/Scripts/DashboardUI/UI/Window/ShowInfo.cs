using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowInfo : MonoBehaviour
{
    private TMP_Text buildingInfo;

    [SerializeField] private Transform focusedObject;

    private void Awake()
    {
        buildingInfo = GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Empty the text
        buildingInfo.text = string.Empty;
    }

    // Update is called once per frame
    private void Update()
    {
        // Detect focused Object
        focusedObject = CameraController.instance.followTransform;

        // Show the building info
        ShowBuildingInfo();
    }

    public void ShowBuildingInfo()
    {
         buildingInfo.text = "Building Name: " + focusedObject.name + "\n" +
                            "Position: " + focusedObject.transform.position;
    }
}
