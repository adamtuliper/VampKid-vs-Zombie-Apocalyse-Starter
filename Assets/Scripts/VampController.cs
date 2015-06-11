using UnityEngine;
using System.Collections;

public class VampController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Animator _animator;

    private int health;
    private bool _grounded;
    private bool _dead;
    private GameController _gameController;

    [SerializeField]
    private GameObject _batBurst;

    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _gameController = GameController.GetGameControllerInScene();

    }

    // Update is called once per frame
    void Update()
    {
        //reading key press or mouse input ? DO IT HERE
        //Reading Input.GetAxis? Here or FixedUpdate
        if (Input.GetButtonDown("Jump"))
        {
            //Edit/Project Settings/Input shows jump default is spacebar
            //_jump = Input.GetButtonDown("Jump");
            //We can prevent multiple jumps in a row via several ways:
            //1. we can limit our y position
            //2. we can limit how high we can apply a force
            //3. we can apply an edge collider to top of world
            //4. we can only jump when grounded
            //if (transform.position.y < 40)
            Debug.Log("Jumping..? Grounded:" + _grounded);
            if (_grounded)
            {
                _rigidBody.AddForce(new Vector2(0, 1200));
                _animator.SetTrigger("Jump");
                _grounded = false;
                _animator.SetBool("Grounded", false);
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            //Edit/Project Settings/Input shows jump default is spacebar
            //_fire = Input.GetButtonDown("Fire1");
            _animator.SetTrigger("Attack");

            //Note that we could instantiate the projectile here, thats 
            //common when we shoot things out in Unity
            //var projectile = Instantiate(_weapon, projectilePosition, Quaternion.identity);
            //However, I'm using a feature of Unity 5 called a StateMachineBehavior and instead
            //calling code (in /Scripts/AttackStateMachineBehavior) from the vamp_run state 
            //on the vampire's animation controller.
        }
    }


    void FixedUpdate()
    {
        if (_dead)
        {
            return;
        }

        //Read the left/right input
        var horizontal = Input.GetAxis("Horizontal");

        //If we're moving to the right, we've flipped this character by setting localScale=-1
        var localScale = transform.localScale;

        //Flips the character left if the input is < 0 and, right if >0 
        if (horizontal < 0)
        {
            // localScale is a Vector 3, which means it contains x,y,z
            localScale.x = 1;
        }
        else if (horizontal > 0f)
        {
            localScale.x = -1;
        }


        transform.localScale = localScale;

        //If we're moving left or right, play the run animation
        if (horizontal != 0)
        {
            _animator.SetBool("Run", true);
        }
        else
        {
            _animator.SetBool("Run", false);
        }

        //Move the actual object by setting its velocity
        _rigidBody.velocity = new Vector2(horizontal * 20, _rigidBody.velocity.y);
    }

    //Called when something hits this object that also has a collider on it
    //that is not set as a trigger. I use this to detect if we've come in contact
    //with a platform. 
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Ensure all of your platforms have a tag of "platform"
        //otherwise we don't detect the landing.
        if (collision.gameObject.tag == "platform")
        {
            _grounded = true;
            _animator.SetBool("Grounded", true);
        }
    }

    //OnTriggerEnter code here


}
