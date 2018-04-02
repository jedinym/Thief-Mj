using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirtAttractor : MonoBehaviour
{
    public float PullForce;
    public float MinHomeDistance;
    
    private void FixedUpdate()
    {
        GameObject[] squirts = GameObject.FindGameObjectsWithTag("Squirt");

        foreach (GameObject squirt in squirts)
        {
            CanBeAttracted canBeAttracted = squirt.GetComponent<CanBeAttracted>();
            Vector2 dirToPl = (transform.position - squirt.transform.position).normalized;
            Rigidbody2D rb = squirt.GetComponent<Rigidbody2D>();

            if (Vector2.Distance(transform.position, squirt.transform.position) <= MinHomeDistance)
                canBeAttracted.CanBeAtt = false;

            if (canBeAttracted.CanBeAtt) 
                rb.AddForce(dirToPl * PullForce * Time.deltaTime);        
        }

        //GameObject[] waves = GameObject.FindGameObjectsWithTag("Wave");

        //foreach (GameObject wave in waves)
        //{
        //    CanBeAttracted canBeAttracted = wave.GetComponent<CanBeAttracted>();
        //    Vector2 dirToPl = (transform.position - wave.transform.position).normalized;
        //    Rigidbody2D rb = wave.GetComponent<Rigidbody2D>();

        //    if (Vector2.Distance(transform.position, wave.transform.position) <= MinHomeDistance)
        //        canBeAttracted.CanBeAtt = false;

        //    if (canBeAttracted.CanBeAtt)
        //        rb.AddForce(dirToPl * PullForce * Time.deltaTime);
        //}
    }
}
