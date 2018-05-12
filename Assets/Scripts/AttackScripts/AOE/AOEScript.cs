using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class AOEScript : MonoBehaviour
{
    public GameObject Enemy;
    //EnemyMovementController EMCSc;


    public float ScaleAdder;
    Vector3 ScaleAddVector;

    public float TelegraphSecs;
    public float MaxSize;
    public float AlphaSubstractor;

    Stopwatch Sw = new Stopwatch();

    SpriteRenderer SRend;
    Color Color;

    void Start()
    {
        //EMCSc = Enemy.GetComponent<EnemyMovementController>();
        SRend = GetComponent<SpriteRenderer>();
        Color = SRend.color;

        ScaleAddVector = new Vector3(ScaleAdder, ScaleAdder, ScaleAdder);
    }

    void FixedUpdate()
    {
        if (!Sw.IsRunning)
            Sw.Start();

        if (Sw.ElapsedMilliseconds >= TelegraphSecs * 1000)
        {
            if (transform.localScale.x < MaxSize && transform.localScale.y < MaxSize && transform.localScale.z < MaxSize)
            {
                transform.localScale += ScaleAddVector;

            }
            else if (transform.localScale.x >= MaxSize && transform.localScale.y >= MaxSize && transform.localScale.z >= MaxSize)
            { 
                StartCoroutine(FadeAndDestroy());
            }
        }
    }

    IEnumerator FadeAndDestroy() // WACKY; try to look into this later // LATER: partially fixed
    {
        //Enemy.GetComponent<EnemyMovementController>().Idle = false;

        while (Color.a > 0f)
        {
            Color.a -= AlphaSubstractor;

            SRend.color = Color;

            yield return null;
        }

        Destroy(gameObject);
    }
}
