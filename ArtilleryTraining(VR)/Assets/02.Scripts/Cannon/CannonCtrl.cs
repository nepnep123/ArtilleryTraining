using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCtrl : MonoBehaviour
{
    public static CannonCtrl instance_CannonCtrl;
    public Transform wheel;

    public float wheelSpeed = 100.0f;
    private float maxHeight = 400.0f;

    public bool isCannonStopped = true; //포가 멈춰있는지 여부

    public GameObject gasin;

    void Start()
    {
        gasin = GameObject.FindGameObjectWithTag("GASIN");
    }

    void Update()
    {
        //바퀴 굴러가는 로직
        if (!isCannonStopped)
        {
            wheel.Rotate(Vector3.forward * Time.deltaTime * wheelSpeed);
        }
    }


    void Awake()
    {
        instance_CannonCtrl = this;
    }

    public IEnumerator GetRigid()
    {
        Rigidbody rigid =  gasin.AddComponent<Rigidbody>();
        yield return new WaitForSeconds(5.0f);
        //못움직이게 막는다. 
        rigid.constraints = RigidbodyConstraints.FreezeAll;
    }

}
