using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private Image sprite;
    bool can = true;
    public Vector3 on = new Vector3(200, 0,0);
    public Vector3 off = new Vector3(-5000, -5000,0);
    public GameObject Parent;
    public bool playOnStart = false;
    void Start()
    {
        sprite = GetComponent<Image>();
        if (playOnStart)
        {
           // StartCoroutine(LighterFromStart());
        }
    }

    // Update is called once per frame
    public IEnumerator Darker()
    {
        if (can)
        {
            StopCoroutine("Lighter");
            //StopAllCoroutines();
            Parent.transform.position = on;
            can = false;
            for (int i = 0; i < 100; i++)
            {
                //Debug.Log(sprite.color.a);
                Color eh = sprite.color;
                eh.a = i / 50f;
                sprite.color = eh;
                yield return new WaitForSeconds(0f);
            }
            can = true;
        }
        

    }
    public IEnumerator Lighter()
    {
        //can = true;
        for (int i = 100; i > 0; i--)
        {
            //Debug.Log(sprite.color.a);
            Color eh = sprite.color;
            eh.a = i / 50f;
            sprite.color = eh;
            yield return new WaitForSeconds(0f);
        }
        yield return new WaitForSeconds(1f);
        if (can)
        {
            Parent.transform.position = off;
        }
        
    }
    public IEnumerator LighterFromStart()
    {
        Parent.transform.position = on;
        //can = true;
        for (int i = 100; i > 0; i--)
        {
            //Debug.Log(sprite.color.a);
            Color eh = sprite.color;
            eh.a = i / 50f;
            sprite.color = eh;
            yield return new WaitForSeconds(0f);
        }
        Parent.transform.position = off;
        sprite.color = Color.black;



    }

    public void MakeLighter()
    {
        StartCoroutine(Lighter());
    }
    public void MakeDarker()
    {
        StartCoroutine(Darker());

    }
}
