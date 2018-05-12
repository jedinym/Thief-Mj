using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFly : MonoBehaviour
{
    Rigidbody2D Rb2d;

    //float DistanceTraveled = 0;
    public float ScaleMultiplier;
    public float TimeToDestroy;

    //Vector2 LasPos;

    // Use this for initialization
    void Start()
    {
        Rb2d = GetComponent<Rigidbody2D>();

        Rb2d.AddForce(transform.up * 400);

        Invoke("DestroyWave", TimeToDestroy);

        InvokeRepeating("ScaleWave", 0f, 0.02f);
    }

    private void Update()
    {
        //DistanceTraveled += Vector2.Distance(new Vector2(transform.position.x, transform.position.y), LasPos);

        //LasPos = new Vector2(transform.position.x, transform.position.y);
    }

    private void FixedUpdate()
    {
        //transform.localScale = transform.localScale + new Vector3(DistanceTraveled, DistanceTraveled, DistanceTraveled) - new Vector3(ScaleMultiplier, ScaleMultiplier, ScaleMultiplier);

        //transform.localScale = Vector3.Scale(new Vector3(DistanceTraveled, DistanceTraveled, DistanceTraveled), transform.localScale);

        //transform.lossyScale.Scale(new Vector3(5, 5, 5));
    }

    private void ScaleWave()
    {
        transform.localScale += new Vector3(ScaleMultiplier, ScaleMultiplier, ScaleMultiplier);
    }

    private void DestroyWave()
    {
        Destroy(gameObject);
    }
}
