using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public void Interact()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
    }
}
