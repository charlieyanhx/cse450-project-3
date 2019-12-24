using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    // outlet
    Rigidbody2D rigidbody;
    public Transform aimPivot;
    public GameObject projectilePrefab;
    SpriteRenderer sprite;
    Animator animator;
    public Text scoreUI;

    //state tracking
    public int jumpsLeft;
    public int score;
    public bool isPaused;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        score = PlayerPrefs.GetInt("Score");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            return;
        }

        scoreUI.text = score.ToString();

        //move player left
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddForce(Vector2.left * 12f);
            sprite.flipX = true;
        }

        //move player right
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddForce(Vector2.right * 12f);
            sprite.flipX = false;
        }

        //aim toward mouse
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;
        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse = radiansToMouse * 180f / Mathf.PI;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

        //shoot
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newProjectile = Instantiate(projectilePrefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpsLeft > 0)
            {
                jumpsLeft--;
                rigidbody.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            }
        }
        animator.SetInteger("JumpsLeft", jumpsLeft);

        if (Input.GetKey(KeyCode.Escape))
        {
            MenuController.instance.Show();
        }
    }

    void Awake()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        //this update event is synchronized with the physics engine
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);

        if (rigidbody.velocity.magnitude > 0)
        {
            animator.speed = rigidbody.velocity.magnitude / 3f;
        }

        else
        {
            animator.speed = 1f;
        }
    }

    //collision event
    void OnCollisionStay2D(Collision2D other)
    {
        //check that we collided with ground
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //check what is directly below our character's feet
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.up, 1f);
            Debug.DrawRay(transform.position,-transform.up*1f);
            //we might have multiple things below our character's feet
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                //check that we collided with ground tight below our feet
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    //reset jump count
                    jumpsLeft = 2;
                }
            }
        }
    }
}
