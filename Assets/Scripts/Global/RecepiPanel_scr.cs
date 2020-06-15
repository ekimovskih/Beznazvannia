using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecepiPanel_scr : MonoBehaviour
{
    public GameObject ComponentPrefab;
    public GameObject ComponentPapa;
    public float timer = 4f;
    //public Image sprite;
    //public Text Count;
    [HideInInspector] public int id;
    public Text Name;

    IEnumerator SelfDeactivate()
    {
        yield return new WaitForSeconds(timer);
        SlotsDeactivate();
        gameObject.SetActive(false);
    }
    void SlotsDeactivate()
    {
        foreach (RecepiSlot_scr slot in ComponentPapa.GetComponentsInChildren<RecepiSlot_scr>())
        {
            Destroy(slot.gameObject);
        }
    }
    public void CheckID(GameObject item)
    {
        Drop_scr Comp = item.GetComponent<Drop_scr>();
        if (id == Comp.id)
        {
            StopCoroutine("SelfDeactivate");
            StartCoroutine(SelfDeactivate());
        }
        else
        {
            StopCoroutine("SelfDeactivate");
            SlotsDeactivate();
            newAsk(item);
        }
    }
    public void newAsk(GameObject item)
    {
        Drop_scr Comp = item.GetComponent<Drop_scr>();
        id = Comp.id;
        //sprite.sprite = item.GetComponent<SpriteRenderer>().sprite;
        Name.text = Comp.supertype;
        for (int i = 0; i< Comp.Recipe.Length; i++)
        {
            GameObject slot = Instantiate(ComponentPrefab, ComponentPapa.transform);
            slot.GetComponent<RecepiSlot_scr>().Set(Comp.Recipe[i], Comp.RecipeCount[i]);
        }
        StartCoroutine(SelfDeactivate());
    }
}
