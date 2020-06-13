using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropPanel_scr : MonoBehaviour
{
    public float timer = 2f;
    public Image sprite;
    public Text Count;
    private int CountInt=0;
    [HideInInspector] public int id;
    public Text Name;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
    public void Show(GameObject drop)
    {
        Drop_scr DropComponent = drop.GetComponent<Drop_scr>();
        Name.text = DropComponent.supertype;
        IncreseCount(DropComponent.count);
        id = DropComponent.id;
        sprite.sprite = drop.GetComponent<SpriteRenderer>().sprite;
    }
    public void IncreseCount(int plus)
    {
        CountInt += plus;
        if (CountInt > 1)
        {
            Count.text = CountInt.ToString();
        }
    }
    public bool CheckID(Drop_scr drop)
    {
        int newID = drop.id;
        if (newID == id)
        {
            IncreseCount(drop.count);
            StopCoroutine("SelfDestroy");
            StartCoroutine(SelfDestroy());
            return true;
        }
        return false;
    }
}

