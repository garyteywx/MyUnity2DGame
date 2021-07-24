using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private bool spaceKeyPressed;
    private float horizontalInput;
    private Rigidbody ridgidbodyComponent;
    private int superJumpsRemanining;

    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        ridgidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Key Pressed");
            spaceKeyPressed=true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    //Fixed Update is User Define update rate
    private void FixedUpdate() 
    {
        ridgidbodyComponent.velocity = new Vector3(horizontalInput, ridgidbodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if(spaceKeyPressed == true)
        {
            float jumpPower = 5f;
            if(superJumpsRemanining>0)
            {
                jumpPower *= 2;
                superJumpsRemanining--;
            }
            ridgidbodyComponent.AddForce(Vector3.up*jumpPower, ForceMode.VelocityChange);
            spaceKeyPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpsRemanining++;
        }

        if (other.gameObject.layer == 10)
        {
            SceneManager.LoadScene("SampleScene");
            Debug.Log("Encountered");
        }
    }
}
