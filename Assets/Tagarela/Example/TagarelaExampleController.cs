using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Tagarela))]
public class TagarelaExampleController : MonoBehaviour
{

    public void Start(){
    }

    public void OnGUI() {
        if (GUILayout.Button(" animation 1 ")) {
            GetComponent<Tagarela>().Play("i_hate_bacon");
			//You can Play using both ways:
			//GetComponent<Tagarela>().Play("i_hate_bacon");
            //GetComponent<Tagarela>().Play(0);
			//And you can use the Stop function, to stop the animation
			//GetComponent<Tagarela>().Stop();  
        }

        if (GUILayout.Button(" animation 2 "))
        {
            GetComponent<Tagarela>().Play(1);
        }

    }


}