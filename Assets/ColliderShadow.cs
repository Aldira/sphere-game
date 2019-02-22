using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderShadow : MonoBehaviour
{

    //影状態のときにプレイヤーの付近にいるかどうかを返す

    bool Shadowtouch = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerShadow") Shadowtouch = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerShadow") Shadowtouch = false;
    }

    public bool ReSt()
    {
        return Shadowtouch;
    }
}
