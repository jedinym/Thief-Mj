using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitSpeed : MonoBehaviour
{
    public float MaxSpeed;
    Rigidbody2D Rb2D;

    private void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        Rb2D.velocity = Vector3.ClampMagnitude(Rb2D.velocity, MaxSpeed);
	}
}
