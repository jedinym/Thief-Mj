//TODO//
//FIX SHOOTING STUFF BASED ON CONDITION

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovementController : MonoBehaviour {

    public GameObject Player;
    Rigidbody2D PlayerRb2D;

    Rigidbody2D Rb2D;

    //public GameObject ShieldHull;
    public GameObject Squirt;
    public GameObject Wave;
    public GameObject AOE;
    public GameObject ArrowContainer;

    //public float followSharpness = 0.1f;
    //Vector3 FollowOffsetOnCircle;
    [SerializeField]
    float InitShootTime; //= 0.0f;
    [SerializeField]
    float NewShootOffsetTime;
    [SerializeField]
    float NewShootOffsetPeriod;
    [SerializeField]
    float MaxSpeedToAOE;
    [SerializeField]
    float DistanceToAOE;

    [SerializeField]
    float NewFollowOffsetTime; //= 0.0f;
    [SerializeField]
    float NewFollowOffsetPeriod; // = 3f; //*seconds to re-calculate new position for thief

    Vector3 PlayerPositionWithOffset;
    public bool Idle;

    [SerializeField]
    float SetIdleAfterAOESecs;

    //
    //private Rigidbody2D rb2d;
    private Animator animator;
    //
    int doBurstFwdHash;
    //int doBurstBwdHash;

    //
    Vector3 currentPosition;
    Vector3 previousPosition;

    //*
    //public Rigidbody2D squirt;

    //*
    private float currentAngle;
    private float targetAngle;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    float GetAngleBetweenVectors(Vector3 target, Vector3 source)
    {
        Vector3 TargetToSourceNormal = (target - source).normalized;
        return Mathf.Atan2(TargetToSourceNormal.y, TargetToSourceNormal.x) * Mathf.Rad2Deg;
    }

    Vector3 TargetPositionWithOffset(Vector3 target)
    {
        //*vector on circlew
        //FollowOffsetOnCircle = Random.insideUnitCircle.normalized;
        //print(FollowOffsetOnCircle);
        //return target + FollowOffsetOnCircle;

        float radius = 1.5f;
        float angle = Mathf.Ceil(Random.value * 360);
        Vector3 position;
        position.x = target.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        position.y = target.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        //pos.z = target.z;
        position.z = 0;

        Collider2D[] hitCols = Physics2D.OverlapCircleAll(position, 0.1f);
        if (hitCols.Length > 0)
        {
            print("Recalculate");
            position = TargetPositionWithOffset(target);
        }

        //RaycastHit2D rch;

        //rch = Physics2D.Raycast(transform.position, position, Vector2.Distance(transform.position, position));

        //if (rch.collider != null)
        //    position = TargetPositionWithOffset(target);

        //if (!Physics.Raycast(transform.position, position, Vector2.Distance(transform.position, position)))
        //{
        //    return position;
        //}


        //if (Rigidbody.Sweep)

        //if (Physics.Raycast(transform.position, position, Vector3.Distance(transform.position, position)))
        //{
        //    print("Oki");
        //}

        //RaycastHit2D[] rh;
        //rh = Physics2D.RaycastAll(transform.position, position, Vector2.Distance(transform.position, position));
        //if (rh.Length > 1)
        //{
        //    print("recalc");
        //    position = TargetPositionWithOffset(target);
        //}

        return position;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Use this for initialization
    void Start () {


        Rb2D = GetComponent<Rigidbody2D>();
        //*
        //rb2d = GetComponent<Rigidbody2D>();

        //*
        PlayerPositionWithOffset = TargetPositionWithOffset(Player.transform.position);

        //float currentAngle = GetAngleBetweenVectors(GhostPositionWithOffset, transform.position);

        //*
        animator = GetComponent<Animator>();
        doBurstFwdHash = Animator.StringToHash("doBurstFwd");
        //doBurstBwdHash = Animator.StringToHash("doBurstBwd");

        //*initial positions conditions to track thief movement
        currentPosition = transform.position;
        previousPosition = transform.position;

        PlayerRb2D = Player.GetComponent<Rigidbody2D>();
        //*squirt shooting
        //InvokeRepeating("ShootSquirt", 3, 2f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!GlobalVariables.IsGameOver)
        {
            //*shooting
            if (Time.time > NewShootOffsetTime)
            {
                if (Time.time > InitShootTime)
                {
                    //ShootSquirt();
                    //ShootWave();
                    //ShootAOE();

                    //ShootArrows();

                    if (Vector3.Distance(Player.transform.position, transform.position) < DistanceToAOE && PlayerRb2D.velocity.magnitude < MaxSpeedToAOE)
                    {
                        ShootAOE();
						StartCoroutine(SetIdleC()); // After a set amount of seconds set Idle to false so the Enemy can move Again
                    }
                    else if (PlayerRb2D.velocity.magnitude < MaxSpeedToAOE)
                    {
                        ShootWave();
                    }
                    else
                    {
                        ShootSquirt();
                    }
                }
                NewShootOffsetTime += NewShootOffsetPeriod;
            }

            if (Idle)
                Rb2D.constraints = RigidbodyConstraints2D.FreezePosition; // Enemy is unmovable while using AOE attack
            else if (!Idle)
                Rb2D.constraints = RigidbodyConstraints2D.None;


            //*modify thief's offset every x seconds
            //if (!Idle)
            //{
            //    Rb2D.constraints = RigidbodyConstraints2D.None;

            //    if (Time.time > NewFollowOffsetTime)
            //    {
            //        PlayerPositionWithOffset = TargetPositionWithOffset(Player.transform.position);

            //        targetAngle = GetAngleBetweenVectors(PlayerPositionWithOffset, transform.position);
            //        //print("Target" + targetAngle);

            //        //*increment time
            //        NewFollowOffsetTime += NewFollowOffsetPeriod;
            //    }

            //    currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, 90f * Time.deltaTime);
            //    //currentAngle = Mathf.MoveTowards(currentAngle, targetAngle, 90f * Time.deltaTime);
            //    //print("Current" + currentAngle);

            //    if (currentAngle != targetAngle)
            //    {
            //        transform.position += (PlayerPositionWithOffset - transform.position) * Time.deltaTime;
            //        transform.position += Quaternion.Euler(0, 0, currentAngle) * Vector3.left * 0.3f * Time.deltaTime;
            //    }
            //    else
            //        transform.position += (PlayerPositionWithOffset - transform.position) * Time.deltaTime;
            //}
            //else
            //{
            //    Rb2D.constraints = RigidbodyConstraints2D.FreezePosition; // Enemy is unmovable while using AOE attack
            //}

            //transform.rotation = Quaternion.Euler(0f, 0f, GetAngleBetweenVectors(Player.transform.position, transform.position) - 90); // Maintain orientation even while Idling
        }
    }

    void LateUpdate()
    {
        //*track thief's movement to show engines burst animation
        currentPosition = transform.position;
        if (currentPosition != previousPosition) animator.SetTrigger(doBurstFwdHash); //*play engines burst forward animation
        previousPosition = currentPosition;


        //transform.LookAt(Player.transform);
    }

    private void FixedUpdate()
    {
        //*for physiscscs!
    }

    void ShootSquirt()
    {
        //Rigidbody2D SquirtClone;
        GameObject SquirtClone;
        //Rigidbody2D SquirtCloneRb2d;

        //float SquirtCloneForce = 120f;
        float ThiefToGhostLerpDistancePct = 0.2f;

        //get intrapolated distance from thief to player
        Vector3 ThiefToGhostLerp = Vector3.Lerp(transform.position, Player.transform.position, ThiefToGhostLerpDistancePct / ((transform.position - Player.transform.position).magnitude));

        //print("PointToPlayer-> X, Y : " + ThiefToGhostLerp.x + "," + ThiefToGhostLerp.y);
        //print("Squirt Shot!");

        //*calculate squirt rotation to point to ghost
        Quaternion RotationInDegrees_z = Quaternion.Euler(0f, 0f, GetAngleBetweenVectors(Player.transform.position, transform.position) - 90);

        //*instantiate squirt in position and rotated
        SquirtClone = Instantiate(Squirt, ThiefToGhostLerp, RotationInDegrees_z) as GameObject;
        //SquirtCloneRb2d = Instantiate(squirt, ThiefToGhostLerp, SquirtRotationInDegrees_z) as Rigidbody2D;
    }

    private void ShootWave()
    {
        GameObject WaveClone;

        float ThiefToGhostLerpDistancePct = 0.4f;

        Vector3 ThiefToGhostLerp = Vector3.Lerp(transform.position, Player.transform.position, ThiefToGhostLerpDistancePct / ((transform.position - Player.transform.position).magnitude));

        Quaternion RotationInDegrees_z = Quaternion.Euler(0f, 0f, GetAngleBetweenVectors(Player.transform.position, transform.position) - 90);

        WaveClone = Instantiate(Wave, ThiefToGhostLerp, RotationInDegrees_z) as GameObject;
        //Vector3 SquirtCloneToGhostNormal = (GhostGameObject.transform.position - SquirtClone.transform.position).normalized;
    }

    private void ShootAOE()
    {
        Idle = true;
        GameObject AOEClone;

        AOEClone = Instantiate(AOE, transform.position, new Quaternion()) as GameObject;
    }

    private void ShootArrows()
    {
        Idle = true;
        GameObject ArrowContainerClone;

        float ThiefToGhostLerpDistancePct = 0.4f;

        Vector3 ThiefToGhostLerp = Vector3.Lerp(transform.position, Player.transform.position, ThiefToGhostLerpDistancePct / ((transform.position - Player.transform.position).magnitude));

        Quaternion RotationInDegrees_z = Quaternion.Euler(0f, 0f, GetAngleBetweenVectors(Player.transform.position, transform.position) - 90);

        ArrowContainerClone = Instantiate(ArrowContainer, ThiefToGhostLerp, RotationInDegrees_z) as GameObject;

        //Vector3 Offset = new Vector3(1f, 1f, 1f);

        //ArrowContainerClone = Instantiate(ArrowContainer, (transform.position + Offset), new Quaternion());
    }

    IEnumerator SetIdleC()
    {
        if (Idle == true)
        {
            yield return new WaitForSeconds(SetIdleAfterAOESecs);

            Idle = false;
        }
    }
}
