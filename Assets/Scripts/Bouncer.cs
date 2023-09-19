using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Bouncer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Vector3 miniBouncerSize = new Vector3(1.5f,1.5f,1.5f);
    [SerializeField] public float resetTimer = 0.15f;
    [SerializeField] public float timer;
    [SerializeField] public Vector3 startSize = new  Vector3(1f, 1f, 1f);
    [SerializeField] private bool ifHit;
    void Start()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "ball" )
        {
        transform.localScale = miniBouncerSize;

        timer = 0;
        ifHit = true;
        }

    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "ball")
        {
            if (timer >= resetTimer & ifHit) 
            Debug.Log("jflkajfalfjalkfjlaksjfla");
            transform.localScale = startSize;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
    }
}
