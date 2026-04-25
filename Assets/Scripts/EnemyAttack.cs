using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public Animator animator;
    public SwordBlock playerBlock;

    [Header("Attack Timing")]
    public float delay = 1.5f;
    float timer;

    bool isAttacking = false;
    bool hasCheckedBlock = false;

    float currentSpeed = 0.4f;
    public float maxSpeed = 1f;
    public float speedIncrease = 0.05f;

    public int currentAttackDirection;

    float attackTimer = 0f;

    public float impactPoint = 0.4f;

    [Header("Arrow UI")]
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject upArrow;
    public GameObject downArrow;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] blockSounds;

    [Header("Health")]
    public Image healthBar;
    public float maxHealth = 100f;
    public float currentHealth;
    public float damagePerHit = 20f;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public float restartDelay = 2.5f;

    bool isDead = false;

    //  FINAL BLOW
    [Header("Final Blow")]
    public GameObject finalBlowText;

    bool canFinalBlow = false;
    bool finalBlowTriggered = false;

    //  SWORD SLASH
    [Header("Sword Slash")]
    public Transform sword;
    public Transform slashStartPoint;
    public Transform slashEndPoint;
    public float slashDuration = 0.25f;

    void Start()
    {
        timer = delay;
        animator.speed = currentSpeed;

        currentHealth = maxHealth;

        HideArrows();
        UpdateHealthUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (finalBlowText != null)
            finalBlowText.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;

        //  FINAL BLOW INPUT
        if (canFinalBlow && !finalBlowTriggered)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine(DoFinalBlow());
            }
            return; // stop combat loop
        }

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;

            float impactTime = impactPoint / animator.speed;
            float attackDuration = 1f / animator.speed;

            if (!hasCheckedBlock && attackTimer >= impactTime)
            {
                CheckBlock();
                hasCheckedBlock = true;
            }

            if (attackTimer >= attackDuration)
            {
                isAttacking = false;
                hasCheckedBlock = false;
                timer = delay;

                HideArrows();
            }

            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            DoAttack();
        }
    }

    void DoAttack()
    {
        if (canFinalBlow) return;

        isAttacking = true;
        attackTimer = 0f;

        currentAttackDirection = Random.Range(0, 4);

        ShowArrow(currentAttackDirection);

        animator.SetInteger("SlashIndex", currentAttackDirection);
        animator.SetTrigger("Attack");

        currentSpeed += speedIncrease;
        currentSpeed = Mathf.Clamp(currentSpeed, 0.4f, maxSpeed);

        animator.speed = currentSpeed;

        //  trigger final blow phase
        if (currentSpeed >= maxSpeed && !canFinalBlow)
        {
            ActivateFinalBlow();
        }
    }

    void ActivateFinalBlow()
    {
        canFinalBlow = true;

        //  stop everything
        isAttacking = false;
        animator.speed = 0f;
        HideArrows();

        if (finalBlowText != null)
            finalBlowText.SetActive(true);

        Debug.Log("FINAL BLOW AVAILABLE");
    }

    void ShowArrow(int direction)
    {
        HideArrows();

        switch (direction)
        {
            case 0: leftArrow.SetActive(true); break;
            case 1: rightArrow.SetActive(true); break;
            case 2: upArrow.SetActive(true); break;
            case 3: downArrow.SetActive(true); break;
        }
    }

    void HideArrows()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        upArrow.SetActive(false);
        downArrow.SetActive(false);
    }

    void CheckBlock()
    {
        if (canFinalBlow) return;

        if (playerBlock.currentBlockDirection == currentAttackDirection)
        {
            Debug.Log("BLOCK SUCCESS");
            PlayBlockSound();
        }
        else
        {
            Debug.Log("PLAYER HIT");
            TakeDamage(damagePerHit);
        }
    }

    void PlayBlockSound()
    {
        if (blockSounds.Length == 0) return;

        int index = Random.Range(0, blockSounds.Length);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(blockSounds[index]);
    }

    void TakeDamage(float amount)
    {
        if (canFinalBlow) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.fillAmount = currentHealth / maxHealth;
    }

    void Die()
    {
        isDead = true;

        Debug.Log("PLAYER DEAD");

        animator.speed = 0f;
        HideArrows();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        StartCoroutine(RestartAfterDelay());
    }

    IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator DoFinalBlow()
    {
        finalBlowTriggered = true;
        isDead = true;

        Debug.Log("FINAL BLOW TRIGGERED");

        if (finalBlowText != null)
            finalBlowText.SetActive(false);

        //  slow motion (optional but nice)
        Time.timeScale = 0.3f;

        //  slash
        yield return StartCoroutine(SlashSword());

        Time.timeScale = 1f;

        // remove enemy
        gameObject.SetActive(false);

        SceneManager.LoadScene("EndScene");
    }

    IEnumerator SlashSword()
    {
        float time = 0f;

        Vector3 startPos = slashStartPoint.position;
        Vector3 endPos = slashEndPoint.position;

        Quaternion startRot = sword.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0, 0, 120f);

        while (time < slashDuration)
        {
            float t = time / slashDuration;

            sword.position = Vector3.Lerp(startPos, endPos, t);
            sword.rotation = Quaternion.Lerp(startRot, endRot, t);

            time += Time.deltaTime;
            yield return null;
        }

        sword.position = endPos;
        sword.rotation = endRot;
    }
}