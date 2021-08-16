using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGFX : MonoBehaviour
{
    #region Singleton

    public static ShootGFX instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject muzzleFlash;
    public Transform firePointPlayer;
    public Transform firePointOffScreen;

    public void ShootGun()
    {
        Instantiate(muzzleFlash, firePointOffScreen.position, Quaternion.identity);
        Instantiate(muzzleFlash, firePointPlayer.position, Quaternion.identity);
    }
}
