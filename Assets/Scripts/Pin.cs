using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;

    Rigidbody rb;

    Vector3 lastPosition;
    Quaternion lastRotation;

    int framesWithoutMoving;

    public bool DidPinFall { get; private set; }


    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
    }

    public void ResetPosition()
    {
        rb.position = startPosition;
        rb.rotation = startRotation;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        lastPosition = startPosition;
        lastRotation = startRotation;

    }
    public bool DidPinMove()
    {
        var didPinMove = (transform.position - lastPosition).magnitude > 0.001f ||
            Quaternion.Angle(transform.rotation, lastRotation) > 0.01f;

        lastPosition = transform.position;
        lastRotation = transform.rotation;

        /*if(didBallMove)
        {
            framesWithoutMoving = 0;
        }
        else
        {
            framesWithoutMoving += 1;
        }*/

        //Ternary operator
        framesWithoutMoving = didPinMove ? 0 : framesWithoutMoving + 1;

        return framesWithoutMoving <= 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pit")) //Pin enters the pit
        {
            var gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gameManager.PinKnockedDown();
            Destroy(gameObject);
            return;
        }
        else if(other.CompareTag("BowlingTrack")) //Pin head hits the floor
        {
            DidPinFall = true;
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
