using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcOffset : MonoBehaviour
{
    public GameObject Player;

    [SerializeField]
    float Radius;
    [SerializeField]
    float RecalcTime;

    float Angle;

    Vector3 Position;

    //Transform PlayerTransform;

    private void Start()
    {
        InvokeRepeating("CalculateOffsetPosition", 0f, RecalcTime);
    }

    private void CalculateOffsetPosition()
    {
        Angle = Mathf.Ceil(Random.value * 360);

        Position.x = Player.transform.position.x + Radius * Mathf.Sin(Angle * Mathf.Deg2Rad);
        Position.y = Player.transform.position.y + Radius * Mathf.Cos(Angle * Mathf.Deg2Rad);
        Position.z = 0f;

        transform.position = Position;
    }
}
