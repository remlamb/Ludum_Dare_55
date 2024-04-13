using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummonerManager : MonoBehaviour
{
    private Vector3 choosenPos;
    private Vector3 targetPosition;
    [SerializeField] private GameObject[] deamons;
    [SerializeField] private RectTransform labelRectTransform;

    private GameObject currentDeamon;
    // Start is called before the first frame update
    void Start()
    {
        currentDeamon = null;
    }

    // Update is called once per frame
    void Update()
    {
    }


    //void SpawnOnClick()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        choosenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        choosenPos.z = gameObject.transform.position.z;
    //        Debug.Log("ClickPos:" + choosenPos.x);

    //        //TODO flip Deamon en fonction de sa position sur l ecran
    //        if (currentDeamon != null)
    //        {
    //            Instantiate(currentDeamon, choosenPos, Quaternion.identity);
    //        }
    //        else
    //        {
    //            Debug.Log("Take A deamon");
    //        }
    //    }
    //}


    //public void ChooseDeamon(int deamon)
    //{
    //    currentDeamon = deamons[deamon];
    //}

}
