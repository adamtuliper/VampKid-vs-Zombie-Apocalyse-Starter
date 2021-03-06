﻿using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;


public class ZombieController : MonoBehaviour
{

    private int _health = 3;
    private bool _isDead;
    private bool _isReadyToMove;
    private Animator _animator;
    private GameController _gameController;
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    //TODO: recycle/object pool these
    private GameObject _hitPrefab;

    private Rigidbody2D _rigidBody;

    //When was the player last spotted? We'll use this so we walk for 
    //a minimum of two seconds when we spot the player.
    private DateTime _playerSpottedTime;
    private Vector2 _lastVelocity;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Idle", true);

        //We use this to flash it red and to fade it away.
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _gameController = GameController.GetGameControllerInScene();
        _rigidBody = GetComponent<Rigidbody2D>();

        //StartCoroutine here
    }

	//IEnumerator ActivateMe here

    public void GotHit()
    {
        if (_isDead) return;

        //Kick off a separate task to flash the zombie
        StartCoroutine(FlashZombieRed());
        //play 'hit' animation.
        _animator.SetTrigger("GotHit");
        _health -= 1;
        Instantiate(_hitPrefab, transform.position, Quaternion.identity);
        if (_health <= 0)
        {
            KillZombie();
        }
    }

    /// <summary>
    /// IEnumerators mean we can call these methods and continue execution from the caller.
    /// Great, normal methods do that you say. Yes, but here we can continue execution and
    /// sleep when we want and resume, while the caller continues on their merry way.
    /// </summary>
    /// <returns></returns>
    IEnumerator FlashZombieRed()
    {
        //Applies a 'flash' to a red color

        //Swap out the color for a damage color
        _spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(.2f);
        //restore the color
        _spriteRenderer.material.color = Color.white;

    }
    public void KillZombie()
    {
        _isDead = true;
        //if health<=0, state is dead and play 'die'
        _animator.SetBool("Die", true);
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        //Prevent detecting any collisions now - he's dead jim.
        GetComponent<Rigidbody2D>().isKinematic = true;

        //Kick off a separate task to fade the zombie away over two seconds
        StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(1);
        while (_spriteRenderer.color.a > 0)
        {
            var color = _spriteRenderer.color;
            //color.a is 0 to 1. So .5*time.deltaTime will take 2 seconds to fade out
            color.a -= (.5f * Time.deltaTime);

            _spriteRenderer.color = color;
            //wait for a frame
            yield return null;
        }
        Destroy(gameObject);
    }
    
    //OnTriggerEnter here


    //FixedUpdate here

}
