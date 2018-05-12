using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowContainerScript : MonoBehaviour
{
    public float DelayArrowStart;

    void Start ()
    {
        StartCoroutine(ActivateArrows());
    }
	
    IEnumerator ActivateArrows()
    {
        foreach (Transform childTransform in transform)
        {
            yield return new WaitForSeconds(DelayArrowStart);

            childTransform.gameObject.GetComponent<ArrowScript>().CanShoot = true;
        }
    }
}
