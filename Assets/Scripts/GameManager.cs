using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentScore;
    private BowlingBall ball;
    [SerializeField] PlayerController playerController;
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
        return true;
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
