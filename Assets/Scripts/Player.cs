using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Death and Respawning")]
    [Tooltip("How long after the player's death, in seconds, before they are respawned?")]
    public float respawnWaitTime = 2f;
    private bool dead = false;
    private Vector3 spawnPoint;
    private Quaternion spawnRotation;
    
    [Header("References")]
    public Transform trans;
    public Transform modelTrans;
    public CharacterController characterController;
    [Header("Movement")]
    [Tooltip("Units moved per second at maximum speed.")]
    public float movespeed = 24;
    [Tooltip("Time, in seconds, to reach maximum speed.")]
    public float timeToMaxSpeed = .26f;
    private float VelocityGainPerSecond { get { return movespeed /
                                                       timeToMaxSpeed; } }
    [Tooltip("Time, in seconds, to go from maximum speed to stationary.")]
    public float timeToLoseMaxSpeed = .2f;
    private float VelocityLossPerSecond { get { return movespeed /
                                                       timeToLoseMaxSpeed; } }
    [Tooltip("Multiplier for momentum when attempting to move in a direction opposite the current traveling direction (e.g. trying to move right when already moving left).")]
    public float reverseMomentumMultiplier = 2.2f;
    private Vector3 movementVelocity = Vector3.zero;

    private void Movement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (movementVelocity.z >= 0) 
                movementVelocity.z = Mathf.Min(movespeed,movementVelocity.z + 
                                                         VelocityGainPerSecond * Time.deltaTime);//delta tager højde for framerate. Hvis der er lav framerate, blive tiden mellem frames multipliceret med VelocityGainPerSecond, for at beholde den samme fart på tværs af enheder.
            else 
                movementVelocity.z = Mathf.Min(0,movementVelocity.z +
                                                 VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (movementVelocity.z > 0) 
                movementVelocity.z = Mathf.Max(0, movementVelocity.z -
                                                  VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
            else 
                movementVelocity.z = Mathf.Max(-movespeed, movementVelocity.z -
                                                           VelocityGainPerSecond * Time.deltaTime);
        }
        else 
        {
            if (movementVelocity.z > 0) 
                movementVelocity.z = Mathf.Max(0,movementVelocity.z -
                                             VelocityLossPerSecond * Time.deltaTime);
            else 
                movementVelocity.z = Mathf.Min(0,movementVelocity.z +
                                             VelocityLossPerSecond * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (movementVelocity.x >= 0) 
                movementVelocity.x = Mathf.Min(movespeed,movementVelocity.x +
                                                         VelocityGainPerSecond * Time.deltaTime);
            else 
                movementVelocity.x = Mathf.Min(0,movementVelocity.x +
                                                 VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (movementVelocity.x > 0)
                movementVelocity.x = Mathf.Max(0,movementVelocity.x -
                                                 VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
            else 
                movementVelocity.x = Mathf.Max(-movespeed,movementVelocity.x -
                                                          VelocityGainPerSecond * Time.deltaTime);
        }
        else 
        {
            if (movementVelocity.x > 0) 
                
                movementVelocity.x = Mathf.Max(0,movementVelocity.x -
                                                VelocityLossPerSecond * 
                                                Time.deltaTime);
            else 
            
                movementVelocity.x = Mathf.Min(0,movementVelocity.x +
                                                VelocityLossPerSecond * 
                                                Time.deltaTime);
        }
        
        if (movementVelocity.x != 0 || movementVelocity.z != 0)
        {
            
            characterController.Move(movementVelocity * Time.deltaTime);
            modelTrans.rotation = Quaternion.Slerp(modelTrans.rotation,Quaternion.
                LookRotation(movementVelocity*Time.deltaTime),.18F);
        }
    }
    
    public void Die()
    {
        if (!dead)
        {
            dead = true;
            Invoke("Respawn",respawnWaitTime);
            movementVelocity = Vector3.zero;
            enabled = false;
            characterController.enabled = false;
            modelTrans.gameObject.SetActive(false);
        }
    }
    
    public void Respawn()
    {
        dead = false;
        trans.position = spawnPoint;
        enabled = true;
        characterController.enabled = true;
        modelTrans.gameObject.SetActive(true);
        modelTrans.rotation = spawnRotation;
    }

    
    public float Speed => Mathf.Abs(movementVelocity.x) + Mathf.Abs(movementVelocity.z) / 2;

    public void Start()
    {
        spawnPoint = trans.position;
        spawnRotation = modelTrans.rotation;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
            Die();
        Movement();
    }
}
