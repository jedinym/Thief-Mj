using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public GameObject Player;

    private void LateUpdate()
    {
        transform.LookAt(Player.transform);
    }
}
