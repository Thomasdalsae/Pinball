using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class Flipper : MonoBehaviour
{
    [SerializeField] private float hitStrength = 1000000;
    [SerializeField] private float dampening = 250f;
    [SerializeField] private HingeJoint hingeJointLeft;
    [SerializeField] private HingeJoint hingeJointRight;

    private JointSpring jointSpringReleased = new();
    private JointSpring jointSpringPressed = new();
    
    private bool leftFlipperPressed, rightFlipperPressed;
    // Start is called before the first frame update
    void Start()
    {
        jointSpringPressed.spring = jointSpringReleased.spring = hitStrength;
        jointSpringPressed.damper = jointSpringReleased.damper = dampening;
        jointSpringPressed.targetPosition = hingeJointLeft.limits.max;
        jointSpringReleased.targetPosition = hingeJointLeft.limits.min;
    }

    private void OnLeftFlipper(InputValue value)
    {
        leftFlipperPressed = value.isPressed;
    }

    private void OnRightFlipper(InputValue value)
    {
        rightFlipperPressed = value.isPressed;
    }
  
    // Update is called once per frame
    void Update()
    {
        if (leftFlipperPressed)
        {
            hingeJointLeft.spring = jointSpringPressed;
        }
        else
        {
            
            hingeJointLeft.spring = jointSpringReleased;
        }
        
        if (rightFlipperPressed)
        {
            hingeJointRight.spring = jointSpringPressed;
        }
        else
        {
            hingeJointRight.spring = jointSpringReleased;
        }
    }
}
