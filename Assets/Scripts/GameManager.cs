using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentScore;

    private BowlingBall ball;
    Pin[] currentPins = new Pin[0];

    //[SerializeField] GameObject[] objectsToDisable;

    [SerializeField] PlayerController playerController;

    [SerializeField] Transform pinSetDefaultPosition;
    [SerializeField] GameObject pinSetPrefab;
    [SerializeField] float throwTimeout = 10f;

    bool throwStarted;
    int throwNumber;
    float remainingTimeout;

    private void Start()
    {
        //DisableAllObjects();

        Invoke(nameof(SetupFrame), 1);
    }
    void DisableAllObjects()
    {
        /*foreach(var obj in objectsToDisable)
        {
            obj.SetActive(false);
        }*/
        /*for(int i = 0; i < objectsToDisable.Length; i++)
        {
            objectsToDisable[i].SetActive(false);
        }*/
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
        throwNumber = 0;

        DisposeLastFrame();

        Instantiate(pinSetPrefab, pinSetDefaultPosition.position, pinSetDefaultPosition.rotation);
        currentPins = FindObjectsOfType<Pin>();

        SetupThrow();
    }

    void DisposeLastFrame()
    {
        foreach(var pin in currentPins)
        {
            if(pin != null)
            {
                Destroy(pin.gameObject);
            }
        }
    }

    private void FinishThrow()
    {
        //Help us know when the object hits the pit and another object can be spawned.
        throwStarted = false;

        foreach(var pin in currentPins)
        {
            if(pin != null && pin.DidPinFall)
            {
                currentScore++;
            }
        }

        if(throwNumber == 0 && currentScore < 10)
        {
            Invoke(nameof(SetupThrow), 1);
            throwNumber++;
            return;
        }

        Invoke(nameof(SetupFrame), 1);
    }

    private void SetupThrow()
    {
        //To handle object throwing after it has hit the pit
        foreach(var pin in currentPins)
        {
            if(pin != null)
            {
                pin.ResetPosition();
            }
        }

        if (ball != null)
        {
            Destroy(ball.gameObject);
        }

        playerController.StartAiming();
        throwStarted = true;
        remainingTimeout = throwTimeout;
    }
}
