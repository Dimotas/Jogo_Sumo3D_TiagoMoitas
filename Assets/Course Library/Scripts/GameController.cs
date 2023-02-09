using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private Rigidbody rb;
    private float Speed;
    private GameObject focalPoint;
    private bool poder;
    private float superPoder = 50f;
    public GameObject indicadorPoder;
    public GameObject informacaoPausa;
    public GameObject pausa;
    public bool ondaShock;
    public TextMeshProUGUI textMeshProUGUI;
    
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Speed = 2f;
        focalPoint = GameObject.Find("FocalPoint");
        poder = false;
        indicadorPoder.SetActive(false);
        pausa.SetActive(false);
        StartCoroutine(OndaShock());

    }

    // Update is called once per frame 
    void Update()
    {
        float vInput = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.transform.forward * vInput * Speed);
        if (transform.position.y < -10f) Destroy(gameObject);
        indicadorPoder.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);


        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1) {

                Time.timeScale = 0;
                pausa.SetActive(true);
                informacaoPausa.SetActive(false);

            }


            else
            {
                Time.timeScale = 1;
                pausa.SetActive(false);
                informacaoPausa.SetActive(true) ;
            }
                
        }


        if (Input.GetKeyDown(KeyCode.H))
        {

            if (ondaShock) { 
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (var enemy in enemies)
                {
                    var dir = (enemy.transform.position - transform.position).normalized;
                    enemy.GetComponent<Rigidbody>().AddForce(dir * 100, ForceMode.Impulse);
                }


                ondaShock= false;
                StartCoroutine(OndaShock());
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
            poder= true;
            indicadorPoder.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(ContarPower());
        }
    }

    IEnumerator ContarPower()
    {
        
        yield return new WaitForSeconds(15f);
        poder= false;
        indicadorPoder.SetActive(false) ;
    }

    IEnumerator OndaShock()
    {
        int timeleft = 60;

        while (timeleft > 0)
        {
            textMeshProUGUI.text = timeleft.ToString();

            yield return new WaitForSeconds(1.0f);

            timeleft--;
        }
        ondaShock = true;
        textMeshProUGUI.text = "H";
    }


   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && poder)
        {
            Rigidbody rbEnemy = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = collision.gameObject.transform.position- transform.position;
            rbEnemy.AddForce(awayFromEnemy*superPoder, ForceMode.Impulse);
        }
    }
}
