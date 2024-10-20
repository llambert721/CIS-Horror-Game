using UnityEngine;
using UnityEngine.SceneManagement;

public class SlenderManAI : MonoBehaviour
{
    public Transform player; // Reference to the player's GameObject
    public float teleportDistance = 10f; // Maximum teleportation distance
    public float teleportCooldown = 5f; // Time between teleportation attempts
    public float returnCooldown = 10f; // Time before returning to base spot
    [Range(0f, 1f)] private float chaseProbability = 0.65f; // Probability of chasing the player
    public float rotationSpeed = 5f; // Rotation speed when looking at the player
    public AudioClip teleportSound; // Reference to the teleport sound effect
    private AudioSource audioSource;

    public GameObject staticObject; // Reference to the "static" GameObject
    public float staticActivationRange = 5f; // Range at which "static" should be activated

    private Vector3 baseTeleportSpot;
    private float teleportTimer;
    private bool returningToBase;
    public LayerMask groundLayer;

    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    private bool isChasing; // is chasing when it's teleported to you and angry
    private float runSpeed = 2.8f;
    public Transform[] waypoints;
    int currentWaypointIndex = 0;
    // private float angryRunSpeed = 4.2f; not used yet

    private void Start()
    {
        baseTeleportSpot = transform.position;
        teleportTimer = teleportCooldown;

        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the teleport sound
        audioSource.clip = teleportSound;

        // Ensure the "static" object is initially turned off
        if (staticObject != null)
        {
            staticObject.SetActive(false);
        }
    }

    private void Update()
    {

        if (player == null)
        {
           
            return;
        }

        teleportTimer -= Time.deltaTime;

        if (teleportTimer <= 0f)
        {
            if (returningToBase)
            {
                TeleportToBaseSpot();
                teleportTimer = returnCooldown;
                returningToBase = false;
            }
            else
            {
                DecideTeleportAction();
                teleportTimer = teleportCooldown;
            }
        }

        RotateTowardsPlayer();

        // Check player distance and toggle the "static" object accordingly
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= staticActivationRange)
        {
            if (staticObject != null && !staticObject.activeSelf)
            {
                staticObject.SetActive(true);
            }
        }
        else
        {
            if (staticObject != null && staticObject.activeSelf)
            {
                staticObject.SetActive(false);
            }
        }

        EnvironmentView();
        
    }

    private void SetChaseProbability(float newProbability)
    {
        chaseProbability = newProbability;
    }

    private void DecideTeleportAction()
    {
        float randomValue = Random.value;

        if (randomValue <= chaseProbability)
        {
            TeleportNearPlayer();
        }
        else
        {
            TeleportToBaseSpot();
        }
    }

    private void Chase()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = runSpeed;
        navMeshAgent.SetDestination(player.position);
    }

    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, 25f, playerMask);

        for(int i = 0; i < playerInRange.Length; i++)
        {
            Debug.Log("monster sees the player");
            Transform playerTransform = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        }
        if (Vector3.Distance(transform.position, player.position) >= 25f)
        {
            StopChasing();
        }
        else
        {
            Chase();
        }
        if (Vector3.Distance(transform.position, player.position) <= 2f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // resets the game because the player is dead
        }
    }

    private void StopChasing()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    private void TeleportNearPlayer()
    {
        Vector3 randomPosition = player.position + Random.onUnitSphere * teleportDistance;

        RaycastHit hit;
        if (Physics.Raycast(randomPosition + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            // Set the spawn position to be the point where the raycast hits the ground
            randomPosition.y = hit.point.y;
        }

        transform.position = randomPosition;

        // Play the teleport sound
        audioSource.Play();
    }

    private void TeleportToBaseSpot()
    {
        transform.position = baseTeleportSpot;
        returningToBase = true;
        StopChasing();
    }

    private void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0f; // Ignore the vertical component

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            targetRotation *= Quaternion.Euler(0, -120, 0); // rotates 90 degrees so it's facing player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
