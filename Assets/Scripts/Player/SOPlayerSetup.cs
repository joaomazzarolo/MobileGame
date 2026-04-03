using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject   
{
    public Animator player;
    public SOString soStringName;

    public float speed = 5;
    public float runSpeed = 15;
    public float jumpForce = 20;
    public Vector2 friction = new Vector2(.1f, 0);

    public string boolRun = "Run";
    public string triggerDeath = "Death";


    public float jumpScaleY;
    public float jumpScaleX;
    public float animationDuration;
    public Ease ease = Ease.OutBack;
}
