using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Pi2 : MonoBehaviour
{
    Ray RF;
    Ray RB;
    Ray RL;
    Ray RR;
    RaycastHit hit;
    Rigidbody body;
    MeshRenderer mr;
    anim anim;
    Collider col;
    GameObject Player;
    public static Queue<GameObject> Item = new Queue<GameObject>() { };

    public Text countdown;

    float front=0.05f;
    float back=-0.05f;
    float right=0.05f;
    float left=-0.05f;
    float fu=0;
    float bu=0;
    float ru=0;
    float lu=0;

    float time = 10;
    int Is = 0;

    public static bool Shadowreturn = false;
    public static bool Itemreturn = false;


    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        mr = GetComponent<MeshRenderer>();
        anim=GetComponent<anim>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (Pl.shadow)
        {
            //影を操作する時の設定
            Shadowreturn = false;
            col.isTrigger = false;
            body.useGravity = true;
            countdown.enabled = true;
            gameObject.layer = 13;
            gameObject.tag = "PlayerShadow";

            //影の操作をする
            if (Input.GetKey("up"))
            {
                transform.Translate(0, fu, front);
            }
            if (Input.GetKey("down"))
            {
                transform.Translate(0, bu, back);
            }
            if (Input.GetKey("right"))
            {
                transform.Translate(right, ru, 0);
            }
            if (Input.GetKey("left"))
            {
                transform.Translate(left, lu, 0);
            }

            //影を出せる時間の処理
            time -= Time.deltaTime;
            countdown.text = time.ToString("f0");

            //時間切れ
            if (time <= 0)
            {
                Pl.shadow = false;
                Shadowreturn = true;
                //影中に取ったアイテムを未取得状態に戻す
                while (Item.Count > 0)
                {
                    anim = Item.Dequeue().GetComponent<anim>();
                    anim.Catch = true;
                }
                
            }
        }
        else
        {
            //影を操作しないときの状態管理
            transform.position = Player.transform.position;
            col.isTrigger = true;
            body.useGravity = false;
            countdown.enabled = false;
            time = 10;
            gameObject.layer = 15;
            gameObject.tag = "Player";
            Itemreturn = false;
            Shadowreturn = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //自分から4方向にレイを飛ばして壁に当たっているか判定する
        RF = new Ray(transform.position, transform.forward);
        RB = new Ray(transform.position, -transform.forward);
        RR = new Ray(transform.position, transform.right);
        RL = new Ray(transform.position, -transform.right);
        
        //レイが当たっている方向の移動を上向きにする

        if (Physics.Raycast(RF, out hit, 0.5f))
        {
            front = 0;
            fu = 0.05f;
        }

        if (Physics.Raycast(RB, out hit, 0.5f))
        {
            back = 0;
            bu = 0.05f;
        }

        if (Physics.Raycast(RR, out hit, 0.5f))
        {
            right = 0;
            ru = 0.05f;
        }

        if (Physics.Raycast(RL, out hit, 0.5f))
        {
            left = 0;
            lu = 0.05f;
        }

        //壁に上っているとき重力をなくす
        if (fu != 0 || bu != 0 || ru != 0 || lu != 0)
        {
            body.useGravity = false;
            body.velocity = Vector3.zero;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //壁から離れたときに移動の初期化
        front = 0.05f;
        fu = 0;
        back = -0.05f;
        bu = 0;
        right = 0.05f;
        ru = 0;
        left = -0.05f;
        lu = 0;
        body.useGravity = true;
    }

    private void OnTriggerStay(Collider other)
    {
        //影状態でのアイテム獲得処理
        if (other.gameObject.tag == "item" && Input.GetKeyDown(KeyCode.Space))
        {
            other.gameObject.layer = 15;
            Item.Enqueue(other.gameObject);
            Itemreturn = true;
        }
    }
}
