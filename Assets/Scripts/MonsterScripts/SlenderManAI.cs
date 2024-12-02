using UnityEngine;
using UnityEngine.SceneManagement;

public class SlenderManAI : MonoBehaviour
{
    public Animator animator;
    public Transform monsterPrefab;
    public Transform player; // Reference to the player's GameObject
    public float teleportDistance = 10f; // Maximum teleportation distance
    private float teleportCooldown = 5f; // Time between teleportation attempts
    public float returnCooldown = 10f; // Time before returning to base spot
    [Range(0f, 1f)] private float chaseProbability = 0.65f; // Probability of chasing the player
    public float rotationSpeed = 5f; // Rotation speed when looking at the player
    private AudioSource audioSource;

    public GameObject staticObject; // Reference to the "static" GameObject
    public float staticActivationRange = 5f; // Range at which "static" should be activated

    private Vector3 monsterSpawnpoint;
    private float teleportTimer;
    private bool returningToBase;
    public LayerMask groundLayer;

    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    private bool isChasing; // is chasing when it's teleported to you and angry
    private float runSpeed = 2.3f;
    public Transform[] waypoints;
    int currentWaypointIndex = 0;
    // private float angryRunSpeed = 4.2f; not used yet

    private float[] teleportCooldowns = { 60, 50, 40, 30, 5 };
    private float[] chaseProbabilities = { 0.4f, 0.5f, 0.6f, 0.7f, 0.65f };

    private float footstepInterval = 1f; // Time between steps
    private float footstepTimer = 0f;
    private MonsterFootsteps footsteps;

    private void Start()
    {
        footsteps = FindObjectOfType<MonsterFootsteps>();
        animator = GetComponentInChildren<Animator>();
        monsterSpawnpoint = transform.position;
        SetDifficulty(0);
        teleportTimer = teleportCooldown;

        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }


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
                returningToBase = false;
            }
            else
            {
                DecideTeleportAction();
            }
            teleportTimer = teleportCooldown;
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
        animator.SetFloat("Speed", navMeshAgent.speed);
        EnvironmentView();
        
        if (navMeshAgent.isStopped == false)
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepInterval - 0.4f)
            {
                footsteps.Footstep();
                footstepTimer = 0f;
            }
        }
        else { footstepTimer = 0f; }
    }

    public void SetDifficulty(int count)
    {
        SetChaseProbability(chaseProbabilities[count]);
        SetTeleportCooldown(teleportCooldowns[count]);
        teleportTimer = teleportCooldown; // do this so it resets the timer
    }

    private void SetChaseProbability(float newProbability)
    {
        chaseProbability = newProbability;
        Debug.Log("Chase Probability: " + chaseProbability);
    }

    private void SetTeleportCooldown(float newCooldown)
    {
        teleportCooldown = newCooldown;
        Debug.Log("Teleport Cooldown: " + teleportCooldown);
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
            // TeleportToBaseSpot(); // this means that it'll randomly decide to teleport to its spawnpoint sometimes
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
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, 12f, playerMask);

        for(int i = 0; i < playerInRange.Length; i++)
        {
            Debug.Log("monster sees the player");
            Transform playerTransform = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        }
        if (Vector3.Distance(transform.position, player.position) >= 12f)
        {
            StopChasing();
        }
        else
        {
            Chase();
        }
        if (Vector3.Distance(transform.position, player.position) <= 1f)
        {
            //Goes to deathscene when player gets touched
            SceneManager.LoadScene("JumpScare");
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

        // start timer for teleporting back to spawnpoint
        returningToBase = true;
        teleportTimer = returnCooldown;

        RaycastHit hit;
        if (Physics.Raycast(randomPosition + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            // Set the spawn position to be the point where the raycast hits the ground
            randomPosition.y = hit.point.y;
        }

        transform.position = randomPosition;

    }

    private void TeleportToBaseSpot()
    {
        transform.position = monsterSpawnpoint;
        returningToBase = false;
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
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            monsterPrefab.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
