using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Vector2 movementVector;
    private CapsuleCollider2D capsuleCollider2D;
    private Rigidbody2D rb;
    public float movementSpeed = 6f;
    public float jumpHeight = 15f;
    public BoxCollider2D doorBoxCollider2D;
    public SpriteRenderer doorSpriteRenderer;
    public Sprite[] statusDoorSpriteArray;
    public float timerToOpenDoor = 0.3f;
    public float timerToCloseDoor = 1.5f;
    private float timer = 0.0f;
    private bool timeStart;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        Vector2 playerVelocity = new Vector2(movementVector.x * movementSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        if (capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("SoulSand")))
        {
            movementSpeed = 4f;
            jumpHeight = 12f;           
        }
        if (capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ice")))
        {
            movementSpeed = 13f;
            jumpHeight = 20f;
        }
        if (capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            movementSpeed = 6f;
            jumpHeight = 15f;
        }
        if (capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Door")))
        {
            timeStart = true;                             
        }
        if (timeStart)
        {
            timer += Time.deltaTime;
        }
        if (timer > timerToOpenDoor && capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Door")))
        {
            doorBoxCollider2D.enabled = false;
            doorSpriteRenderer.sprite = statusDoorSpriteArray[1];
        }
        if (timer > timerToCloseDoor && !capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Door")))
        {
            doorBoxCollider2D.enabled = true;
            doorSpriteRenderer.sprite = statusDoorSpriteArray[0];
            timer = timer - timerToCloseDoor;
            timeStart = false;
        }
    }
    private void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
        Debug.Log(movementVector);
    }
    private void OnJump(InputValue value)
    {
        if (!capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")) && !capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ice")) && !capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("SoulSand")))
        {
            return; 
        }
        if (value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpHeight);
        }
    }
}