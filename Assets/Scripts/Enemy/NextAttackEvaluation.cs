using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAttackEvaluation : MonoBehaviour
{
    //Random RNG = new Random();

    public GameObject Player;

    public float DistanceToAOE;
    public float MaxSpeedToAOE;


    Rigidbody2D PlayerRb2D;

    Attack AOE;
    Attack Arrow;
    Attack Squirt;
    Attack Wave;

    //EnemyMovementController EMCSc;

    //public Attack NextAttack;

    void Start()
    {
        AOE = new Attack("AOE");
        Arrow = new Attack("Arrow");
        Squirt = new Attack("Squirt");
        Wave = new Attack("Wave");

        PlayerRb2D = Player.GetComponent<Rigidbody2D>();

        //EMCSc = GetComponent<EnemyMovementController>();
    }

    void Update()
    {
        //print(PlayerRb2D.velocity.magnitude);
        //if (Vector3.Distance(Player.transform.position, transform.position) < DistanceToAOE && PlayerRb2D.velocity.magnitude < MaxSpeedToAOE)
        //{
        //    NextAttack = Squirt;
        //}
        //else if (PlayerRb2D.velocity.magnitude < MaxSpeedToAOE)
        //{

        //    //if (Random.Range(0, 1) == 0)
        //    //    NextAttack = Wave;
        //    //else
        //    //    NextAttack = Arrow;
        //}
        //else
        //{
        //    NextAttack = Squirt;
        //}
    }

    private void LateUpdate()
    {
        //EMCSc.NextAttackInQueue = NextAttack;
    }

    public Attack GetNextAttack()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < DistanceToAOE && PlayerRb2D.velocity.magnitude < MaxSpeedToAOE)
        {
            return AOE;
        }
        else if (PlayerRb2D.velocity.magnitude < MaxSpeedToAOE)
        {
            if (Random.Range(0, 2) == 0)
                return Wave;
            else
                return Arrow;
        }
        else
        {
            return Squirt;
        }

        //return null;
    }
}

public class Attack
{
    public string ValueString;

    public Attack(string _valueString)
    {
        ValueString = _valueString;
    }
}