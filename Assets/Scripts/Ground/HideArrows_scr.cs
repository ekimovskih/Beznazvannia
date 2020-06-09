using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideArrows_scr : MonoBehaviour
{
    // Start is called before the first frame update
    Transform Player;
    SpriteRenderer sprite;
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 r = (transform.position - Player.position);
        Vector2 s = new Vector2(r.x,r.y);
        float asd = s.magnitude;
        if (asd < 4)
        {
            Color a = sprite.color;
            a.a = (asd-1)/3;
            sprite.color = a;
        }
    }
}
