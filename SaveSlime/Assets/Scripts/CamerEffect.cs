using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerEffect : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Confuse());
    }

    IEnumerator Confuse()
    {
        while(true)
        {
        yield return new WaitForSeconds(Random.Range(3f, 8f));
        transform.position = new Vector3(0, 0, 50);
        transform.rotation = Quaternion.Euler(0, 180, 180);
        yield return new WaitForSeconds(Random.Range(3f, 8f));
        transform.position = new Vector3(0, 0, -50);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
