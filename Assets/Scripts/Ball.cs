using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;
using Object = UnityEngine.Object;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private Vector2 previousPosition;
    //Thrust
    public float m_Thrust = 70f;
    public float bouncerThrust = 50;
    public float miniBoncerThrustEXP = 2000;
    public float jackPointThrust = 100;
    //Points
    [SerializeField] private int points;
    [SerializeField] private int currentPoints;
    [SerializeField] private int bouncerPoints = 1000;
    [SerializeField] private int MiniBouncerPoints = 500;
    [SerializeField] private int jackPointPoints = 5000;

    private float ingameTimer = 0f;
    private int ingameLife = 3;
    private float deathCD = 10f;
    private int extra;
    private int extraLifeInterval = 30000; 

    [SerializeField] private TextMeshProUGUI textPoints;
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private TextMeshProUGUI textLife;
    //Size

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -25, 0);

        points = 0;
        ingameLife = 3;
        extra = extraLifeInterval;
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

    
    public void IncreaseScore(int amount)
    {
        points += amount;
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

                IncreaseScore(bouncerPoints); 
             }
             
             

             if (other.gameObject.tag=="MiniBoncer")
             {
                 Vector3 dir = other.contacts[0].point - transform.position;

                 dir = dir.normalized;
                 
                 //GetComponent<Rigidbody>().AddExplosionForce(bouncerThrust,transform.position,8);
                 GetComponent<Rigidbody>().AddForce(dir * miniBoncerThrustEXP,ForceMode.Impulse);

                 IncreaseScore(MiniBouncerPoints);
             }

             if (other.gameObject.tag=="Jackpot")
             {
                 Vector3 dir = other.contacts[0].point - transform.position;

                 dir = dir.normalized;
                 
                 GetComponent<Rigidbody>().AddForce(dir*jackPointThrust,ForceMode.Impulse);
                 points += jackPointPoints;
                 
                 IncreaseScore(5000);
             }

             if (other.gameObject.tag =="Out")
             {
                 if (deathCD > 10)
                 {
                    ingameLife--;
                    deathCD = 0;
                 }
                 if (ingameLife == 0)
                 {
                     SceneManager.LoadScene("MainMenu");
                 }
             }
             
             
    }
    
    
    

    private void Update()
    {
        deathCD += Time.deltaTime;
        ingameTimer += Time.deltaTime;
        textTime.text = "Timer: " + Mathf.Round(ingameTimer);
        textLife.text = "Life: " + ingameLife; 
        if (currentPoints < points)
        {
            currentPoints += (int)(8500 * Time.deltaTime);
            if (currentPoints > points)
            {
                currentPoints = points;
            }

            textPoints.text = currentPoints.ToString("00000000");
            
        }

        if (points >= extra)
        {
            ingameLife += 1;
            extra += extraLifeInterval;
        }
        
        //Skriv forklarelse (ball spinner korrekt etter vinkel den treffer i korrekt akse)
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
