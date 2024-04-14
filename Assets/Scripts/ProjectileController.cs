using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ProjectileController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject playerGO;
    public bool isMoving = false;

    [SerializeField] private PlayerController playerController;

    private AudioSource source;



    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ////moving a projectile
        //if (isMoving)
        //{
        //    Vector3 newPosition = Vector3.Lerp(transform.position, playerGO.transform.position, speed * Time.deltaTime);
        //    rb.MovePosition(newPosition);
        //}

        if (isMoving)
        {
            Vector3 direction = (playerGO.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;


            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (direction.x < 0)
            {
                angle -= 180f;
            }
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            if (source.isPlaying == false)
            {
                source.Play();
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
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
            playerController.OnLifeDecrease();
        }
    }
}
