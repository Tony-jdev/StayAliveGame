using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PickUpController : MonoBehaviour
{
    [SerializeField]
    private ProjectileGunTutorial gunScript;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private BoxCollider coll;
    [SerializeField]
    private Transform player, gunContainer, fpsCam;

    [SerializeField]
    private float pickUpRange;
    [SerializeField]
    private float dropForwardForce, dropUpwardForce;

    [SerializeField]
    private bool equipped;
    private static bool slotFull;
    
    [SerializeField]
    private TextMeshProUGUI ammunitionDisplay;
    [SerializeField]
    private Button shootButton;
    [SerializeField]
    private Button pickUpButton;

    private void Start()
    {
        //Setup
        if (!equipped)
        {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
            ammunitionDisplay.enabled = false;
            shootButton.gameObject.SetActive(false);
            pickUpButton.gameObject.SetActive(false);
        }
        if (equipped)
        {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
            ammunitionDisplay.enabled = true;
            shootButton.gameObject.SetActive(true);
            pickUpButton.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        
        if(!equipped && distanceToPlayer.magnitude <= pickUpRange && !slotFull)pickUpButton.gameObject.SetActive(true);
        else if(equipped || distanceToPlayer.magnitude >= pickUpRange)
        {
            pickUpButton.gameObject.SetActive(false);
        }
        
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    public void Change()
    {
        
    }
    
    private void PickUp()
    {
        gunScript.enabled = true;
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;
        ammunitionDisplay.enabled = true;
        shootButton.gameObject.SetActive(true);
    }

    private void Drop()
    {
        gunScript.enabled = false;
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);
        
        ammunitionDisplay.enabled = false;
        shootButton.gameObject.SetActive(false);
        pickUpButton.gameObject.SetActive(false);
    }
}
