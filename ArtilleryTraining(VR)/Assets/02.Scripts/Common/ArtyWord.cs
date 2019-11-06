using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtyWord : MonoBehaviour
{

    public static string LocationToWord(Vector3 location)
    {
        string locX = string.Empty;
        string locZ = string.Empty;
        string locWord = string.Empty;

        float x = Mathf.Round(location.x * 10) / 10;
        float z = Mathf.Round(location.z * 10) / 10;

        locX = x.ToString("00000");
        locZ = z.ToString("00000");

        locWord = StringToArtyWord(locX) +  StringToArtyWord(locZ);

        return locWord;
    }

    public static string AngleToWord(int angle)
    {
        string angleWord = StringToArtyWord(angle.ToString());

        return angleWord;
        
    }

    public static string StringToArtyWord(string word)
    {
        string artyWord = string.Empty;
        int j = -1;

        for (int i = 0; i < word.Length; i++)
        {
            if (int.TryParse(word.Substring(i, 1), out j))
            {
                j = int.Parse(word.Substring(i, 1));
            }
            else
            {
                j = -1;
            }

            artyWord += IntToWord(j);
        }

        return artyWord;
    }

    public static string IntToWord(int num)
    {
        string word;
        switch (num)
        {
            case 0:
                word = " 공";
                break;
            case 1:
                word = " 하나";
                break;
            case 2:
                word = " 둘";
                break;
            case 3:
                word = " 삼";
                break;
            case 4:
                word = " 넷";
                break;
            case 5:
                word = " 오";
                break;
            case 6:
                word = " 여섯";
                break;
            case 7:
                word = " 칠";
                break;
            case 8:
                word = " 팔";
                break;
            case 9:
                word = " 아홉";
                break;
            default:
                word = " 점";
                break;
        }

        return word;
    }
}
