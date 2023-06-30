using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int flightSpeed = 10;
    int wingPower = 200;
    new Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = new Vector2(0, 0);
            rigidbody.AddForce(new Vector2(0,wingPower));
            //ParticleSystem e = GameObject.Find("Particle System") as ParticleSystem;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(rigidbody.velocity.y * 4, -90, 90));
    }
}
