using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //For UI health bar
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float Speed; //Walking speed
    private Vector2 move;

    //aniamtors and models
    [SerializeField] private Animator animator;
    [SerializeField] private Animator AlienAnimator;
    [SerializeField] private Animator RobotAnimator;
    [SerializeField] private Animator TankAnimator;
    [SerializeField] private GameObject AlienModel;
    [SerializeField] private GameObject RobotModel;
    [SerializeField] private GameObject TankModel;

    public float health; //Max health
    private float currentHealth; //Current player health
    public Slider healthBar; //Setting up health bar UI
    public float chipSpeed = 2.0f;
    private float lerpTimer;

    public Image frontHealthBar;
    public Image backHealthBar;

    public delegate void PlayerEventHandler();
    public event PlayerEventHandler OnPlayerDeath;

    public int coinAmount = 0; // Sets the coin amount of the player to 0

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //sets animatior, model, and stats based on selected character
        if (Characterselected.AlienSelected)
        {
            AlienModel.SetActive(true);
            animator = AlienAnimator;
            health = 50f;
            Speed = 6f;
        }
        else if (Characterselected.RobotSelected)
        {
            RobotModel.SetActive(true);
            animator = RobotAnimator;
            health = 75f;
            Speed = 4f;
        }
        else if (Characterselected.TankSelected)
        {
            TankModel.SetActive(true);
            animator = TankAnimator;
            health = 100f;
            Speed = 2f;
        }
        currentHealth = health; //Initalize player health
    }

    private void Update()
    {
        //movePlayer(); this in update and fixed update disables update and probably also fixed update
        //UpdateHealthUI();
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
        AlienModel.SetActive(false);
        RobotModel.SetActive(false);
        TankModel.SetActive(false);
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