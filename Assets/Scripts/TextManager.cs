using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : Singleton<TextManager>
{
    public static string[] message;
    private int index = 0;
    private Text text;
    public GameObject textManager;
    [Range(0.1f, 100.0f)]
    public float timer = 0.6f;
    // Use this for initialization
    void Start()
    {
        textManager = GameObject.Find("TestManager");
        textManager.SetActive(false);
        if (message == null)
        {
            message = new string[5] {"Hi, every budy!",
                                    "This is a hololens program.",
                                    "The superwoman will introduce something to you.",
                                    "A video showcase will also be displayed.",
                                    "Thanks for using this program!"};
        }
        //    GameObject gametext = GameObject.Find("TextDisPlayWindow");
        //    text = gametext.GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //timer -= Time.deltaTime;
        //for (int i = 0; i < message.Length -1; i++)
        //{

        //}
        //if (timer <= 0.0f)
        //{
        //  // display next paragraph 

        // // reset timer to default;
        //    timer = 5.0f;
        //}

        //if (VedioPlayer.Instance.movie.isPlaying)
        //{

        //    if (index > message.Length - 1) return;
        //    //if (Input.GetMouseButtonDown(0))
        //    //{

        //    text.text += message[index] + "\n";
        //    index++;
        //    //yield return new WaitForSeconds(0.5f);
        //}

        //    Debug.Log(text.text);
    }
}



