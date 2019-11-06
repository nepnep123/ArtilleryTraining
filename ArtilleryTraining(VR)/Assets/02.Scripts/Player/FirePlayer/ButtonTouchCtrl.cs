using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTouchCtrl : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // x(사각) : 지금 800 -> 200까지 -50 -> 200되면 -1
    //각도를 mil 단위로 계산
    void OnTriggerStay(Collider coll)
    {

        if (coll.transform.tag == "VERTICAL_UP")
        {
            Vector3 rot = CannonDirection.instance_CannonDirec.cannonTr.localEulerAngles;
            rot.x = rot.x - 0.05625f; // 1m 씩 증가 
            CannonDirection.instance_CannonDirec.cannonTr.localEulerAngles = rot;
        }
        if (coll.transform.tag == "VERTICAL_DOWN")
        {
            Vector3 rot = CannonDirection.instance_CannonDirec.cannonTr.localEulerAngles;
            rot.x = rot.x + 0.05625f; // 1m 씩 증가 
            CannonDirection.instance_CannonDirec.cannonTr.localEulerAngles = rot;
        }
        if (coll.transform.tag == "HORIZONTAL_RIGHT")
        {
            Vector3 rot = CannonDirection.instance_CannonDirec.cannonTr.localEulerAngles;
            rot.y = rot.y + 0.05625f; // 1m 씩 증가 
            CannonDirection.instance_CannonDirec.cannonTr.localEulerAngles = rot;
        }
        if (coll.transform.tag == "HORIZONTAL_LEFT")
        {
            Vector3 rot = CannonDirection.instance_CannonDirec.cannonTr.localEulerAngles;
            rot.y = rot.y - 0.05625f; // 1m 씩 증가 
            CannonDirection.instance_CannonDirec.cannonTr.localEulerAngles = rot;
        }
    }
}
