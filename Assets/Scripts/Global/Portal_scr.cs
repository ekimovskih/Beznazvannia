using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_scr : MonoBehaviour
{
    // Start is called before the first frame update
    public bool next = true;
    public GameObject[] Symbols;
    [Range(0, 5)] public int sequence = 0;
    public string WhereToPort;
    private bool can = false;
    void Start()
    {
        can = next;
        if (!next)
        {
            GetComponent<AudioManager>().PlayAudioMine(0);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"&&can&& WhereToPort != null)
        {
            StopAllCoroutines();
            StartCoroutine(Plus());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (WhereToPort != null)
            {
                can = true;
            } 
            StopAllCoroutines();
            StartCoroutine(Minus());
        }
        
    }
    IEnumerator Plus()
    {
        if (sequence < 0)
        {
            sequence = 0;
        }
        //StopCoroutine("Minus");
        for (int i = sequence; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            Symbols[i].SetActive(true);
            GetComponent<AudioManager>().PlayAudioAttack();
            sequence++;
        }
        GetComponent<AudioManager>().PlayAudioHit();
        GameObject.Find("DarkScreen").GetComponent<DarkScreen>().MakeDarker();
        yield return new WaitForSeconds(1f);
        GameObject.Find("DarkScreen").GetComponent<DarkScreen>().MakeLighter();
        GameObject.Find("Player").GetComponent<Player_movement_scr>().Completedlevels++;
        SceneManager.LoadScene(WhereToPort);//, LoadSceneMode.Additive);
    }
    IEnumerator Minus()
    {
        if (sequence > 4)
        {
            sequence = 4;
        }
        //StopAllCoroutines();
        //StopCoroutine("Plus");
        for (int i = sequence; i > -1; i--)
        {
            yield return new WaitForSeconds(1f);
            Symbols[i].SetActive(false);
            GetComponent<AudioManager>().PlayAudioAttack();
            sequence--;
        }
        //Application.LoadLevel(WhereToPort);

    }
}
