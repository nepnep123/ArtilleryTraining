using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance_playerCtrl;
    private float speed = 10.0f;

    public bool isMove = false; //플레이어가 움직일수 잇는지 여부 
    public bool isOp = false; //현재 플레이어가 OP인지 SP인지 여부 

    public GameObject leftWalkietalkie; //왼손에 들고있는 무전기
    public GameObject rightGPVFX_Binoculars; //오른손에 들고있는 쌍안경 (OP에 있을 때만 SetActive true);

    public Transform op_SpawnPoint; //op위치
    public Transform sp_SpawnPoint; //sp위치 

    //캐릭터 움직임(오른쪽 조이스틱)
    public OVRInput.Axis2D rightStick = OVRInput.Axis2D.SecondaryThumbstick;

    //필요한 컴포넌트 
    private Transform tr;
    private CharacterController cc;
    private Animator anim;


    void Start()
    {
        tr = GetComponent<Transform>();
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        ChangePlayer();
    }

    void Awake()
    {
        instance_playerCtrl = this;

    }

    void Update()
    {
        //포가 도착하기 전까지는 움직이지 못한다.
        if (isMove)
        {
            MovePlayer();
        }
    }

    //캐릭터가 현재 OP인지 SP인지 구분하는 함수
    public void ChangePlayer()
    {
        if (isOp)
        {
            tr.position = sp_SpawnPoint.position;
            tr.rotation = sp_SpawnPoint.rotation;

            leftWalkietalkie.SetActive(true); 
            foreach(MeshRenderer mesh in rightGPVFX_Binoculars.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.enabled = false;
            }
            foreach(SkinnedMeshRenderer mesh in rightGPVFX_Binoculars.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                mesh.enabled = false;
            }
            rightGPVFX_Binoculars.GetComponentInChildren<BoxCollider>().enabled = false;
            rightGPVFX_Binoculars.GetComponentInChildren<Camera>().enabled = false;
            isMove = true;
            isOp = false;
        }
        else
        {
            tr.position = op_SpawnPoint.position;
            tr.rotation = op_SpawnPoint.rotation;

            leftWalkietalkie.SetActive(true);            
            foreach(MeshRenderer mesh in rightGPVFX_Binoculars.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.enabled = true;
            }
            foreach(SkinnedMeshRenderer mesh in rightGPVFX_Binoculars.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                mesh.enabled = true;
            }
            rightGPVFX_Binoculars.GetComponentInChildren<BoxCollider>().enabled = true;

            isMove = false;
            isOp = true;
        }
    }



    public void MovePlayer()
    {
        Vector2 rightStick_pos = OVRInput.Get(rightStick);

        // rightStick_pos.y = Input.GetAxis("Vertical");
        // rightStick_pos.x = Input.GetAxis("Horizontal");

        if (rightStick_pos.y >= 0.2f)
        {
            //Debug.LogFormat("============ x : {0} y : {1}=========", rightStick_pos.x, rightStick_pos.y);
            cc.SimpleMove(Camera.main.transform.forward * speed);
        }

        if (rightStick_pos.y <= -0.2)
        {
            //Debug.LogFormat("============ x : {0} y : {1}=========", rightStick_pos.x, rightStick_pos.y);
            cc.SimpleMove(-Camera.main.transform.forward * speed);
        }

        if (rightStick_pos.x >= 0.2)
        {
            //Debug.LogFormat("============ x : {0} y : {1}=========", rightStick_pos.x, rightStick_pos.y);
            cc.SimpleMove(Camera.main.transform.right * speed);
        }

        if (rightStick_pos.x <= -0.2)
        {
            //Debug.LogFormat("============ x : {0} y : {1}=========", rightStick_pos.x, rightStick_pos.y);
            cc.SimpleMove(-Camera.main.transform.right * speed);
        }
    }

    //시야를 가리기 때문에 5초 뒤에 쌍안경 생성 
    IEnumerator SetActiveBino(){
        yield return new WaitForSeconds(5.0f);
        rightGPVFX_Binoculars.SetActive(true);
    }
}
