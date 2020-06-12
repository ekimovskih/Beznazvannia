using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_button : MonoBehaviour
{
    // Start is called before the first frame update
 
    public void Exit()
    {
        Application.Quit();
    }
    public void LoadNewGame()
    {
        StartCoroutine(LoadGame());
        
    }
    public void Mute()
    {
        AudioListener camera = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        camera.enabled = !camera.isActiveAndEnabled;
    }
    IEnumerator LoadGame()
    {
        GameObject.Find("DarkScreen").GetComponent<DarkScreen>().MakeDarker();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Village_0");
    }
}
