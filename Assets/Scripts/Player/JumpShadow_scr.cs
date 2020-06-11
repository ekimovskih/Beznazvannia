using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpShadow_scr : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime;
    void Start()
    {
        StartCoroutine(SekfDestroy());
    }
    private void Update()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, pos.y);
    }
    IEnumerator SekfDestroy()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }
}
