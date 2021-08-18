using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;

    private void Awake()
    {
        target = GameObject.FindWithTag("CameraPoint").transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target!= null)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;

        }
    }
}
