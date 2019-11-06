using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static Bullet instance_bullet;
    


    void Start()
    {
        //tr = gameObject.transform;
    }

    void Update()
    {

    }

    void Awake()
    {
        instance_bullet = this;
    }

    // //목표지점에 도달했을때
    // void OnTriggerEnter(Collider coll)
    // {
    //     if (coll.transform.tag == "TARGET")
    //     {
            
    //         Destroy(gameObject, 0.5f);
    //     }
    // }
}
