using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech : MonoBehaviour
{
    AudioSource aSource;
    int length;
    int idx;

    #region AudioClip
    public AudioClip radio;

    public AudioClip one;
    public AudioClip two;
    public AudioClip three;
    public AudioClip four;
    public AudioClip five;
    public AudioClip six;
    public AudioClip seven;
    public AudioClip eight;
    public AudioClip nine;
    public AudioClip zero;

    public AudioClip dot;
    public AudioClip over;

    public AudioClip declination;
    public AudioClip azimuth;
    public AudioClip elevation;
     
    public AudioClip ourLocation;

    public AudioClip left;
    public AudioClip right;
    public AudioClip up;
    public AudioClip down;

    public AudioClip primary;
    public AudioClip fix;
    #endregion

    void Start()
    {
        aSource = GetComponent<AudioSource>();
        //string word = "편각 둘 넷 공 하나 사각 넷 여섯 팔 이상";
        //StartCoroutine(SpeechWord(word));
    }

    public IEnumerator SpeechWord(string word)
    {
        string[] words = word.Split(' ');
        length = words.Length;

        // 시작 시 지지직
        if(idx == 0)
        {
            aSource.clip = radio;
            aSource.Play();
            yield return new WaitForSeconds(1f);
        }

        // 종료 시 지지직
        if (idx == length)
        {
            idx = 0;
            aSource.clip = radio;
            aSource.Play();
            yield break;
        }

        switch (words[idx])
        {
            // idx에 따라 단어별로 발성
            case "하나":
                aSource.clip = one;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.6f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "둘":
                aSource.clip = two;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "삼":
                aSource.clip = three;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.6f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "넷":
                aSource.clip = four;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "오":
                aSource.clip = five;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "여섯":
                aSource.clip = six;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.6f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "칠":
                aSource.clip = seven;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "팔":
                aSource.clip = eight;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "아홉":
                aSource.clip = nine;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.8f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "공":
                aSource.clip = zero;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "점":
                aSource.clip = dot;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "이상":
                aSource.clip = over;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "편각":
                aSource.clip = declination;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "사각":
                aSource.clip = elevation;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "방위각":
                aSource.clip = azimuth;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "아군진지좌표":
                aSource.clip = ourLocation;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(1.3f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "좌로":
                aSource.clip = left;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "우로":
                aSource.clip = right;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "위로":
                aSource.clip = up;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "아래로":
                aSource.clip = down;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "최초":
                aSource.clip = primary;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            case "수정":
                aSource.clip = fix;
                if (!aSource.isPlaying) aSource.Play();
                yield return new WaitForSeconds(0.5f);
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
            default:
                ++idx;
                StartCoroutine(SpeechWord(word));
                break;
        }
    }
}