using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviourPunCallbacks, IPunObservable
{
    public bool automatic;
    public float timer;
    public float fireRate = .2f;
    public LayerMask mask;
    public GameObject bulletPref;
    public float power;

    private ShootGFX shootGFX;

    bool fire = false;

    private void Start()
    {
        timer = fireRate;
        shootGFX = ShootGFX.instance;
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
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
                fire = false;
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                ShootGun();
            }
        }

    }

    private void ShootGun()
    {
        fire = true;
        shootGFX.ShootGun();

        ShootGFX gfx = GetComponent<ShootGFX>();

        var bullet = Instantiate(bulletPref, gfx.firePointPlayer.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * power);

        //RaycastHit hitInfo;

        //if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 100, mask))
        //{
        //    if (hitInfo.collider.gameObject == gameObject) return;
        //    if(hitInfo.collider.gameObject.TryGetComponent(out PlayerController pc))
        //    {
        //        //GetComponent<PlayerController>().Health -= .1f;
        //        pc.Health -= .1f;
        //        print(pc.Health);
        //    }
        //    Debug.Log(hitInfo.collider.name);
        //}
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
