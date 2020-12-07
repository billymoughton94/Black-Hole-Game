
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float damage = 10f;
    public float range = 50f;
    public float fireRate = 15f;
    public Transform gunEnd;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private float nextFire = 1f;

    public Camera fpsCam;
    // Update is called once per frame

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot() //MIGHT BE GOOD TO ADD A TIMER UNTIL NEXT RAYCAST CAN BE MADE OTHERWISE KILLING WILL BE SUPER EASY //
    {
        RaycastHit hit;
       if ( Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

           Monster_Controller target = hit.transform.GetComponent<Monster_Controller>();

            if (target != null)
            {
                target.takeDamage(damage);
            }
        }
    }
}
