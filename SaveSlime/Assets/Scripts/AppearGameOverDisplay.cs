using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearGameOverDisplay : MonoBehaviour
{
    public GameObject gameOverDisplay;

    private void Start()
    {
        StartCoroutine(Appear());
    }

    IEnumerator Appear()
    {
        yield return new WaitForSeconds(1.1f);
        gameOverDisplay.SetActive(true);
    }
}
