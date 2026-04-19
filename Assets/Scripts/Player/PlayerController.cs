using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

public class PlayerController : Singleton<PlayerController>
{
    public float speed = 1f;
    public Transform target;
    public float lerpSpeed = 1f;
    public string enemyTag = "Enemy";
    public string endLineTag = "EndLine";
    public GameObject endScreen;

    private Vector3 _pos;
    private bool _canRun;

    private float _currentSpeed;
    private Vector3 _startPosition;
    private void Start()
    {
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
        if (collision.transform.tag == enemyTag)
        {
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "EndLine") EndGame();

    }

    private void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }

    public void StartToRun()
    {
        _canRun = true;
    }

    public void SetPowerUpText(string s)
    {
        //uiTextPowerUp.text = s;
    }
    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }
    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }
}
