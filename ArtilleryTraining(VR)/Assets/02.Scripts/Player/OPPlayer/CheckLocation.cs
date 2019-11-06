using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckLocation : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private float passedTime = 0.0f;
    private float selectedTime = 1.0f;

    private LayerMask layer;

    void Start()
    {
        layer = 1 << LayerMask.NameToLayer("LOCATION");
    }

    void Update()
    {
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, 10000f, layer))
        {
            GameObject location = hit.collider.gameObject;

            passedTime += Time.deltaTime;
            if (passedTime >= selectedTime && passedTime < selectedTime + Time.deltaTime)
            {
                location.GetComponentInChildren<Canvas>().transform.LookAt(transform);
                location.GetComponentInChildren<Text>().enabled = true;

                float x = Mathf.Round(location.transform.position.x * 10);
                float y = Mathf.Round(location.transform.position.y * 100) / 100;
                float z = Mathf.Round(location.transform.position.z * 10);

                location.GetComponentInChildren<Text>().text = string.Format("좌표 (<color=#FF0000>{0}</color>, <color=#0000FF>{2}</color>)\n고도 <color=#00FF00>{1} m</color>", x, y, z);

                if(OPManager.instance_OPManager._targetPos == Vector3.zero){
                    // OP매니저 내의 적 진지가 null일 시 적 진지좌표 대입
                    OPManager.instance_OPManager._targetPos = new Vector3(x, y, z);
                }else if(OPManager.instance_OPManager._firePos == Vector3.zero){
                    // OP매니저 내의 아군 진지가 null일 시 아군 진지좌표 대입
                    OPManager.instance_OPManager._firePos = new Vector3(x, y, z);
                }else{
                    // 적, 아군 진지가 null이 아닐 경우 피탄지 좌표 대입
                    OPManager.instance_OPManager._hitPos = new Vector3(x, y, z);
                }

                OPManager.instance_OPManager.SetStep(-1);
                StartCoroutine("ResetLocation", location);
            }
        }
        else
        {
            passedTime = 0.0f;
        }
    }

    IEnumerator ResetLocation(GameObject location)
    {
        yield return new WaitForSeconds(1f);
        location.GetComponentInChildren<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(3f);
        location.GetComponentInChildren<Text>().enabled = false;
    }
}
