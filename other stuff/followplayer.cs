using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followplayer : MonoBehaviour
{
    public Transform player;
    private Transform trans;
    private void Start()
    {
        trans = gameObject.GetComponent<Transform>();
    }
    private void Update()
    {
        trans.position = new Vector3(0, player.position.y, -10);
       
    }
}
