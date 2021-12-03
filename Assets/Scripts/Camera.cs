using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Player == null) return;

        Vector3 position = transform.position;
        position.x = Player.transform.position.x;
        position.y = Player.transform.position.y;
        transform.position = position;

    }
}
