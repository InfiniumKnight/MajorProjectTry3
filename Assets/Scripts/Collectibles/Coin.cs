using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public int rotateSpeed = 20;

    public PlayerController playerController;

    [SerializeField]
    private int speed = 5;

    [SerializeField]
    private float height = 1;

    [SerializeField]
    private float extraHeight = 2;

    public delegate void CoinPickedUp();
    public static event CoinPickedUp Coined;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Has the coin float up and down, as well as rotate
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        Vector3 pos = transform.position;
        float newY = Mathf.Sin(Time.time * speed);
        newY = newY + extraHeight;
        transform.position = new Vector3(pos.x, (newY * height), pos.z);
    }

    //Adds a coin to the players inventory when they come into contact, and destroys the coin
    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Coined.Invoke();
            Destroy(gameObject);
        }
    }

}
