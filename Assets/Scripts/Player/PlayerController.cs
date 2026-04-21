using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    public float speed = 1f;
    public Transform target;
    public float lerpSpeed = 1f;
    public string enemyTag = "Enemy";
    public string endLineTag = "EndLine";
    public GameObject endScreen;
    public TextMeshPro uiTextPowerUp;

    private Vector3 _pos;
    private bool _canRun;

    private float _currentSpeed;
    private Vector3 _startPosition;
    public bool invincible = false;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    private float _baseSpeedToAnimation = 7;

    private void Start()
    {
        animatorManager.Play(AnimatorManager.AnimationType.IDLE);
        _startPosition = transform.position;
        ResetSpeed();
    }
    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(enemyTag))
        {
            if (!invincible)
            {
                MoveBack(collision.transform);
                LoseGame();
            }
        }
        else if (collision.transform.CompareTag(endLineTag))
        {
            WinGame();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(enemyTag))
        {
            if (!invincible)
            {
                MoveBack(other.transform);
                LoseGame();
            }
        } else if (other.transform.CompareTag(endLineTag))
        {
            WinGame();
        }

    }

    private void MoveBack(Transform t)
    {
        t.DOMoveZ(1f, .3f).SetRelative();
    }

    private void WinGame()
    {
        _canRun = false;
        animatorManager.Play(AnimatorManager.AnimationType.IDLE);
        endScreen.SetActive(true);
    }

    private void LoseGame()
    {
        _canRun = false;
        animatorManager.Play(AnimatorManager.AnimationType.DEAD);
        endScreen.SetActive(true);
    }
    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed/_baseSpeedToAnimation);
    }

    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }
    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }
    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }
    public void SetInvincible(bool b)
    {
        invincible = b;
    }
    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y + amount,
        animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }
    public void ResetHeight()
    {
        transform.DOMoveY(_startPosition.y, .1f);
    }
    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
}
