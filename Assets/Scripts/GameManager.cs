using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentScore;

    private BowlingBall ball;
    Pin[] currentPins = new Pin[0];

    [SerializeField] GameObject[] objectsToDisable;

    [SerializeField] PlayerController playerController;

    private void Start()
    {
        DisableAllObjects();
    }
    void DisableAllObjects()
    {
        /*foreach(var obj in objectsToDisable)
        {
            obj.SetActive(false);
        }*/
        for(int i = 0; i < objectsToDisable.Length; i++)
        {
            objectsToDisable[i].SetActive(false);
        }
    }
    public void PinKnockedDown()
    {
        currentScore++;
    }

    public void BallKnockedDown()
    {
        ball = null;
    }

    public void BallThrown(BowlingBall bowlingBall)
    {
        ball = bowlingBall;
    }

    private bool CheckIfPiecesAreStatic()
    {
        foreach(var pin in currentPins)
        {
            if(pin != null && pin.DidPinMove())
            {
                return false;
            }
        }

        var ballStatus = ball == null || !ball.DidBallMove();
        return ballStatus;
    }
    void Update()
    {
        if(!playerController.wasBallThrown)
        {
            return;
        }

        if (CheckIfPiecesAreStatic())
        {
            FinishThrow();
        }
    }

    private void SetupFrame()
    {
        currentScore = 0;
    }

    private void FinishThrow()
    {
        //Help us know when the object hits the pit and another object can be spawned.
        Debug.Log("Finish throw called!");
    }

    private void SetupThrow()
    {
        //To handle object throwing after it has hit the pit
    }
}
