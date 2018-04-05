using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostCollisionController : MonoBehaviour
{
    public GameObject ScoreObj;
    ScoreScript ScSc;

    List<DamageVals> DamageValuesList;

    private void Start()
    {
        ScSc = ScoreObj.GetComponent<ScoreScript>();

        DamageValuesList = new List<DamageVals>(Resources.LoadAll<DamageVals>("HitDamageValues")); // Loading all Damage values from folder Resources/HitDamageValues in Assets
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ScSc.Score -= (from v in DamageValuesList where v.Tag == collision.collider.tag select v.Damage).FirstOrDefault();
    }

    private void OnTriggerEnter2D(Collider2D collision) //Trigger only to allow objects (e.g. Waves) to pass through player
    {
        ScSc.Score -= (from v in DamageValuesList where v.Tag == collision.tag select v.Damage).FirstOrDefault();
    }
}
