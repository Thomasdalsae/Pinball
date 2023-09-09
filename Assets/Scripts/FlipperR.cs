using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperR : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurveRight;
    private float _timerR = 0.0f;
    private float _hitCD = 0.3f;
    private float _hitCDTimer = 0.0f;

    private void Update()
    {
        if (_hitCDTimer > _hitCD)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                _timerR = 0;
            }

        }

        //Timers
        _timerR += Time.deltaTime;
        _hitCDTimer += Time.deltaTime;
        
        //rotatsjon logic
        float angleRotatin = _animationCurveRight.Evaluate(_timerR) * 75;
        Vector3 localRotation = new Vector3(angleRotatin, 0, 0);
        transform.localEulerAngles = localRotation;
        
    }
}
