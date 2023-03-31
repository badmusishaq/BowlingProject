using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    Vector3 lastPosition;
    Quaternion lastRotation;

    int framesWithoutMoving;
    public bool DidBallMove()
    {
        var didBallMove = (transform.position - lastPosition).magnitude > 0.001f ||
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
        framesWithoutMoving = didBallMove ? 0 : framesWithoutMoving + 1;

        return framesWithoutMoving <= 10;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("BowlingTrack"))
        {
            Debug.Log("The ball just it a collider");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pit")) //Ball enters the pit
        {
            var gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gameManager.BallKnockedDown();
            Debug.Log("Got Triggered!");
            Destroy(gameObject);
            return;
        }
    }
}
