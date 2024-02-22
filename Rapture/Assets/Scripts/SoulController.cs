using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulController : MonoBehaviour
{
    [SerializeField] TMP_Text soulCounter;
    void Start()
    {

        soulCounter.text = "0";
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("I hit something");
        if(col.tag == "Player")
        {
            soulCounter.text = "Soul Hit";
            Destroy(this.gameObject);
        }
    }




}
