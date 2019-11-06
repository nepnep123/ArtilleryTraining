using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OPManager : MonoBehaviour
{
    public static OPManager instance_OPManager;

    public GameObject boardOP;
    public Transform player; //플레이어 위치 

    private BoardManager boardMgr;

    string inputText;
    public int idx;

    public Vector3 _firePos;   // 아군 진지
    public Vector3 _targetPos; // 적 진지
    public Vector3 _hitPos;    // 피탄지

    public Speech walkiTalki;
    private string word;
    void Start()
    {
        boardMgr = boardOP.GetComponent<BoardManager>();
        // idx = 3;
        SetStep(idx);
    }

    void Awake()
    {
        instance_OPManager = this;
    }

    void ChangePlayer(){
        PlayerCtrl.instance_playerCtrl.ChangePlayer();
        StartCoroutine(walkiTalki.SpeechWord(word)); 
    }

    public void SetStep(int step)
    {

        switch (step)
        {
            case 0:
                boardMgr.Erase(string.Empty);
                inputText = "적 진지를\n관측하십시오";
                boardMgr.Write(inputText, false);
                break;
            case 1:
                boardMgr.Erase(string.Empty);
                inputText = "아군 포사격 진지를\n관측하십시오";
                boardMgr.Write(inputText, false);
                break;
            case 2:
                boardMgr.Erase(string.Empty);
                inputText = "무전기로 포수에게\n좌표와 방위각을 하달하십시오";
                boardMgr.Write(inputText, false);

                //포상에 방열할 위치를 뿌려준다. 
                UIManager.instance_UIManager.cannonPos_txt.GetComponent<Text>().text = "방열 위치 :(" + _firePos.x + ", " + _firePos.z + ") 방위각 : " + CalculateMil.CalcuateAzimuth(_firePos, _targetPos) + "밀"
                                    + "\n<color=red>차량은 자동으로 위치로 갑니다.</color> ";
                word = "아군진지좌표" + ArtyWord.LocationToWord(_firePos) + " 방위각" + ArtyWord.AngleToWord(CalculateMil.CalcuateAzimuth(_firePos, _targetPos)) + " 이상";
                break;
            case 3:
                // 3초 후 SP로 이동 
                Invoke("ChangePlayer", 3f);
                break;
            case 4:
                boardMgr.Erase(string.Empty);
                inputText = "피탄지를\n관측하십시오";
                boardMgr.Write(inputText, false);
                break;
            case 5:
                boardMgr.Erase(string.Empty);
                inputText = "무전기로 포수에게\n수정 편사각을 하달하십시오";
                boardMgr.Write(inputText, false);

                //포상에 수정 편사각을 뿌려준다. 
                UIManager.instance_UIManager.fixDeclination = ((_targetPos.z < _hitPos.z) ? "좌로 " : "우로 ") + CalculateMil.FixAzimuth(_firePos, _targetPos, _hitPos).ToString();
                UIManager.instance_UIManager.fixElevationAngle = ((_targetPos.y < _hitPos.y) ? "아래로 " : "위로 ") + CalculateMil.FixElevationAngle(_firePos, _targetPos, _hitPos).ToString();

                word = "수정 편각 " + ((_targetPos.z < _hitPos.z) ? "좌로 " : "우로 ") + ArtyWord.StringToArtyWord(CalculateMil.FixAzimuth(_firePos, _targetPos, _hitPos).ToString())
                    + " 수정 사각 " + ((_targetPos.y < _hitPos.y) ? "아래로 " : "위로 ") + ArtyWord.StringToArtyWord(CalculateMil.FixElevationAngle(_firePos, _targetPos, _hitPos).ToString()) + " 이상";
                break;
            case 6:
                // 3부터 반복
                idx = 3;
                SetStep(idx);
                break;
            default:
                // boardMgr.Write(inputText, true);
                SetStep(++idx);
                break;
        }
    }
}
