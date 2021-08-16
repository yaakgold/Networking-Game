using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour, IPunObservable
{
    public bool automatic;
    public float timer;
    public float fireRate = .2f;
    public LayerMask mask;

    private ShootGFX shootGFX;

    bool fire = false;

    private void Start()
    {
        timer = fireRate;
        shootGFX = ShootGFX.instance;
    }

    private void Update()
    {
        if(automatic)
        {
            if(Input.GetMouseButton(0))
            {
                timer += Time.deltaTime;
                if(timer >= fireRate)
                {
                    ShootGun();
                    timer = 0;
                }
            }
            if(timer < fireRate)
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0) || fire)
            {
                ShootGun();
            }
        }

    }

    private void ShootGun()
    {
        shootGFX.ShootGun();

        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Vector2.zero);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 100, mask))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out PlayerController pc))
            {
                GetComponent<PlayerController>().Health -= .1f;
                //pc.TakeDamage();
            }
            Debug.Log(hitInfo.collider.name);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(fire);
        }
        else
        {
            fire = (bool)stream.ReceiveNext();
        }
    }
}
