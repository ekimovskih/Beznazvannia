using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_scr : MonoBehaviour
{
    // Start is called before the first frame update
    [Range (0,50)]public float BounceStr = 40f;
    public string type = "none";
    public int id;
    public int InStack;
    public int count = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y+0.5f);
    }
    public void Bounce(GameObject collision)
    {
        GameObject ThisObj = this.gameObject;
        Vector2 self = new Vector2(ThisObj.transform.position.x, ThisObj.transform.position.y);
        Vector2 other = new Vector2(collision.transform.position.x, collision.transform.position.y);

        self = -(other - self);
        float VecLength = Mathf.Sqrt(self.x * self.x + self.y * self.y);
        self /= VecLength;
        //Debug.Log(self);
        ThisObj.transform.GetComponent<Rigidbody2D>().AddForce(self * BounceStr);
    }
    public void IsEmpty()
    {
        Destroy(this.gameObject);
    }
}
