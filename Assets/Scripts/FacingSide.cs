using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingSide : MonoBehaviour
{
    [SerializeField] private bool isRightSided = false;
    void Start()
    {
        if (isRightSided)
        {
            if (this.gameObject.transform.position.x > 0)
            {
                this.gameObject.transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                this.gameObject.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            if (this.gameObject.transform.position.x > 0)
            {
                this.gameObject.transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                this.gameObject.transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
    void Update()
    {

    }
}
