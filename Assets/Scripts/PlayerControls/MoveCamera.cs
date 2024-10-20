using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPostion;

    void Start()
    {
        transform.position = cameraPostion.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPause)
            return;
        transform.position = cameraPostion.position;
    }
}
