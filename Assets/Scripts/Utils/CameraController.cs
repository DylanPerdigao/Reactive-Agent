using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        
        //Debug.Log(1.0f / Time.deltaTime);
    }
}
