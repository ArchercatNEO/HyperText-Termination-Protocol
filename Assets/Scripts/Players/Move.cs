using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 15f;
    [SerializeField] float gravity = 50f;
    [SerializeField] float jump = 20f;

    Vector3 direction;


    // Update is called once per frame
    void Update(){

        float metaSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift)) metaSpeed *= 2;

        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");
        
        if (controller.isGrounded){
            direction = new Vector3(deltaX, 0, deltaZ);
            direction = transform.TransformDirection(direction) * metaSpeed;

            if (Input.GetKey(KeyCode.Space)) direction.y = jump;
        }

        direction.y -= gravity * Time.deltaTime; 
        controller.Move(direction * Time.deltaTime);
    }
    

    
}
