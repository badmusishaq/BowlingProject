using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("BowlingTrack"))
        {
            Debug.Log("The ball just it a collider");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pit"))
        {
            Debug.Log("Got Triggered!");
            Destroy(gameObject);
        }
    }
}
