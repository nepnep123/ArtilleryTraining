using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonDirection : MonoBehaviour
{
    public static CannonDirection instance_CannonDirec;
    //캐논 transform
    public Transform cannonTr;
    public Transform enemy;

    public float verticalInput;
    public float horizontalInput;

    public int verticalMil;
    public int horizontalMil;

    private Animator anim;
    public int hashShoot;

    public AudioClip shootSoundNear;
    AudioSource myAudio;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        hashShoot = Animator.StringToHash("IsShoot");
        myAudio = GetComponent<AudioSource>();
    }

    void Awake()
    {
        instance_CannonDirec = this;
    }

    // Update is called once per frame
    void Update()
    {
        //편사각 구하는 공식
        verticalInput = -(cannonTr.localEulerAngles.x - 360);
        horizontalInput = -cannonTr.localEulerAngles.y + 90 + 135;
        //Debug.Log(horizontalInput);

        verticalMil = CalculateMil.DegreeToMil(verticalInput);
        horizontalMil = CalculateMil.DegreeToMil(horizontalInput);
        //Debug.Log(horizontalMil);
    }

    public void CannonLookAt(){
        // cannonTr.LookAt(new Vector3(enemy.position.x, enemy.position.y, enemy.position.z));
    }

    public void ShootAnim(){
        anim.SetTrigger(hashShoot);
        myAudio.PlayOneShot(shootSoundNear);
    }



}
