using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ProjectileController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject playerGO;
    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //moving a projectile
        if (isMoving)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, playerGO.transform.position, speed * Time.deltaTime);
            rb.MovePosition(newPosition);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Demon")
        {
            //TODO OnHit
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Player")
        {
            //TODO OnHit
            Destroy(this.gameObject);
        }
    }
}
