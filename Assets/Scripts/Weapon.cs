using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] float bulletSpeed = 100f;
    [SerializeField] Transform projectileInitialPos;
    [SerializeField] float shootingReloadTime = 1;



    [Header("Weapon")]
    [SerializeField] Sprite weapon;
    [SerializeField] Sprite emptyWeapon;

    private float gunCadency;
    private bool hasFired = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectGunCadency();
        Fire();
    }


    void Fire()
    {
        if (Input.GetButtonDown("Fire1") && gunCadency <= 0)
        {
            GameObject proj = Instantiate(projectile, projectileInitialPos.position, projectileInitialPos.rotation);
            Rigidbody2D instantiatedProjectile = proj.GetComponent<Rigidbody2D>();
            Vector2 v = instantiatedProjectile.GetRelativeVector(Vector2.up * bulletSpeed);
            instantiatedProjectile.velocity = v;
            gunCadency = shootingReloadTime;
            hasFired = true;
            GetComponent<AudioSource>().Play();
        }
    }

    private void DetectGunCadency()
    {
        if (gunCadency > 0 && hasFired)
        {
            gunCadency -= Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().sprite = emptyWeapon;
        }
        else if (gunCadency <= 0)
        {
            hasFired = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = weapon;
        }
    }
}
