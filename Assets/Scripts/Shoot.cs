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
        if(automatic)
        {
            if (!photonView.IsMine) return;
            if (Input.GetMouseButton(0) || fire)
            {
                timer += Time.deltaTime;
                if(timer >= fireRate || fire)
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
        photonView.RPC("Fire", photonView.Owner);

<<<<<<< HEAD
        AudioManager.Instance.Play("bang");

        var bullet = Instantiate(bulletPref, gfx.firePointPlayer.transform.position, Quaternion.identity);
=======
        var bullet = PhotonNetwork.Instantiate("Bullet", shootGFX.firePointPlayer.transform.position, Quaternion.identity);
>>>>>>> 50e8e258c3db2dd39cdfae8314440241f590f577
        bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * power);
    }

    [PunRPC]
    void Fire()
    {
        shootGFX.ShootGun();
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
