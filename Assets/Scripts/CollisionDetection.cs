using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private ParticleSystem destroyParticles; // particle system which plays when coin is collected
    private ScoreCounter scoreCounter;

    private int scoreAmount = 50;
   
    private void OnTriggerEnter(Collider other) // when other collider enter the trigger
    {
        if (other.tag == "Player") // only if its tag is Player
        {
            destroyParticles.transform.position = transform.position; // position particles at position of this gameobject
            destroyParticles.Play(); 
            scoreCounter.Score += scoreAmount; // add score
            gameObject.SetActive(false); // deactivate gameobject
        }
    }
    void Start()
    {
        destroyParticles = GameObject.Find("DestroyParticles").GetComponent<ParticleSystem>();
        scoreCounter = GameObject.Find("Counter").GetComponent<ScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 10, 0); // continuosly rotate gameobject
    }
}
