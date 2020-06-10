using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private Image sprite;
    bool can = true;
    Vector3 on = new Vector3(200, 0,0);
    Vector3 off = new Vector3(200, -1000,0);
    public GameObject Parent;
    void Start()
    {
        sprite = GetComponent<Image>();
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

    public void MakeLighter()
    {
        StartCoroutine(Lighter());
    }
    public void MakeDarker()
    {
        StartCoroutine(Darker());

    }
}
