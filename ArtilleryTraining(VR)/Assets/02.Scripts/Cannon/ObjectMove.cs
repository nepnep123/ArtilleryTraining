using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public static ObjectMove instance_ObjectMove;
    public Transform[] wheels; //자동차 바퀴 
    public Transform targetPos; //방열 위치

    public float wheelSpeed = 100.0f;
    public float carSpeed = 10.0f; //자동차 스피드 
    public bool isStopped = true; //차량이 멈춰있는지 여부 
    public bool isArrive = false; //차량 도착 여부 

    public bool isCannonArrive = false; //다시 sp로 왔을 때 

    public Hashtable ht1; //가신을 들고 출발

    public Hashtable ht2; //가신을 놓고 차 혼자 출발
    //public Transform lookObject;

    void Awake()
    {
        instance_ObjectMove = this;

    
    }

    void Start()
    {
        ht1 = new Hashtable();
        ht1.Add("path", iTweenPath.GetPath("WithGasin"));
        ht1.Add("time", 10.0f);
        ht1.Add("easetype", iTween.EaseType.linear);
        ht1.Add("orienttopath", true);
        ht1.Add("oncompletetarget", this.gameObject);
        ht1.Add("oncomplete", "FinishPath");

        ht2 = new Hashtable();
        ht2.Add("path", iTweenPath.GetPath("WithoutGasin"));
        ht2.Add("time", 20.0f);
        ht2.Add("easetype", iTween.EaseType.linear);
        ht2.Add("orienttopath", true);
        ht2.Add("oncompletetarget", this.gameObject);
        ht2.Add("oncomplete", isStopped = true);
    }

    public void StartFirePlayer()
    {
        //오피측에서 관측후 수치를 보내주면 차 이동
        if (!PlayerCtrl.instance_playerCtrl.isOp && !isCannonArrive)
        {
            isStopped = false;
            CannonCtrl.instance_CannonCtrl.isCannonStopped = false;

            iTween.MoveTo(this.gameObject, ht1);
            UIManager.instance_UIManager.cannonPos_txt.SetActive(true);
            isCannonArrive = true;
            GetComponent<AudioSource>().Play();
        }
    }

    void Update()
    {
        
        StartFirePlayer();

        if (!isStopped)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].Rotate(-Vector3.forward * Time.deltaTime * wheelSpeed);
            }
        }

        if (isArrive)
        {
            CannonDirection.instance_CannonDirec.CannonLookAt();

            transform.position = Vector3.MoveTowards(transform.position, targetPos.position,
                         carSpeed * Time.deltaTime);
        }
    }

    void FinishPath()
    {
        //Debug.Log("Finished Path !!!!");
        isStopped = false;
        // //메인캐릭터 움직임 시작.
        // PlayerCtrl.instance_playerCtrl.isMove = true;
        StartCoroutine(CannonSetPosition());
    }

    public IEnumerator ParkingCar()
    {
        yield return new WaitForSeconds(2.0f);
        isStopped = false;
        iTween.MoveTo(this.gameObject, ht2);
    }

    //챠랑이 포상에 도착했을 때 
    IEnumerator CannonSetPosition()
    {
        yield return new WaitForSeconds(2.0f);
        CannonCtrl.instance_CannonCtrl.isCannonStopped = true;
        isArrive = true;
        UIManager.instance_UIManager.cannonPos_txt.SetActive(false);
        UIManager.instance_UIManager.gasinUp_txt.SetActive(true);
        yield return new WaitForSeconds(7.0f);
        UIManager.instance_UIManager.gasinUp_img.SetActive(true);
        UIManager.instance_UIManager.bulletReady_txt.SetActive(false);

    }




}
