using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperL : MonoBehaviour
{
   [SerializeField] private AnimationCurve _animationCurveLeft;
    private float _timerL = 0.0f;
    private float _hitCD = 0.3f;
    private float _hitCDTimer = 0.0f;

    private void Update()
    {
        if (_hitCDTimer > _hitCD)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _timerL = 0;
            }

        }

        //Timers
        _timerL += Time.deltaTime;
        _hitCDTimer += Time.deltaTime;
        
        //rotatsjon logic
        float angleRotatin = _animationCurveLeft.Evaluate(_timerL) * 75;
        Vector3 localRotation = new Vector3(angleRotatin, 180, 0);
        //transform.localEulerAngles = localRotation;
        GetComponent<Rigidbody>().transform.localEulerAngles = localRotation;
    }
}
