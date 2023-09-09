using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    private float _timer = 0.0f;

    void Start()
    {
        
    }
    

    private void Update()
    {
       // Vector3 StartPosition = transform.position;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _timer = 0;

        }

        _timer += Time.deltaTime;
        
        float angleRotatin = _animationCurve.Evaluate(_timer);
        Vector3 startPosition = transform.position;
        //startPosition = new Vector3(startPosition.x, angleRotatin);
        transform.localPosition = new Vector3(startPosition.x, angleRotatin  - 8.22f, startPosition.z);
    }
}

