using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance_UIManager;

    [Header("Gasin UI")]
    public GameObject gasinUp_img;

    [Header("Cylinder Info UI")]
    public GameObject gasinUp_txt;
    public GameObject bulletReady_txt;
    public GameObject opCommunication_txt;

    [Header("Other UI")]
    public GameObject cannonPos_txt;

    public GameObject readyToBullet_img;
    public GameObject readyToBullet_txt;
    //탄 설명 img
    public GameObject bullet01_info_img;
    public GameObject bullet02_info_img;
    public GameObject bullet03_info_img;

    public GameObject missTarget_txt; //탄이 빗나갓을때 UI; 

    [Header("Input Data UI")]
    public GameObject input_canvas; //trigger Image 포함 
    public GameObject input_info_img;

    public Text currentData_txt; //현재 편사각 txt
    public Text opData_txt; //op 편사각 txt

    public string fixDeclination;
    public string fixElevationAngle;

    private Animator anim;
    public int hashIsTouchTrigger;

    // Start is called before the first frame update
    void Start()
    {
        cannonPos_txt.SetActive(false);
        gasinUp_txt.SetActive(false);
        bulletReady_txt.SetActive(false);
        opCommunication_txt.SetActive(false);
        gasinUp_img.SetActive(false);
        readyToBullet_img.SetActive(false);
        readyToBullet_txt.SetActive(false);

        bullet01_info_img.SetActive(false);
        bullet02_info_img.SetActive(false);
        bullet03_info_img.SetActive(false);

        input_canvas.SetActive(false);
        input_info_img.SetActive(false);

        missTarget_txt.SetActive(false);

        anim = input_canvas.GetComponentInChildren<Animator>();
        hashIsTouchTrigger = Animator.StringToHash("IsTouchTrigger");
    }

    void Awake()
    {
        instance_UIManager = this;
    }

    void Update()
    {
        //현재 편사각 
        currentData_txt.text = "현재 편각 : " + CannonDirection.instance_CannonDirec.horizontalMil +
                                " / 사각 : " + CannonDirection.instance_CannonDirec.verticalMil;

        //OP 전달 받은 편사각 
        opData_txt.text = "최초 편각 : " + 2511 + " / 사각 : " + 431
         + "<color=red>\n\t\t" + fixDeclination + ((fixDeclination != "") ? "/ " : " ") + fixElevationAngle + "</color>";

    }

    public void TouchTrigger()
    {
        anim.SetTrigger(hashIsTouchTrigger);
    }

}
