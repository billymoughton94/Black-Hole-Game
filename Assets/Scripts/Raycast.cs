using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float damage = 10f;
    public float range = 50f;
    public float fireRate = 100f;
    public Transform gunEnd;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private float nextFire = 0f;

    public ParticleSystem muzzleFlash;

    public Camera fpsCam;
    Inventory_Controller inventory;

    AudioSource gunShotAudio;

    private void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory_Controller>();
        gunShotAudio = GetComponent<AudioSource>();
    }

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
        gunShotAudio.Play();
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            switch (hit.transform.tag)
            {
                case "Monster":
                    Monster_Controller target = hit.transform.GetComponent<Monster_Controller>();
                    if (target != null)
                    {
                        target.takeDamage(damage);
                    }
                    break;
                case "Iron":
                    inventory.addItem(new Item("Iron", 5));
                    Destroy(hit.transform.gameObject);
                    break;
                case "Berry":
                    inventory.addItem(new Item("Berry", 1, true));
                    Destroy(hit.transform.gameObject);
                    break;
                case "Obsidian":
                    inventory.addItem(new Item("Obsidian", 2));
                    Destroy(hit.transform.gameObject);
                    break;
            }
        }
    }
}
