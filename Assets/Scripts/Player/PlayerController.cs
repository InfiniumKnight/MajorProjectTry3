using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //For UI health bar
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float Speed = 3.0f; //Walking speed
    private Vector2 move;

    [SerializeField] private Animator animator;

    public float health = 100f; //Max health
    private float currentHealth; //Current player health
    public Slider healthBar; //Setting up health bar UI
    public float chipSpeed = 2.0f;
    private float lerpTimer;

    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Weapon Levels")]
    [SerializeField] public int SwordLevel;
    [SerializeField] public int GunLevel;
    [SerializeField] public int BombLevel;

    public delegate void PlayerEventHandler();
    public event PlayerEventHandler OnPlayerDeath;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    private void Start()
    { 
        currentHealth = health; //Initalize player health
    }

    private void Update()
    {
        movePlayer();
        UpdateHealthUI();
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    void movePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        transform.Translate(movement * Speed * Time.deltaTime, Space.World);

        if (movement != Vector3.zero)
        {
            try
            {
                animator.SetBool("isRunning", true);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            }
            catch (System.NullReferenceException exception)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
                Debug.Log("animator is missing" + exception.ToString());
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void HandleHealth(float enemyDamage)
    {
        currentHealth -= enemyDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, health); //Make sure health doesn't go below zero
        lerpTimer = 0f;
        Debug.Log("Health is at " + currentHealth);
        if (currentHealth <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        Debug.Log("Player has died!");
        OnPlayerDeath?.Invoke();
        //No destroy gameobject yet due to not knowing if we are going to have a title screen or how player is going to die
    }

    private void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = currentHealth / health;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void RestoreHealth(float healAmount)
    {
        currentHealth += healAmount;
        lerpTimer = 0f;
        currentHealth = health;
    }
}