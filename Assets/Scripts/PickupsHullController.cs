using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupsHullController : MonoBehaviour {

    //*outside visible objects
    public GameObject PickupsShell;

    //*internal objects
    private GameObject[] PickupsShellClones;

    public float SpawnTimeInitSec;
    public float SpawnTimeIntervalSec;

    public float ScaleMultiplier;

    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("SpawnPickupsShellClone", SpawnTimeInitSec, SpawnTimeIntervalSec);

        //*debug
        //SpawnPickupsShellClone();
        //SpawnPickupsShellClone();
        //SpawnPickupsShellClone();
        //SpawnPickupsShellClone();

    }

    private List<Vector3> SpawnSpotsInitialization()
    {
        //*list of spawn points X, Y
        //List<Vector3> SpawnSpotVectors = new List<Vector3>
        //{
        //    //*initialize list by manual spawns points
        //    new Vector3(1, 0)
        //    //new Vector3(10, -10),
        //    //new Vector3(-10, 10),
        //    //new Vector3(-10, -10),
        //    //new Vector3(20, 20),
        //    //new Vector3(20, -20),
        //    //new Vector3(-20, 20),
        //    //new Vector3(-20, -20)
        //};

        //List<Vector3> SpawnSpotVectors = new List<Vector3>();
        //List<GameObject> SpawnSpotObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnSpot"));

        //foreach (GameObject SpawnSpot in SpawnSpotObjects)
        //{
        //    SpawnSpotVectors.Add(SpawnSpot.transform.localPosition);
        //    //print(SpawnSpot.transform.localPosition);
        //}

        List<Vector3> SpawnSpotVectors = (from go in GameObject.FindGameObjectsWithTag("SpawnSpot") select go.transform.position).ToList();

        return SpawnSpotVectors;
    }

    private Vector3 GetRandomValidSpawnSpot(List<Vector3> _SpawnSpots)
    {
        int RandomSpawnSpotElementIndex = Random.Range(0, _SpawnSpots.Count);
        return _SpawnSpots[RandomSpawnSpotElementIndex];
    }

    //*check existence of spawn on spawn spot
    

    private void SpawnPickupsShellClone()
    {
        //*initialize spawn spots array
        List<Vector3> SpawnSpots = SpawnSpotsInitialization(); //This should maybe be in Start() itself for perf. reasons
        //*choose random element from array
        Vector3 SpawnSpot = GetRandomValidSpawnSpot(SpawnSpots);

        //*debug print
        //Debug.Log("SpawnPoint [X,Y] : [" + SpawnSpot.x + "," + SpawnSpot.y + "]");

        //*check existing clones positions agains generater spawn spot for validity
        PickupsShellClones = GameObject.FindGameObjectsWithTag("PickupsShell");
        bool isValidSpawnSpot = true;

        foreach (GameObject PickupsShellClone in PickupsShellClones)
        {
            if (PickupsShellClone.transform.localPosition == SpawnSpot)
            {
                isValidSpawnSpot = false;
                break;
            }
        }

        if (isValidSpawnSpot)
        {
            GameObject PickupsShellClone;
            //PickupsShellClone = Instantiate(PickupsShell, transform, false) as GameObject;
            PickupsShellClone = Instantiate(PickupsShell, SpawnSpot, new Quaternion(0, 0, 0, 0));
            PickupsShellClone.transform.localScale = new Vector3(0.2f, 0.2f, 0);
            //PickupsShellClone.transform.localPosition = new Vector3(SpawnSpot.x, SpawnSpot.y, 0);
        }
        else
        {
            if (GameObject.FindGameObjectsWithTag("PickupsShell").Count() < GameObject.FindGameObjectsWithTag("SpawnSpot").Count()) //To avoid StackOverflowException because of Calls when there are not any SpawnSpots left
            {
                SpawnPickupsShellClone();
            }
        }
    }

    // Update is called once per frame
 //   void Update () {
		
	//}
}
