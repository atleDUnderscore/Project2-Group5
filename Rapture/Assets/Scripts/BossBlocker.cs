using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBlocker : MonoBehaviour
{
    [SerializeField] GameObject bossBlockCol;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        bossBlockCol.SetActive(true);
    }
}
