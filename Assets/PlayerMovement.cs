using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    int flightSpeed = 5;
    int wingPower = 200;
    new Rigidbody2D rigidbody;
    private new ParticleSystem particleSystem;
    private bool grounded = false;
    private void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        rigidbody.gravityScale = 1f;
    }

    private void Update()
    {
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            rigidbody.velocity = new Vector2(0, 0);
            rigidbody.AddForce(new Vector2(0,wingPower));
            particleSystem.Play();
            grounded = false;
        }

        //FastFall
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rigidbody.gravityScale = 2.5f;
        }
        else
        {
            rigidbody.gravityScale = 1;
        }

        //Check if Groundet -> Velocity = 0 or Die
        if (transform.position.y <= -4.2f)
        {
            Debug.Log(rigidbody.velocity.y);
            if (rigidbody.velocity.y > -8.75f)
            {
                rigidbody.velocity = new Vector2(0, 0);
                grounded = true;
            }
            else
                Die();
        }
        
        //Clamp Y position to Screen
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -4.2f, 4));

        //Check if Grounded
        if (grounded)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            //Change Rotation according to velocity
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(rigidbody.velocity.y * 4, -90, 90));
        }
        
        //Set Speed Of Player
        transform.Translate(new Vector2(flightSpeed, 0) * Time.deltaTime, Space.World);
        
        //Camera Follow Player
        Camera.main.transform.position = new Vector3( transform.position.x+4, 0, -10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
            Die();
    }
    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
