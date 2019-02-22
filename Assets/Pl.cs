using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Pl : MonoBehaviour {

    Rigidbody rg;
    ColliderShadow CS;
    Collider col;

    bool ShadowDecision;

    public static bool shadow = false;

	// Use this for initialization
	void Start () {
        rg = GetComponent<Rigidbody>();
        CS=GetComponentInChildren<ColliderShadow>();
        col = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
        

        if (shadow == false)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //シフトキーが押されているとき方向キーで見渡す
                if (Input.GetKey("right"))
                {
                    transform.Rotate(0, 1f, 0);
                }
                if (Input.GetKey("left"))
                {
                    transform.Rotate(0, -1f, 0);
                }
            }
            else
            {
                //方向キーで移動
                if (Input.GetKey("up"))
                {
                    transform.Translate(0, 0, 0.05f);
                }
                if (Input.GetKey("down"))
                {
                    transform.Translate(0, 0, -0.05f);
                }
                if (Input.GetKey("right"))
                {
                    transform.Translate(0.05f, 0, 0);
                }
                if (Input.GetKey("left"))
                {
                    transform.Translate(-0.05f, 0, 0);
                }

                transform.rotation = new Quaternion(0, 0, 0, 0);

                //操作を影に切り替える
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    shadow = true;
                }
            }

            col.isTrigger = false;
            rg.useGravity = true;
            rg.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            col.isTrigger = true;
            rg.useGravity = false;
            rg.constraints= RigidbodyConstraints.FreezeAll;
            ShadowDecision = CS.ReSt();
            //影がプレイヤーに帰ってきたとき
            if (ShadowDecision == true && Input.GetKeyDown(KeyCode.Z))
            {
                if (Pi2.Itemreturn)
                {
                    //影の状態で獲得したアイテムをプレイヤーに持ち帰ったとき獲得したものをアイテム化する
                    while (Pi2.Item.Count > 0)
                    {
                        var i = Pi2.Item.Dequeue().GetComponent<anim>();
                        i.ChangeUI();
                    }
                }
                shadow = false;
            }

        }

        
	}

}
