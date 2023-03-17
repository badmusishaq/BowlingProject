using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] Vector3 cameraOffset;

    [SerializeField] Animator aimingArrow;
    [SerializeField] Animator testAnim;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = ball.transform.position + cameraOffset;

        aimingArrow.SetBool("Aiming", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            aimingArrow.SetBool("Aiming", false);

            testAnim.Play("Rotate");
            testAnim.Play("Scale");
        }
    }
}
