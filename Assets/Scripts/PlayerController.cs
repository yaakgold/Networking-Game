using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    public float Health = 1f;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    public GameObject PlayerUiPrefab;
    [SerializeField]
    private float _moveSpeed = -10f;
    [SerializeField]
    private float _gravity = -10f;
    [SerializeField]
    private float _jumpHeight = 3f;
    [SerializeField]
    private float _groundDistance = 0.5f;
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private LayerMask _groundMask;

    private bool _isGrounded;
    private Vector3 _velocity;
    private CharacterController _controller;
    private Animator _animator;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }

        DontDestroyOnLoad(gameObject);

        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if(!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
            gameObject.layer = 9;
        }

        if (PlayerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(PlayerUiPrefab);
            _uiGo.GetComponent<PlayerUI>().SetTarget(this);
        }
    }

    void Update()
    {
        Gravity();
        if (photonView.IsMine)
        {
            Movement();
            print(Health);
            if (Health <= 0f)
            {
                GameManager.Instance.LeaveRoom();
            }
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertial = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertial;

        _controller.Move(move * _moveSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }

    private void Gravity()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if(_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void JumpAnimation()
    {
        _animator.SetBool("grounded", _isGrounded);
    }

    public void TakeDamage()
    {
        Health -= .1f;
    }

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //We own this player: send other our data
            //stream.SendNext(transform.position);
            stream.SendNext(Health);
        }
        else
        {
            //Network player, receive data
            //transform.position = (Vector3)stream.ReceiveNext();
            Health = (float)stream.ReceiveNext();
        }
    }

    #endregion
}
