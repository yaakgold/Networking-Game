using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private float _speed;
    private float _horizontalSpeed;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        AnimatePlayer();
    }

    private void AnimatePlayer()
    {
        _speed = Input.GetAxis("Vertical");
        _horizontalSpeed = Input.GetAxis("Horizontal");

        animator.SetFloat("speed", _speed);
        animator.SetFloat("horizontal speed", _horizontalSpeed);
    }
}
