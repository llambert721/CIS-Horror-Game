using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpScare : MonoBehaviour
{
    public float targetY = -0.72f; // The target y-position
    public float speed = 10f; // Speed of the movement, adjust to make it "very quick"
    public string sceneToLoad = "DeathScreen"; // Name of the scene to load

    private void Start()
    {
        // Start the movement coroutine
        StartCoroutine(MoveToTarget());
    }

    private System.Collections.IEnumerator MoveToTarget()
    {
        // Move the GameObject until it reaches the target position
        while (Mathf.Abs(transform.position.y - targetY) > 0.01f)
        {
            // Move towards the target position quickly
            Vector3 newPosition = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, targetY, transform.position.z),
                speed * Time.deltaTime);

            transform.position = newPosition;
            yield return null; // Wait until the next frame
        }

        // Snap to target position to prevent any small discrepancies
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);

        // Wait for 2 seconds before loading the next scene
        yield return new WaitForSeconds(2f);

        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
