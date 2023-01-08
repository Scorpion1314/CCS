using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetMouseButton(1))
        {
            CameraController.instance.transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }

    public void OnMouseDown()
    {
        CameraController.instance.followTransform = transform;
    }
}
