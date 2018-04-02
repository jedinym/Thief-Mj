using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    public KeyCode pressUp;
    public KeyCode pressDown;
    public KeyCode pressLeft;
    public KeyCode pressRight;

    public float MoveForwardSpeed;
    public float MoveBackwardSpeed;
    public float RotateSpeed;
    bool IsDashing;


    private Rigidbody2D Rb2d;
    private Animator animator;

    int doBurstFwdHash;
    int doBurstBwdHash;

    // Use this for initialization
    void Start()
    {

        Rb2d = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        doBurstFwdHash = Animator.StringToHash("doBurstFwd");
        doBurstBwdHash = Animator.StringToHash("doBurstBwd");
    }

    // Update is called once per frame
    void Update()
    {
        if (Rb2d.velocity.magnitude < 9f)
        {
            IsDashing = false;
        }

        //print(Rb2d.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        if (!GlobalVariables.IsGameOver)
        {
            if (Input.GetKey(pressUp))
            {
                Rb2d.AddForce(transform.up * MoveForwardSpeed);
                animator.SetTrigger(doBurstFwdHash);
            }

            if (Input.GetKey(pressDown))
            {
                Rb2d.AddForce(-transform.up * MoveBackwardSpeed);
                animator.SetTrigger(doBurstBwdHash);
            }

            if (!IsDashing)
            {
                if (Input.GetKey(pressLeft))
                    transform.Rotate(Vector3.forward * RotateSpeed * Time.deltaTime);

                if (Input.GetKey(pressRight))
                    transform.Rotate(Vector3.back * RotateSpeed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift)) // FIX LATER; INPUT IN UPDATE & ACTION IN FIXEDUPDATE
                Dash();

            if (Input.GetKeyDown(KeyCode.Escape))
                Time.timeScale = 0;

            //if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
            //    Time.timeScale = 1;
        }
    }

    private void Dash()
    {
        //GetComponent<LimitSpeed>().MaxSpeed += 20;
        //MoveForwardSpeed += 50;
        //ResetMoveForwardSpeedAfterTime();

        //StartCoroutine(ResetMoveForwardSpeedAfterTime());

        IsDashing = true;
        Rb2d.AddForce(transform.up * (MoveForwardSpeed - 10), ForceMode2D.Impulse);
    }

    //private IEnumerator ResetMoveForwardSpeedAfterTime()
    //{
    //    yield return new WaitForSeconds(0.2f);

    //    MoveForwardSpeed -= 50;
    //}
}
