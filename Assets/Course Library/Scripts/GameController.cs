using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private Rigidbody rb;
    private float Speed;
    private GameObject focalPoint;
    private bool hasPower;
    private float powerForce = 50f;
    public GameObject powerIndicator;
    public GameObject pausa;
    public GameObject pausaInfo;
    public bool shockWave;
    public TextMeshProUGUI textMeshProUGUI;
    
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Speed = 2f;
        focalPoint = GameObject.Find("FocalPoint");
        hasPower = false;
        powerIndicator.SetActive(false);
        pausa.SetActive(false);
        StartCoroutine(ShockWave());

    }

    // Update is called once per frame 
    void Update()
    {
        float vInput = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.transform.forward * vInput * Speed);
        if (transform.position.y < -10f) Destroy(gameObject);
        powerIndicator.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);


        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1) {

                Time.timeScale = 0;
                pausa.SetActive(true);
                pausaInfo.SetActive(false);

            }


            else
            {
                Time.timeScale = 1;
                pausa.SetActive(false);
                pausaInfo.SetActive(true) ;
            }
                
        }


        if (Input.GetKeyDown(KeyCode.H))
        {

            if (shockWave) { 
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (var enemy in enemies)
                {
                    var dir = (enemy.transform.position - transform.position).normalized;
                    enemy.GetComponent<Rigidbody>().AddForce(dir * 100, ForceMode.Impulse);
                }


                shockWave= false;
                StartCoroutine(ShockWave());
            }
        }


        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector3.zero;
        }


    }

    
            
     

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            hasPower= true;
            powerIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(ContarPower());
        }
    }

    IEnumerator ContarPower()
    {
        
        yield return new WaitForSeconds(15f);
        hasPower= false;
        powerIndicator.SetActive(false) ;
    }

    IEnumerator ShockWave()
    {
        int countdown = 60;

        while (countdown > 0)
        {
            textMeshProUGUI.text = countdown.ToString();

            yield return new WaitForSeconds(1.0f);

            countdown--;
        }
        shockWave = true;
        textMeshProUGUI.text = "H";
    }


   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPower)
        {
            Rigidbody rbEnemy = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = collision.gameObject.transform.position- transform.position;
            rbEnemy.AddForce(awayFromEnemy*powerForce, ForceMode.Impulse);
        }
    }
}
