using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderManager : MonoBehaviour
{
    private static GameObject txtWindow;
    private static Text text;
    private float timercount = 0.0f;
    private int index;
    private bool isTalking = false;
    private static bool isInitiated = false;
    //private IEnumerator emuEnumerator;
    void Start()
    {

        if (!isInitiated)
        {
            initial();
        }
        Debug.Log("Hi");
    }

    void OnLevelWasLoaded(int level)
    {
        Debug.Log(level);
    }

    void initial()
    {
        txtWindow = GameObject.Find("TextDisPlayWindow");
        DontDestroyOnLoad(txtWindow);
        text = txtWindow.GetComponent<Text>();
        timercount = TextManager.Instance.timer * 1000000.00f;
        index = 0;
        isInitiated = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("object entered the trigger");
        TextManager.Instance.textManager.SetActive(true);
        VedioPlayer.Instance.gameObject.SetActive(true);


        VedioPlayer.Instance.movie.Play();
        VedioPlayer.Instance.audio.Play();
        txtWindow.SetActive(true);
        //first line of  
        text.text = TextManager.message[index];
        Debug.Log(text.text);
        index++;
        isTalking = true;
        //nextLines();
        Invoke("nextLines", 5.0f);
    }

    void nextLines()
    {
        if (isTalking)
        {

            while (index < TextManager.message.Length)
            {

                StartCoroutine(printLines());

            }
        }
    }
    IEnumerator printLines()
    {
        if (isTalking && index < TextManager.message.Length && index > 0)
        {
           // yield return new WaitForSeconds(5.0f);
            text.text = TextManager.message[index++];
            
            Debug.Log("***********"+ text.text);
            Invoke("nextLines", 5.0f);
           
        }
        return null;
    }

}

