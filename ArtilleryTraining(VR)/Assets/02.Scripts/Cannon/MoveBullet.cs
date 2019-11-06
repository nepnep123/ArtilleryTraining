using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBullet : MonoBehaviour
{
    public GameObject location;
    public GameObject smokeEffect; //포탄이 맞은 위치에 발생할 효과
    public GameObject bulletTrail_effec; //포탄 꼬리 효과 

    private AudioSource myAudio;
    public AudioClip shootSoundFar;

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
        GameObject _trailEffect = Instantiate(bulletTrail_effec, transform.position, Quaternion.identity);

        if (transform.position.x < 0)
        {
            Destroy(gameObject);
            HandCtrl.instance_HandCtrl.isFire = true;
            StartCoroutine(MissTargetShow());
        }
        Destroy(_trailEffect, 0.1f);
    }

    void OnCollisionEnter(Collision coll)
    {
        // gameObject.GetComponent<CapsuleCollider>().isTrigger = true;

        Instantiate(location, transform.position, Quaternion.identity);
        GameObject _effect = Instantiate(smokeEffect, transform.position, Quaternion.identity);
        myAudio.PlayOneShot(shootSoundFar);

        HandCtrl.instance_HandCtrl.isFire = true;
        Destroy(_effect, 10.0f);
        UIManager.instance_UIManager.missTarget_txt.SetActive(false);

        //Invoke("ChangePlayer", 2f);
        PlayerCtrl.instance_playerCtrl.ChangePlayer();
        OPManager.instance_OPManager.SetStep(-1);

        Destroy(gameObject);
    }

    IEnumerator MissTargetShow(){
        
        UIManager.instance_UIManager.missTarget_txt.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        UIManager.instance_UIManager.missTarget_txt.SetActive(false);
    }
}
