using UnityEngine;
using UnityEngine.UI;

public class ScaleThroughButton : MonoBehaviour
{
    private GameObject button;
    Button scaledownbtn;

    void Start()
    {
        button = GameObject.Find("Scale Down");
        scaledownbtn = button.GetComponent<Button>();
    }
    // Use this for initialization
    public void scaleUP()
    {
        transform.localScale += new Vector3(1, 1, 1);
        transform.position += new Vector3(0, 0.5f, 0);
        scaledownbtn.interactable = true;
    }

    public void scaleDown()
    {
       
        Debug.Log(string.Format("{0}_{1}_{2}",transform.localScale.x,transform.localScale.y,transform.localScale.z));
        Debug.Log("--------------------------");
        Debug.Log(string.Format("{0}_{1}_{2}", transform.position.x, transform.position.y, transform.position.z));
        if (transform.localScale.z < 1.0f)
        {


            scaledownbtn.interactable = false;



        }
        else
        {
            transform.localScale -= new Vector3(1, 1, 1);
            transform.position += new Vector3(0, -0.5f, 0);
        }
       
    }
}
