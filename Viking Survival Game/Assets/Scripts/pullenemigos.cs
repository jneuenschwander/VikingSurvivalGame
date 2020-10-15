using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class pullenemigos : MonoBehaviour
{
    public GameObject enemigo;
    public int xPos;
    public int zPos;
    public int numEnemigos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemigoDrop());
    }
    IEnumerator EnemigoDrop()
    {
        while (1 < numEnemigos)
        {
            xPos = Random.Range(240, 260);
            zPos = Random.Range(175, 185);
            Instantiate(enemigo, new Vector3(xPos, 120, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            numEnemigos -= 1;
        }
    }


}
