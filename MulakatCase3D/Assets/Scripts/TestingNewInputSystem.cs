using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingNewInputSystem : MonoBehaviour
{
    private Rigidbody sphereRigidbody;
    private void Awake()
    {
        sphereRigidbody = GetComponent<Rigidbody>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            sphereRigidbody.AddForce(Vector3.up * 15f, ForceMode.Impulse);
            
        }
        
    }


}
