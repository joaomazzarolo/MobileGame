using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public Vector2 velocity;
    public HealthBase healthBase;
    private Animator _currentPlayer;
    public ParticleSystem jumpVFX;
    public GameObject jumpVFXObject;
    public ParticleSystem runVFX;
    public GameObject runVFXObject;
    public AudioSource jumpSFX;

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;

    private void Awake()
    {
        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerDeath;
        }
        _currentPlayer = Instantiate(soPlayerSetup.player, transform);

        jumpVFXObject.SetActive(true);
        runVFXObject.SetActive(true);
    }

    private void OnPlayerDeath()
    {
        healthBase.OnKill -= OnPlayerDeath;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }
    private void OnValidate()
    {
        if (_currentPlayer == null) _currentPlayer = GetComponent<Animator>();
    }
    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            if (myRigidBody.transform.localScale.x != -1)
            {
                myRigidBody.transform.DOScaleX(-1, .1f);
            }
            myRigidBody.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) ? -soPlayerSetup.runSpeed : -soPlayerSetup.speed, myRigidBody.velocity.y);
            PlayRunVFX();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            if (myRigidBody.transform.localScale.x == -1)
            {
                myRigidBody.transform.DOScaleX(1, .1f);
            }
            myRigidBody.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) ? soPlayerSetup.runSpeed : soPlayerSetup.speed, myRigidBody.velocity.y);
            PlayRunVFX();
        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
        }

        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity += soPlayerSetup.friction * -1;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity -= soPlayerSetup.friction * -1;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && myRigidBody.velocity.y == 0)
        {
            myRigidBody.velocity = Vector2.up * soPlayerSetup.jumpForce;
            myRigidBody.transform.localScale = Vector2.one;
            jumpVFX.Play();
            jumpSFX.Play();
            DOTween.Kill(myRigidBody.transform);
            HandleScaleJump();

        }
    }

    private void HandleScaleJump()
    {
        myRigidBody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigidBody.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }

    private void PlayRunVFX()
    {
        runVFX.Play();
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
