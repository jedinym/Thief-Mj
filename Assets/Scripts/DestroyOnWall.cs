using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Background")
            Destroy(gameObject);
    }
}
