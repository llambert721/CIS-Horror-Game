using UnityEngine;
using UnityEngine.SceneManagement;

public class SlenderManAI : MonoBehaviour
{
    public Animator animator;
    public Transform monsterPrefab;
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

    private float footstepInterval = 0.8f; // Time between steps
    private float footstepTimer = 0f;
    private MonsterFootsteps footsteps;

    public LayerMask playerMask;
    public GameObject playerClass;
    private FirstPersonController playerObject;
    public LayerMask obstacleMask;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    private bool isChasing; // is chasing when it's teleported to you and angry
<<<<<<< Updated upstream:Assets/Scripts/SlenderManAI.cs
    private float runSpeed = 2.8f;
    public Transform[] waypoints;
    int currentWaypointIndex = 0;
=======
    private float runSpeed = 2.3f;
    private float sneakSpeed = 1f;
    public Transform[] waypoints;
    int currentWaypointIndex = 0;
    private bool justTeleported = false;
    private bool isSneaking = false;
>>>>>>> Stashed changes:Assets/Scripts/MonsterScripts/SlenderManAI.cs
    // private float angryRunSpeed = 4.2f; not used yet

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
<<<<<<< Updated upstream:Assets/Scripts/SlenderManAI.cs
        baseTeleportSpot = transform.position;
=======
        playerObject = playerClass.GetComponent<FirstPersonController>();
        footsteps = FindObjectOfType<MonsterFootsteps>();
        monsterSpawnpoint = transform.position;
        SetDifficulty(0);
>>>>>>> Stashed changes:Assets/Scripts/MonsterScripts/SlenderManAI.cs
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
        animator.SetFloat("Speed", navMeshAgent.speed);
        EnvironmentView();

        if (!isSneaking) // Check based on your movement logic
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepInterval - 0.4f)
            {
                footsteps.FootStep();
                footstepTimer = 0f;
            }
        }
        else if (isChasing)
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer >= footstepInterval)
            {
                footsteps.FootStep();
                footstepTimer = 0f;
            }
        }
        else
        {
            footstepTimer = 0f; // Reset timer if the monster isn't chasing
        }
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
            // 50/50 chance to either sprint after player or sneak up quietly
            float randomValue2 = Random.value;
            if (randomValue < 0.5)
            {
                SneakTeleportToPlayer();
            }
            else
            {
                SprintTeleportToPlayer();
            }
        }
        else
        {
            TeleportToBaseSpot();
        }
    }

    private void Chase()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
    }

    void EnvironmentView()
    {
<<<<<<< Updated upstream:Assets/Scripts/SlenderManAI.cs
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, 25f, playerMask);
=======
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, playerObject.playerSoundRadius, playerMask);
>>>>>>> Stashed changes:Assets/Scripts/MonsterScripts/SlenderManAI.cs

        for(int i = 0; i < playerInRange.Length; i++)
        {
            Debug.Log("monster senses the player");
            Transform playerTransform = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        }
<<<<<<< Updated upstream:Assets/Scripts/SlenderManAI.cs
        if (Vector3.Distance(transform.position, player.position) >= 25f)
=======
        if (Vector3.Distance(transform.position, player.position) >= playerObject.playerSoundRadius && !justTeleported)
>>>>>>> Stashed changes:Assets/Scripts/MonsterScripts/SlenderManAI.cs
        {
            StopChasing();
        }
        else
        {
            navMeshAgent.speed = runSpeed;
            Chase();
        }
        if (Vector3.Distance(transform.position, player.position) <= 1f)
        {
            //Goes to deathscene when player gets touched
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void NextPoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    private void StopChasing()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    public void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if(Vector3.Distance(transform.position, player) <= 0.3)
        {
            // tom left off here
        }
    }

    private void SprintTeleportToPlayer()
    {
        Vector3 randomPosition = player.position + Random.onUnitSphere * teleportDistance;
<<<<<<< Updated upstream:Assets/Scripts/SlenderManAI.cs
=======
        navMeshAgent.speed = runSpeed;
        Chase();
        isSneaking = false;

        // start timer for teleporting back to spawnpoint
        returningToBase = true;
        teleportTimer = returnCooldown;
        Debug.Log("monster will teleport home after " + teleportTimer + " seconds");
>>>>>>> Stashed changes:Assets/Scripts/MonsterScripts/SlenderManAI.cs

        RaycastHit hit;
        if (Physics.Raycast(randomPosition + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            // Set the spawn position to be the point where the raycast hits the ground
            randomPosition.y = hit.point.y;
        }

        transform.position = randomPosition;

        // Play the teleport sound
        audioSource.Play();
        // we no longer play any teleport sound so that it's a bit scarier and harder, but tom is keeping it on for now for testing
    }

    private void SneakTeleportToPlayer()
    {
        // tom is going to change this method's name to SneakTeleportToPlayer() and add SprintTeleportToPlayer()
        Vector3 randomPosition = player.position + Random.onUnitSphere * teleportDistance;
        navMeshAgent.speed = sneakSpeed;
        Chase();
        isSneaking = true;

        // start timer for teleporting back to spawnpoint
        returningToBase = true;
        teleportTimer = returnCooldown;
        Debug.Log("monster will teleport home after " + teleportTimer + " seconds");

        RaycastHit hit;
        if (Physics.Raycast(randomPosition + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            // Set the spawn position to be the point where the raycast hits the ground
            randomPosition.y = hit.point.y;
        }

        transform.position = randomPosition;

        // Play the teleport sound
        audioSource.Play();
        // we no longer play any teleport sound so that it's a bit scarier and harder, but tom is keeping it on for now for testing
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
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            monsterPrefab.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
