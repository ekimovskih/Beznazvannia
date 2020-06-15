using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_button : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D CursorDefault;
    public Texture2D CursorClick;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&& CursorDefault!=null)
        {
            Cursor.SetCursor(CursorClick, Vector2.zero, CursorMode.Auto);
        }
        if (Input.GetMouseButtonUp(0)&&CursorDefault != null)
        {
            Cursor.SetCursor(CursorDefault, Vector2.zero, CursorMode.Auto);
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void LoadNewGame()
    {
        StartCoroutine(LoadGame());
        StartCoroutine(VolumeDown());


    }
    public void Mute()
    {
        //AudioSource[] Asources = 
        AudioListener camera = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        AudioListener.volume = 0f;
        //camera.enabled = !camera.isActiveAndEnabled;
    }
    public void UnMute()
    {
        //AudioSource[] Asources = 
        AudioListener camera = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        AudioListener.volume = 1f;
        //camera.enabled = !camera.isActiveAndEnabled;
    }
    IEnumerator LoadGame()
    {
        GameObject.Find("DarkScreen").GetComponent<DarkScreen>().MakeDarker();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Village_0");
    }
    IEnumerator VolumeDown()
    {
        AudioSource camera = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        for (int i = 0; i < 100; i++)
        {
            camera.volume -= 0.004f;
            yield return new WaitForSeconds(0f);
        }
    }
}
