using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    [Tooltip("The key that you need to press to toss a stone")][SerializeField] KeyCode ThrowKey = KeyCode.G;

    public Projectile projectile;
    private FirstPersonController playerScriptObject;
    private bool readyToThrow = true;
    public float ThrowForce;

    public Transform playerCamTransform;
    public float timeBetweenThrows;

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    public void MyInput()
    {
        if (Input.GetKeyDown(ThrowKey) && readyToThrow)
        {
            Debug.Log("Input G Successful");
            InitializeProjectile();
            Invoke("ResetThrow", timeBetweenThrows);
        }
    }

    public void InitializeProjectile()
    {
        Vector3 playerDirection = playerCamTransform.forward;
        readyToThrow = false;

        Projectile currentRock = Instantiate(projectile, playerCamTransform.position, Quaternion.identity);
        currentRock.Initialize(playerDirection * ThrowForce);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
