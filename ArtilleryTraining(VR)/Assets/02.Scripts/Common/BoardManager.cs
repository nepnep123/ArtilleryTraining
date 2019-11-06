using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    private Text thisText;
    private string eraseText;

    private bool isFull;

    private AudioSource boardSource;

    void Awake()
    {
        thisText = GetComponentInChildren<Text>();
        boardSource = GetComponent<AudioSource>();
    }

    public bool Write(string text, bool full)
    {
        if (text.Equals(string.Empty) || text == null) return isFull;

        // 문장을 완성시킬지 여부를 받아옴
        isFull = full;

        if (full)
        {
            if (boardSource.isPlaying) boardSource.Stop();
            // 문장 완성이 참일때 문장 한번에 완성
            thisText.text = eraseText + text;
        }
        else
        {
            if (!boardSource.isPlaying) boardSource.Play();
            // 문장 완성이 거짓일때 문장 단어별로 입력
            StartCoroutine("Writing", text);
        }
        return isFull;
    }

    IEnumerator Writing(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (!isFull)
            {
                // 0.1초마다 문자열 1글자씩 입력
                yield return new WaitForSeconds(0.1f);
                // 줄바꿈할땐 0.3초 추가
                if (text.Substring(i, 1) == "\n") yield return new WaitForSeconds(0.3f);

                if (isFull) yield break;

                thisText.text += text.Substring(i, 1);
                if (i == text.Length - 1)
                {
                    if (boardSource.isPlaying) boardSource.Stop();
                    isFull = true;
                }
            }
        }
    }

    public void Erase(string text)
    {
        thisText.text = text;
        eraseText = text;
    }


}
