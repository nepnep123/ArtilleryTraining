using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyToBulletPoint : MonoBehaviour
{
    public static ReadyToBulletPoint instance_readyTobullet;

    public Speech walkiTalki;
    bool isFirst = true;
    public string word;
    [HideInInspector]
    public GameObject readyBullet;

    void Awake()
    {
        instance_readyTobullet = this;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.tag == "BULLET_01")
        {
            readyBullet = coll.gameObject;

            UIManager.instance_UIManager.readyToBullet_img.SetActive(false);
            UIManager.instance_UIManager.opCommunication_txt.SetActive(true);
            UIManager.instance_UIManager.bulletReady_txt.SetActive(false);

            UIManager.instance_UIManager.readyToBullet_txt.SetActive(true);
            UIManager.instance_UIManager.readyToBullet_txt.GetComponentInChildren<Text>().text = "항력감소 이중목적 고폭탄<color=red>(K310)</color>";

            UIManager.instance_UIManager.input_info_img.SetActive(true); //방향포경위치 info
            StartCoroutine(SpeechAngle());
            //사격준비 완료 !! 
            HandCtrl.instance_HandCtrl.isFire = true;

        }
        //두번째 탄
        if (coll.transform.tag == "BULLET_02")
        {
            readyBullet = coll.gameObject;

            UIManager.instance_UIManager.readyToBullet_img.SetActive(false);
            UIManager.instance_UIManager.opCommunication_txt.SetActive(true);
            UIManager.instance_UIManager.bulletReady_txt.SetActive(false);

            UIManager.instance_UIManager.readyToBullet_txt.SetActive(true);
            UIManager.instance_UIManager.readyToBullet_txt.GetComponentInChildren<Text>().text = "사거리연장 고폭탄 <color=red>(KM549A1)</color>";

            UIManager.instance_UIManager.input_info_img.SetActive(true);
            StartCoroutine(SpeechAngle());
            HandCtrl.instance_HandCtrl.isFire = true;

        }
        //세번째 탄
        if (coll.transform.tag == "BULLET_03")
        {
            readyBullet = coll.gameObject;

            UIManager.instance_UIManager.readyToBullet_img.SetActive(false);
            UIManager.instance_UIManager.opCommunication_txt.SetActive(true);
            UIManager.instance_UIManager.bulletReady_txt.SetActive(false);

            UIManager.instance_UIManager.readyToBullet_txt.SetActive(true);
            UIManager.instance_UIManager.readyToBullet_txt.GetComponentInChildren<Text>().text = "이중목적 고폭탄 <color=red>(K305)</color>";

            UIManager.instance_UIManager.input_info_img.SetActive(true);
            StartCoroutine(SpeechAngle());
            HandCtrl.instance_HandCtrl.isFire = true;

        }
    }

    IEnumerator SpeechAngle()
    {
        if (isFirst)
        {
            word = "최초 편각" + ArtyWord.StringToArtyWord("2511") + " 최초 사각" + ArtyWord.StringToArtyWord("431") + " 이상";
            yield return new WaitForSeconds(5.0f);
            StartCoroutine(walkiTalki.SpeechWord(word));
            isFirst = false;
        }
    }
}
