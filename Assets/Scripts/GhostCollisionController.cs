using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostCollisionController : MonoBehaviour
{
    public GameObject ScoreObj;
    ScoreScript ScSc;

    List<DamageVals> DamageValuesList;

    float OriginalFixedDeltaTime;

    public float LenOfSlowDown;
    public float SlowTimeSpeed;
    
    float TimeAtSlowDown;

    private void Start()
    {
        ScSc = ScoreObj.GetComponent<ScoreScript>();

        DamageValuesList = new List<DamageVals>(Resources.LoadAll<DamageVals>("HitDamageValues")); // Loading all Damage values from folder Resources/HitDamageValues in Assets

        OriginalFixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (Time.timeScale == SlowTimeSpeed && Time.unscaledTime - TimeAtSlowDown >= LenOfSlowDown)
        {
            TimeSpeedUp();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ScSc.Score -= (from v in DamageValuesList where v.Tag == collision.collider.tag select v.Damage).FirstOrDefault();

        if (collision.gameObject.tag != "Background" && collision.gameObject.tag != "Pickups")
            TimeSlowDown();
    }

    private void OnTriggerEnter2D(Collider2D collision) //Trigger only to allow objects (e.g. Waves) to pass through player
    {
        ScSc.Score -= (from v in DamageValuesList where v.Tag == collision.tag select v.Damage).FirstOrDefault();

        if (collision.gameObject.tag != "Background" && collision.gameObject.tag != "Pickups")
            TimeSlowDown();
    }

    private void TimeSlowDown()
    {
        TimeAtSlowDown = Time.unscaledTime;
        Time.timeScale = SlowTimeSpeed;
        Time.fixedDeltaTime *= SlowTimeSpeed; // So the movement isnt choppy
    }

    private void TimeSpeedUp()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = OriginalFixedDeltaTime;
    }
}
