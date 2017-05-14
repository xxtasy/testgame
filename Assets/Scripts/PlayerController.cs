﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.SerializableAttribute]
public class Boundary {
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 20;

    private SpriteRenderer spriteRenderer;
    private GameObject feet;
    private GameObject head;
    //private Animator animator;

    // Use this for initialization
    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        feet = GameObject.Find("Feet Collider");
        head = GameObject.Find("Head Collider");
        //animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity() {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded) {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump")) {
            if (velocity.y > 0) {
                velocity.y = velocity.y * 0.5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            feet.transform.rotation = Quaternion.Euler(0f, (Mathf.Round(feet.transform.rotation.y) == 1 ? 0f : 180f), 0);
            head.transform.rotation = Quaternion.Euler(0f, (Mathf.Round(head.transform.rotation.y) == 1 ? 0f : 180f), 0);
        }

        //animator.SetBool("grounded", grounded);
        //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;

        if(transform.position.y < -5.0f) {
            Die();
        }
    }

	//this function kills the player if player collides with anything with the 'KillPlayer' tag
	//for now, testing purposes reloads the level
	/*void OnTriggerEnter2D(Collider2D other) {
		if (tag == "Player" && other.tag == "KillPlayer") {
			Debug.Log ("Dead!"); //print Dead! in console -> for testing purposes
			SceneManager.LoadScene("Level 1"); //load level 1 for now
		}
	}*/

    public void Die() {
        Debug.Log("Player dead!"); //print Dead! in console -> for testing purposes
        SceneManager.LoadScene("Level 1"); //load level 1 for now
    }

    public bool isGrounded() {
        return grounded;
    }

    public Vector3 getSpeed() {
        return velocity;
    }
}