using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    private GameObject player;
    private Vector3 moveDirection;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        moveDirection = Vector3.left;
    }

    private void Update()
    {
        if (player.GetComponent<PlayerController>().isPlayerDead == true)
            moveDirection = Vector3.zero;

        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (transform.position.x < -12)
            Destroy(gameObject);
    }
}
