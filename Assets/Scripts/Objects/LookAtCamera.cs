using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
            return;
        }
        
        transform.LookAt(_camera.transform);
    }
}
