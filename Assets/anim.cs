using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class anim : MonoBehaviour {

    public Animator an;
    MeshRenderer mr;
    Rigidbody rb;

    public static int itemsuu = 0;
    public bool Catch = false;
    

    // Use this for initialization
    void Start () {
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        //このゲームオブジェクトがアイテム化されてるときこのスクリプトに入らないようにする
        if (gameObject.layer == 10)
        {
            GetComponent<anim>().enabled = false;
        }

        //影に獲得されて時間切れになったとき元に戻す
        if (Pi2.Shadowreturn && Catch)
        {
            mr.shadowCastingMode = ShadowCastingMode.On;
            gameObject.layer = 14;
            Catch = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //一定範囲にプレイヤーか影が入ったときアニメーションをポップアップ
        if (other.gameObject.tag == "Player"|| other.gameObject.tag == "PlayerShadow")
        {
            an.SetBool("ポップアップイン", false);
            an.SetBool("ポップアップ", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //一定範囲からプレイヤーか影が出たときアニメーションをフェードアウト
        if (other.gameObject.tag == "Player"|| other.gameObject.tag == "PlayerShadow")
        {
            an.SetBool("ポップアップイン", true);
            an.SetBool("ポップアップ", false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //このアイテムが“プレイヤーに”獲得された時の処理
        if (Input.GetKeyDown(KeyCode.Space) && other.gameObject.tag=="Player")
        {
            ChangeUI();
        }

        //このアイテムが“影に”獲得された時の処理
        if (Input.GetKeyDown(KeyCode.Space) && other.gameObject.tag == "PlayerShadow")
        {
            mr.shadowCastingMode = ShadowCastingMode.Off;
            Catch = true;
        }
    }

    //アイテム獲得処理
    public void ChangeUI()
    {
        gameObject.layer = LayerMask.NameToLayer("UI 3D Object");
        rb.useGravity = false;
        transform.position = new Vector3(-30 + 7 * itemsuu, 42, 115);
        transform.localScale = new Vector3(3, 3, 3);
        itemsuu += 1;
    }
}
