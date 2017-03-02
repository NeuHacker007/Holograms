using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderManager : MonoBehaviour
{
    private GameObject txtWindow;
    private Text text;
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
         
    }

    void OnLevelWasLoaded(int level)
    {
        Debug.Log(level);
    }

    void initial()
    {
        txtWindow = GameObject.Find("TextDisPlayWindow");
        //DontDestroyOnLoad(txtWindow);

        //DontDestroyOnLoad(this.gameObject);
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
        // here we re-acquire the game object because when the scence changes, the game object assigned
        // to textWindow has been destroyed as well as the text object. In order to solve this problem
        // we re-aquire the game object and its correspond text component. 
        if (txtWindow == null)
        {
            txtWindow = GameObject.Find("TextDisPlayWindow");
        }
        if (text == null)
        {
            text = txtWindow.GetComponent<Text>();
        }


        txtWindow.SetActive(true);
        Debug.Log("-----------------------------------HHHHH------------------");
        //first line of  
        text.text = TextManager.message[index];
        Debug.Log(text.text);
        index++;
        isTalking = true;
        // this will give us a timer function which the nextLines function will be called
        // after 5.0f seconds.
        Time.timeScale = 1;
        Invoke("nextLines", 5.0f);
    }

    void nextLines()
    {
        if (isTalking)
        {
            //GameObject guopin = GameObject.Find("guopin");
            //guopin.AddComponent<ColliderManager>();
            // This will iterate all the Messages that is specified from Unity Inspector
            // or use the default settings in the TextManager class.This part, the index
            // is start from 1 which is the sencond lines in the message Array because
            // the first line has already displayed above. 
            while (index < TextManager.message.Length)
            {
                // This is used to set intervals between two lines when they are show up
                // The reason why we use StartCoroutine function is that we have already
                // used the Invoke function previously. I tested to use Invoke twice but failed
                // to do so here. Therefore, we choose to use StartCoroutine function. 
              
               StartCoroutine(printLines());
             // Invoke("printLines", 5.0f);
            }
        }
    }
   IEnumerator printLines()
    {


        if (isTalking && index < TextManager.message.Length && index > 0)
        {
           // yield return new WaitForSeconds(5);
            text.text = TextManager.message[index++];

            Debug.Log("***********" + text.text);

            Invoke("nextLines", 5.0f);
        }
        return TextManager.message.GetEnumerator();
        
    }

}



