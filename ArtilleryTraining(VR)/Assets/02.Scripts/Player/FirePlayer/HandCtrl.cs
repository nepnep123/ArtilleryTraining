using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCtrl : MonoBehaviour
{
    public static HandCtrl instance_HandCtrl;

    public Transform firePos; //포탄 발사 위치
    public GameObject Bullet; //발사할 포탄
    public GameObject fireParticle; //발사했을 때 포구 이펙트

    public Speech walkiTalki;

    [HideInInspector]
    public Transform cannonObject; //그랩된 오브젝트
    [HideInInspector]
    public Transform bulletObject;

    private bool isTouched = false;
    private bool isTrigger = false;
    private bool isGrabbed = false;

    [HideInInspector]
    public bool isFire = false;

    //필요한 컴포넌트 
    private Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

        firePos = GameObject.Find("FirePos").transform;
        Bullet = Resources.Load("Bullet") as GameObject;
        fireParticle = Resources.Load("StartShoot") as GameObject;
    }

    void Awake()
    {
        instance_HandCtrl = this;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            // if (Input.GetMouseButtonDown(0))
            // {
            //     StartCoroutine(Fire());
            // }

            //트리거 버튼 이벤트(가신 내릴때)
            if (isTouched && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                // 가신 내리는 소리
                cannonObject.GetComponentsInChildren<AudioSource>()[1].Play();
                //이 이후에는 부모 객체는 사용 불가 된다.
                cannonObject.SetParent(null);
                StartCoroutine(CannonCtrl.instance_CannonCtrl.GetRigid()); //포와 차량 분리 
                isTrigger = true;
                isTouched = false;

                //차량 주차 시작(2초뒤)
                ObjectMove.instance_ObjectMove.isArrive = false;
                StartCoroutine(ObjectMove.instance_ObjectMove.ParkingCar());
                //ObjectMove.instance_ObjectMove.StartCoroutine("ParkingCar");
            }
            if (isTrigger && OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
            {
                UIManager.instance_UIManager.gasinUp_txt.SetActive(false);
                UIManager.instance_UIManager.gasinUp_img.SetActive(false);
                UIManager.instance_UIManager.bulletReady_txt.SetActive(true);

                isTrigger = false;
                isTouched = false;

                cannonObject.SetParent(null);
                cannonObject = null;
            }

            //그랩 버튼 이벤트 (탄 들때)
            if (isTouched && OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
            {
                bulletObject.SetParent(tr);
                UIManager.instance_UIManager.readyToBullet_img.SetActive(true);

                UIManager.instance_UIManager.bullet01_info_img.SetActive(false);
                UIManager.instance_UIManager.bullet02_info_img.SetActive(false);
                UIManager.instance_UIManager.bullet03_info_img.SetActive(false);

                isGrabbed = true;
            }
            if (isGrabbed && OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
            {
                bulletObject.SetParent(null);

                UIManager.instance_UIManager.bullet01_info_img.SetActive(false);
                UIManager.instance_UIManager.bullet02_info_img.SetActive(false);
                UIManager.instance_UIManager.bullet03_info_img.SetActive(false);

                isGrabbed = false;
                isTouched = false;
                bulletObject = null;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        //가신과 충돌했을때
        if (coll.transform.tag == "GASIN")
        {
            cannonObject = coll.gameObject.transform;
            isTouched = true;
        }
        //첫번째 탄
        if (coll.transform.tag == "BULLET_01")
        {
            bulletObject = coll.gameObject.transform;
            UIManager.instance_UIManager.bullet01_info_img.SetActive(true);
            UIManager.instance_UIManager.bullet02_info_img.SetActive(false);
            UIManager.instance_UIManager.bullet03_info_img.SetActive(false);
            isTouched = true;
        }
        //두번째 탄
        if (coll.transform.tag == "BULLET_02")
        {
            bulletObject = coll.gameObject.transform;
            UIManager.instance_UIManager.bullet01_info_img.SetActive(false);
            UIManager.instance_UIManager.bullet02_info_img.SetActive(true);
            UIManager.instance_UIManager.bullet03_info_img.SetActive(false);
            isTouched = true;
        }
        //세번째 탄
        if (coll.transform.tag == "BULLET_03")
        {
            bulletObject = coll.gameObject.transform;
            UIManager.instance_UIManager.bullet01_info_img.SetActive(false);
            UIManager.instance_UIManager.bullet02_info_img.SetActive(false);
            UIManager.instance_UIManager.bullet03_info_img.SetActive(true);
            isTouched = true;
        }

        //방향포경위치에 닿았을때
        if (coll.transform.tag == "InputModel")
        {
            //입력 UI 생성
            UIManager.instance_UIManager.input_canvas.SetActive(true);
            UIManager.instance_UIManager.input_info_img.SetActive(false);

            // isFire = true; //사격 준비 완료 

            Debug.Log(isFire + " IsFire");
        }


        //사격 개시 
        if (coll.transform.tag == "TRIGGER" && isFire)
        {
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        //진동로직
        OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);

        isFire = false;

        //준비선상에 올려놓은 탄은 사라지고 다시 장전해야한다. 
        Destroy(ReadyToBulletPoint.instance_readyTobullet.readyBullet);
        UIManager.instance_UIManager.readyToBullet_txt.SetActive(false);

        UIManager.instance_UIManager.TouchTrigger();
        yield return new WaitForSeconds(2.0f);
        CannonDirection.instance_CannonDirec.ShootAnim();

        GameObject _effect = Instantiate(fireParticle, firePos.position, firePos.rotation);
        Destroy(_effect, 2.0f);
        //포탄 생성 
        GameObject _bullet = Instantiate(Bullet, firePos.position, firePos.rotation);

    }
}
