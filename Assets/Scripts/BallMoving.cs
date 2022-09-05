using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoving : MonoBehaviour
{
    private Vector3 startMousePos; // position of pressed ball
    private Vector3 endMousePos; // position to move for
    private Vector3 currentMousePos;
    public Vector3 direction; // direction to move the ball

    private bool canMove = false; // if ball is allowed to move
    private float speed = 0; // speed of ball moving
    private float speedMultiplier = 2;

    [SerializeField] private LineRenderer lineRenderer;

    private ParticleSystem hitParticle;

    private void OnMouseDown() // when mouse pressed the ball
    {
        startMousePos = FindMousePos();
    }
    private void OnMouseDrag() // while dragging the mouse activate and show the line - direction where to move the ball
    {
        currentMousePos = FindMousePos();
        lineRenderer.gameObject.SetActive(true);
        lineRenderer.SetPosition(0, new Vector3(transform.position.x, 0.01f,transform.position.z));
        lineRenderer.SetPosition(1, new Vector3(currentMousePos.x, 0.01f, currentMousePos.z));
    }
    private void OnMouseUp() // when mouse is released
    {
        endMousePos = FindMousePos();
        speed = CalculateSpeed() * speedMultiplier; // speed of move
        direction = (endMousePos - transform.position).normalized; // direction to move
        lineRenderer.gameObject.SetActive(false);
        canMove = true; // allowing ball to move
    }
    private void OnCollisionEnter(Collision collision) // when ball collides with walls
    {
        direction = Vector3.Reflect(direction, collision.contacts[0].normal); // change direction of movement as reflection from wall
        
        hitParticle.transform.position = transform.position; // put hit particlesystem to the position of ball
        hitParticle.transform.rotation = collision.transform.rotation; // sign same rotation as ball
        hitParticle.Play();//and play particle system

        StartCoroutine(Squish());
    }
    IEnumerator Squish() // when ball hits the wall squish it
    {
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        transform.localScale = new Vector3(Mathf.Abs(direction.z), 1, Mathf.Abs(direction.x)); // change localscale according to the direction
        yield return new WaitForSeconds(0.05f); // after 0.05 sec change localscale back to original
        transform.localScale = new Vector3(1, 1, 1f);
    }
   
    Vector3 FindMousePos() // find position of mouse where it hits the screen
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        else return Vector3.zero;
    }
    float CalculateSpeed() // calculating speed of ball depending on applied force to the ball(distance between start and end points)
    {
        return Vector3.Distance(endMousePos, startMousePos);
    }
    void MoveBall() // moving the ball
    {
        // change position according to direction of movement and speed
        transform.position += new Vector3(direction.x * Time.deltaTime * speed, 0, direction.z * Time.deltaTime * speed);
        transform.Rotate(10*direction.z,0,-10*direction.x); // rotate in direction of movement
        speed -= 0.1f; // decrease the speed
    }
    
   
    void Start()
    {
        lineRenderer.positionCount = 2;
        hitParticle = GameObject.Find("HitParticles").GetComponent<ParticleSystem>();
        
    }

    void Update()
    {
        // if ball is allowed to move and speed is positive -> move the ball
        if (canMove && speed > 0) MoveBall();
    }
}
