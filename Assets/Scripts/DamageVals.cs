using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Damage Values", menuName = "Damage Value")]
public class DamageVals : ScriptableObject
{
    public string Tag; // The same tag which belongs to object which has this damage

    public int Damage;
}
