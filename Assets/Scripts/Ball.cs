using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private Vector2 previousPosition;
    //Thrust
    public float m_Thrust = 10000f;
    public float bouncerThrust = 1000;
    public float miniBoncerThrust = 500;
    public float jackPointThrust = 2000;
    //Points
    [SerializeField] public float points = 0;
    [SerializeField] public int bouncerPoints = 50;
    [SerializeField] public int MiniBouncerPoints = 25;
    [SerializeField] public int jackPointPoints = 500;
    //Size

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider Object)
              {
                  Debug.Log("did hit Object");
                  
                  if (Object.tag == "shooter")
                  {
                      Debug.Log("Hit: " + Object.transform.name);
                      m_Rigidbody.AddForce(0,m_Thrust,0,ForceMode.Impulse); 
                  }
                
                   
              }

    private void OnCollisionEnter(Collision other)
    {
            Debug.Log("Points: "+points + other.transform.name);
             if (other.gameObject.tag=="bouncer")
             {
                 Debug.Log("Hit: " + other.transform.name);
                 Vector3 dir = other.contacts[0].point - transform.position;

                 dir = dir.normalized;
                 
                 GetComponent<Rigidbody>().AddForce(dir*bouncerThrust,ForceMode.Impulse);

                 points += bouncerPoints;
             }

             if (other.gameObject.tag=="MiniBoncer")
             {
                 Vector3 dir = other.contacts[0].point - transform.position;

                 dir = dir.normalized;
                 
                 GetComponent<Rigidbody>().AddForce(dir*miniBoncerThrust,ForceMode.Impulse);

                 points += MiniBouncerPoints;
             }

             if (other.gameObject.tag=="Jackpot")
             {
                 Vector3 dir = other.contacts[0].point - transform.position;

                 dir = dir.normalized;
                 
                 GetComponent<Rigidbody>().AddForce(dir*jackPointThrust,ForceMode.Impulse);
                 points += jackPointPoints;
             }
             
    }

    private void Update()
    {//Skriv forklarelse (ball spinner korrekt etter vinkel den treffer i korrekt akse)
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        //calculates the object's movement direction and speed between frames
        Vector2 speed = position - previousPosition;
        //used to determine the rotation axis for an object based on its movement direction.
        Vector2 rotationAxis = Vector2.Perpendicular(speed);
        //This determines the axis around which the object will rotate. rotating in the opposite direction of its movement
        transform.Rotate(new Vector3(rotationAxis.x, rotationAxis.y,0),-speed.magnitude * 35, Space.World);
        //line updates the previousPosition variable to match the current position. This is important for calculating the speed in the next frame.
        previousPosition = position;
    }
}
