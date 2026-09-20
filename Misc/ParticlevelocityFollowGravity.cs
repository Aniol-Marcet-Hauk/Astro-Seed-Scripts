using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlevelocityFollowGravity : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameManager.gameManager.player;
    }

    private void Update()
    {
         transform.position =player.position;
    }
}
