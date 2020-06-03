using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_scr : MonoBehaviour
{
    public int Damage = 5;
    public float Strength;
    public bool Follow = false;
    //private bool ComandToAttack = false;
    private Transform Parent;
    private GameObject player;
    // Start is called before the first frame update

    private void Start()
    {
        player = GameObject.Find("Player");
        Parent = GetComponentInParent<Transform>();
        Enemy_propertys_scr ggTimer = GetComponentInParent<Enemy_propertys_scr>();
        if (!Follow)
        {
            AttackDirrection();
        }
        StartCoroutine(SelfDestroy(ggTimer.AttackDuration));
    }

    private void Update()
    {
        if (Follow)
        {
            transform.localPosition = Vector3.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player_movement_scr>().TakeDamage(Damage, Strength, transform);
        }
    }
    public void AttackDirrection()
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1000);
        //transform.position = new Vector3(0, 0, 0);
    }
    IEnumerator SelfDestroy(float time)
    {
        yield return new WaitForSeconds(time/2);
        Destroy(this.gameObject);
        //Debug.Log("selfdestroy");
    }//pa4imu ti spavnishsa hz gde?!
}
