using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behavior_Script : MonoBehaviour {

    public float speed;
    public float springAbility;

    public bool grounded;

    public Rigidbody2D player1RB;
    public Rigidbody2D player2RB;

    private Animator anim;

    private float maxSpeed = 0.5f;
    //public bool onConveyor = false;

	void Start () {
        player1RB = gameObject.GetComponent<Rigidbody2D>();
        player2RB = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update() {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed",Mathf.Abs(Input.GetAxis("Horizontal")));

        if (Input.GetKeyDown("a")) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKeyDown("d")) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKeyDown("w") && grounded == true) {
            player1RB.AddForce(Vector2.up * springAbility);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        player1RB.AddForce(Vector2.right * speed * moveHorizontal);

        if (player1RB.velocity.x > maxSpeed)
        {
            player1RB.velocity = new Vector2(maxSpeed, player1RB.velocity.y);
        }
        if (player1RB.velocity.x < -maxSpeed)
        {
            player1RB.velocity = new Vector2(-maxSpeed, player1RB.velocity.y);
        }
    }
}
