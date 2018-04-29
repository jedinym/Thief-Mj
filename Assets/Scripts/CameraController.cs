using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float SmoothSpeed;
    Vector3 SmoothPos;
    Vector3 DesPos;

    //Vector3 offset;
    public GameObject player;

    //*CAMERA STOPS BEFORE IT HITS THE BOUNDS OF SPRITE
    public GameObject background;

    float rightBound;
    float leftBound;
    float topBound;
    float bottomBound;

    Transform playerTransform;
    //*CAMERA STOPS BEFORE IT HITS THE BOUNDS OF SPRITE

    // Use this for initialization
    void Start () {

        //offset = transform.position - player.transform.position;

        //*CAMERA STOPS BEFORE IT HITS THE BOUNDS OF SPRITE
        //float vertExtent = Camera.main.camera.orthographicSize;
        float vertExtent = GetComponent<Camera>().orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        //spriteBounds = GameObject.Find("Background").GetComponentInChildren<SpriteRenderer>();
        SpriteRenderer backgroundSpriteRenderer = background.GetComponentInChildren<SpriteRenderer>();

        //target = GameObject.FindWithTag("Player").transform;
        playerTransform = player.transform;

        leftBound = (float)(horzExtent - backgroundSpriteRenderer.sprite.bounds.size.x / 2.0f);
        rightBound = (float)(backgroundSpriteRenderer.sprite.bounds.size.x / 2.0f - horzExtent);
        bottomBound = (float)(vertExtent - backgroundSpriteRenderer.sprite.bounds.size.y / 2.0f);
        topBound = (float)(backgroundSpriteRenderer.sprite.bounds.size.y / 2.0f - vertExtent);
        //*CAMERA STOPS BEFORE IT HITS THE BOUNDS OF SPRITE

    }

    // Update is called once per frame
    //void Update () {



    //}

    private void FixedUpdate()
    {
        //*CAMERA STOPS BEFORE IT HITS THE BOUNDS OF SPRITE
        DesPos = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        //DesPos.x = Mathf.Clamp(DesPos.x, leftBound, rightBound);
        //DesPos.y = Mathf.Clamp(DesPos.y, bottomBound, topBound);


        transform.position = DesPos;
        //transform.position = pos;
        //*CAMERA STOPS BEFORE IT HITS THE BOUNDS OF SPRITE
    }
}
