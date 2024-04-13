using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ProjectileController : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject playerGO;


    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //moving a projectile
        Vector3 newPosition = Vector3.Lerp(transform.position, playerGO.transform.position, speed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }
}
