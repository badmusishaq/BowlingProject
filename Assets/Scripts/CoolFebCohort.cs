using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolFebCohort : MonoBehaviour
{
    [SerializeField] private float randomDecimal = 5.6f;
    [SerializeField] private string randomText = "Welcome to the cohort!";
    [SerializeField] private int randomInt = 25;
    [SerializeField] private bool randomBool;

    [SerializeField] private float xMoveSpeed;
    [SerializeField] private float zMoveSpeed;

    MeshRenderer ballRenderer;
    [SerializeField] Material tempMaterial;

    [SerializeField] int myNumber = 15;

    [SerializeField] float minPosition;
    [SerializeField] float maxPosition;

    private Rigidbody ballRigidBody;

    [SerializeField] float forceValue;

    // Start is called before the first frame update
    void Start()
    {
        ballRenderer = GetComponent<MeshRenderer>();
        ballRigidBody = GetComponent<Rigidbody>();
        //ballRenderer.enabled = false;
        ballRenderer.material = tempMaterial;
        /*Debug.Log(randomText);
        Debug.Log("Random decimal = " + randomDecimal);
        Debug.Log("Random Integer = " + randomInt);
        Debug.Log("Boolean value = " + randomBool);*/

        Debug.Log(randomText + " Random decimal = " + randomDecimal +
            " Random Integer = " + randomInt + " Boolean value = " + randomBool);

        ConditionalStatement();
    }

    void ConditionalStatement()
    {
        if(myNumber > 20 || myNumber < 10)
        {
            Debug.Log(myNumber + " is either greater than 20 or less than 10");
        }
        /*else if (myNumber != 20)
        {
            Debug.Log(myNumber + " is not equals to 20");
        }*/
        else
        {
            Debug.Log(myNumber + " is not less than 20");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Running");
        //MoveObject();

        //MoveConstraint();

        LaunchBall();
    }

    void LaunchBall()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ballRigidBody.AddForce(-transform.forward * forceValue, ForceMode.Force);
        }
    }

    void MoveObject()
    {
        //transform.Translate(xMoveSpeed * Input.GetAxis("Horizontal"), 0, zMoveSpeed * Input.GetAxis("Vertical"));
        transform.Translate(Vector3.back * zMoveSpeed * Input.GetAxis("Vertical"));
        transform.Translate(Vector3.left * xMoveSpeed * Input.GetAxis("Horizontal"));
        //transform.Rotate(0, 1, 0);
    }

    void MoveConstraint()
    {
        float moveDistance = -1 * Input.GetAxis("Vertical");

        Vector3 currentPosition = transform.position + new Vector3(0, 0, moveDistance);

        if(currentPosition.z > maxPosition)
        {
            currentPosition.z = maxPosition;
        }
        else if(currentPosition.z < minPosition)
        {
            currentPosition.z = minPosition;
        }
        transform.position = currentPosition;
    }


}
