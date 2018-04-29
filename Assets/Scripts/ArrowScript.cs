using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public bool CanShoot;

    Rigidbody2D Rb2D;

	// Use this for initialization
	void Start ()
    {
        Rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void FixedUpdate()
    {
        if (CanShoot)
            Rb2D.AddForce(transform.up * 50);
    }
}
