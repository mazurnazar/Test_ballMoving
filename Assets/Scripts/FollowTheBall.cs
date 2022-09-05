using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheBall : MonoBehaviour
{
    private GameObject ball;
    [SerializeField] private float xPos, yPos, zPos;
    // Start is called before the first frame update
    void Awake()
    {
        
        ball = GameObject.Find("Ball");
      
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // change position of camere according to the ball position
        transform.position = new Vector3(ball.transform.position.x - 15, 10, ball.transform.position.z + 1);
    }
}
