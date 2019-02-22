using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Player;
    public GameObject PlayerShadow;

	// Use this for initialization
	void Start () {
         
	}
	
	// Update is called once per frame
	void Update () {
        if (Pl.shadow)
        {
            //カメラをプレイヤーの影に追尾させる
            Vector3 playershadow = PlayerShadow.transform.position;
            transform.position = playershadow + new Vector3(0, 6, -10);

        }
        else
        {
            //カメラをプレイヤーに追尾させる
            Vector3 player = Player.transform.position;
            float rotationy = Player.transform.rotation.y;
            transform.position = player + Player.transform.forward * -10 + Player.transform.up * 6;
            transform.LookAt(player);

        }
	}
}
