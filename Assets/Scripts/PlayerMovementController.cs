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
    public float DashSpeed;
    bool IsDashing;


    private Rigidbody2D Rb2d;
    private Animator animator;

    int doBurstFwdHash;
    int doBurstBwdHash;

    float InputForward;
    float InputRotate;
    float InputDash;
    bool IsDashAxisInUse;
    bool UseDashMove;

    float LastPauseTime;

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
        InputForward = Input.GetAxisRaw("Vertical"); //Forward and backwards
        InputRotate = Input.GetAxisRaw("Horizontal"); //Rotation 
        if (Input.GetButtonDown("Dash")) //Dashing
            UseDashMove = true;

        if (Input.GetButtonDown("Pause")) //Pause game
        {
            if (Time.timeScale == 1)
            {
                LastPauseTime = Time.unscaledTime;
                Time.timeScale = 0;
            }
            else
            {
                if (Time.timeScale == 0 && Time.unscaledTime - LastPauseTime > 0.1) //If time from pause is unchecked game does't pause
                    Time.timeScale = 1;
            }
        }

        if (IsDashing && Rb2d.velocity.magnitude < 9f)
            IsDashing = false;

        if (InputForward > 0)
            animator.SetTrigger(doBurstFwdHash);

        if (InputForward < 0)
            animator.SetTrigger(doBurstBwdHash);


        //print(Rb2d.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        if (!IsDashing)
        {
            Rb2d.AddForce(transform.up * InputForward * MoveForwardSpeed * Time.deltaTime); //if input is 0 dont move at all // Move forward speed and backwards are the same here
            transform.Rotate(Vector3.forward * InputRotate * RotateSpeed * Time.deltaTime, Space.World); //if input is 0 dont move at all
        }

        if (UseDashMove)
            Dash();

        //if (!GlobalVariables.IsGameOver)
        //{
        //    if (Input.GetKey(pressUp))
        //    {
        //        Rb2d.AddForce(transform.up * MoveForwardSpeed);
        //        animator.SetTrigger(doBurstFwdHash);
        //    }

        //    if (Input.GetKey(pressDown))
        //    {
        //        Rb2d.AddForce(-transform.up * MoveBackwardSpeed);
        //        animator.SetTrigger(doBurstBwdHash);
        //    }

        //    if (!IsDashing)
        //    {
        //        if (Input.GetKey(pressLeft))
        //            transform.Rotate(Vector3.forward * RotateSpeed * Time.deltaTime);

        //        if (Input.GetKey(pressRight))
        //            transform.Rotate(Vector3.back * RotateSpeed * Time.deltaTime);
        //    }

        //    if (Input.GetKeyDown(KeyCode.LeftShift)) // FIX LATER; INPUT IN UPDATE & ACTION IN FIXEDUPDATE
        //        Dash();

        //    if (Input.GetKeyDown(KeyCode.Escape))
        //        Time.timeScale = 0;

        //    //if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        //    //    Time.timeScale = 1;
        //}
    }

    private void Dash()
    {
        IsDashing = true;
        UseDashMove = false;
        Rb2d.AddForce(transform.up * DashSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }
}
