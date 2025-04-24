using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        Vector3 pos = transform.position;
        float newY = Mathf.Sin(Time.time * speed);
        newY = newY + extraHeight;
        transform.position = new Vector3(pos.x, (newY * height), pos.z);
    }


    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Destroy(gameObject);
        }
    }

    private void OnEnable ()
    {

    }

    private void OnDisable ()
    {

    }

}
