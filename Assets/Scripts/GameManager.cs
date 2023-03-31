using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentFrameScore;

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

    [SerializeField] TMP_Text frameNumber;
    [SerializeField] TMP_Text frameTotalScore;
    [SerializeField] TMP_Text frame1stThrowScore;
    [SerializeField] TMP_Text frame2ndThrowScore;

    int currentThrowScore;
    int currentFrame;
    int totalScore;

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
        currentFrameScore++;
        currentThrowScore++;
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
        if(!throwStarted || !playerController.wasBallThrown)
        {
            return;
        }

        remainingTimeout -= Time.deltaTime;

        if (remainingTimeout <= 0 || CheckIfPiecesAreStatic())
        {
            FinishThrow();
        }
    }

    private void SetupFrame()
    {
        currentFrameScore = 0;
        throwNumber = 0;

        DisposeLastFrame();

        Instantiate(pinSetPrefab, pinSetDefaultPosition.position, pinSetDefaultPosition.rotation);
        currentPins = FindObjectsOfType<Pin>();

        currentFrame++;

        ResetScoreUI();
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
                currentFrameScore++;
                currentThrowScore++;
                Destroy(pin.gameObject);
            }
        }

        totalScore += currentThrowScore;

        UpdateScoreUI();

        if(throwNumber == 0 && currentFrameScore < 10)
        {
            Invoke(nameof(SetupThrow), 1);
            throwNumber++;
            return;
        }

        Invoke(nameof(SetupFrame), 1);
    }

    private void SetupThrow()
    {
        currentThrowScore = 0;

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

    void UpdateScoreUI()
    {
        if(throwNumber == 0)
        {
            if(currentFrameScore == 10)
            {
                frame2ndThrowScore.text = "X";
            }
            else
            {
                frame1stThrowScore.text = currentFrameScore.ToString();
            }
        }
        else
        {
            frame2ndThrowScore.text = currentFrameScore == 10 ? "/" : currentThrowScore.ToString();
        }

        frameTotalScore.text = totalScore.ToString();
    }

    void ResetScoreUI()
    {
        frameNumber.text = currentFrame.ToString();
        frame1stThrowScore.text = "";
        frame2ndThrowScore.text = "";
        frameTotalScore.text = totalScore.ToString();
    }
}
