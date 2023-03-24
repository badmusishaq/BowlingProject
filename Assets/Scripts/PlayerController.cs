using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] Vector3 cameraOffset;

    [SerializeField] Animator aimingArrow;
    [SerializeField] Animator testAnim;

    [SerializeField] List<Rigidbody> ballBody;
    [SerializeField] Transform throwDirection;
    [SerializeField] float throwForce;

    public bool wasBallThrown;

    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = ball.transform.position + cameraOffset;

        aimingArrow.SetBool("Aiming", true);
    }

    // Update is called once per frame
    void Update()
    {
        TryThrowBall();
    }

    void TryThrowBall()
    {
        if(wasBallThrown || !Input.GetButtonDown("Fire1"))
        {
            return;
        }

        wasBallThrown = true;

        var selectedPrefab = ballBody[Random.Range(0, ballBody.Count)];

        var newBallBody = Instantiate(selectedPrefab, throwDirection.position, throwDirection.rotation);
        newBallBody.AddForce(-throwDirection.forward * throwForce, ForceMode.Impulse);

        gameManager.BallThrown(newBallBody.GetComponent<BowlingBall>());
    }
    void SwitchAnimation()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            aimingArrow.SetBool("Aiming", false);

            testAnim.Play("Rotate");
            testAnim.Play("Scale");
        }
    }    
}
