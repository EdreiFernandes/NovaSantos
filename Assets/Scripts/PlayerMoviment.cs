using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Alice 
 */

public class PlayerMoviment : MonoBehaviour
{
    private GunControl scriptGun;

    private Rigidbody2D playerRigidBody;
    [SerializeField] private Transform groundCheck;

    private bool onGround;   
    private bool canClimb;   
    private bool jumping = false;
    private bool facingRight = true;

    private Vector2 respawnPoint;

    private float currentTime;
    private float initialGravity;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private void Start()
    {       
        playerRigidBody = GetComponent<Rigidbody2D>();
        scriptGun = GetComponentInChildren<GunControl>();
        initialGravity = playerRigidBody.gravityScale;
        respawnPoint = transform.position;
    }

    private void Update()
    {
        CheckJump();
    }

    void FixedUpdate()
    {
        HorizontalMoviment();
        if (canClimb) VerticalMoviment();
        Jump();
        Shoot();
    }

    private void VerticalMoviment()
    {
        float vertical = Input.GetAxis(Constants.VerticalInput);

        playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, vertical * speed);
    }

    private void HorizontalMoviment()
    {
        float horizontal = Input.GetAxisRaw(Constants.HorizontalInput);

        playerRigidBody.velocity = new Vector2(horizontal * speed, playerRigidBody.velocity.y);

        //test direction
        if (horizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private void Jump()
    {
        if (jumping)
        {
            playerRigidBody.velocity = Vector2.zero;
            playerRigidBody.AddForce(Vector2.up * jumpForce);
            jumping = false;
        }
    }

    private void CheckJump()
    {
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer(Constants.GroundLayer));        

        if (Input.GetButtonDown(Constants.JumpInput) && onGround)
        {
            jumping = true;
        }
    }

    private void Shoot()
    {
        currentTime++;
        if (Input.GetButton(Constants.FireInput))
        {
            currentTime = 0;
            if (Time.time > scriptGun.nextFire)
            {
                scriptGun.nextFire = Time.time + scriptGun.fireRate;
                scriptGun.Shoot();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals(Constants.LadderTag))
        {
            canClimb = true;
            playerRigidBody.gravityScale = 0;
        }

        if(collision.tag.Equals(Constants.RespawnTag))
        {
            transform.position = respawnPoint;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals(Constants.LadderTag))
        {
            canClimb = false;
            playerRigidBody.gravityScale = initialGravity;
        }
    }
}
