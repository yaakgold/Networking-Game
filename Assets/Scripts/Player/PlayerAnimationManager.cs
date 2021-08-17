using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviourPun
{
    #region Private Fields

    [SerializeField]
    private float directionDampTime = .25f;
    private Animator anim;

    #endregion

    #region MonoBehavior Callbacks


    private void Start()
    {
        anim = GetComponent<Animator>();
        if(!anim)
        {
            Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
        }
    }

    private void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;

        if (!anim)
            return;

        // deal with Jumping
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        // only allow jumping if we are running.
        if (stateInfo.IsName("Base Layer.Run"))
        {
            // When using trigger parameter
            if (Input.GetButtonDown("Fire2"))
            {
                anim.SetTrigger("Jump");
            }
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(v < 0)
        {
            v = 0;
        }

        anim.SetFloat("Speed", h * h + v * v);
        anim.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
    }

    #endregion
}
