using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag != "Ground")
        //{
        //    DeactiveGravity();
        //}
    }

    public void ActiveGravity()
    {
        rb.simulated = true;
    }
    public void DeactiveGravity()
    {
        rb.simulated = false;
    }

}
