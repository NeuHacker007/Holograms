using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{

    //public void Button_onClick()
    //{
    //    SceneManager.LoadScene("Guppin");
    //}
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
