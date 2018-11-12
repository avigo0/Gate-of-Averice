using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    public GameObject player;
    public float posX;
    public float posY;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    void LateUpdate()
    {
        posX = player.transform.position.x;
        posY = player.transform.position.y + 3;
        Debug.Log(posX);
        gameObject.transform.position = new Vector3(posX, posY, gameObject.transform.position.z);
    }


}
