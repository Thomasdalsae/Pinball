using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Bouncer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Vector3 miniBouncerSize = new Vector3(0.1f,0.1f,0.1f);
    [SerializeField] public float resetTimer = 1.5f;
    [SerializeField] public float timer;
    [SerializeField] public Vector3 startSize;
    void Start()
    {
        startSize = transform.localScale;
    }

    private void OnCollisionEnter(Collision other)
    {
        
        transform.localScale = miniBouncerSize;

        timer = resetTimer;

    }
    // Update is called once per frame
    void Update()
    {
        timer =- Time.deltaTime;
        if (timer >= 0)
        {
            transform.localScale = startSize;
        }
    }
}
