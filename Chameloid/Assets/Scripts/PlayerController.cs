using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;
using System;


public class PlayerController : MonoBehaviour
{
    //Serialize keeps property private (cannot be changed from other scripts) but allows unity accesses it
    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 10.0f;

    [SerializeField] private Canvas winCanvas;
    private BoxCollider2D boxCollider2d;


    private int lives = 3;

    private bool red = true;
    private bool blue;
    private bool yellow;
    private bool nothing;

    public string currentColour;



    private Rigidbody2D rBody;
    private SpriteRenderer m_SpriteRenderer;
    private Vector3 startPos;
    private Image m_Image;

    private string sceneName;

    private int currentScene;

    public TextMeshProUGUI currentTimeText;


    private void Awake() {
        rBody = GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        winCanvas.gameObject.SetActive(false);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        sceneName = SceneManager.GetActiveScene().name;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        startPos = transform.position; //set respawn position
    }

    void playerColor(){
        if(red == true){
            m_SpriteRenderer.color = Color.red;
            
            
        }
        if(blue == true){
            m_SpriteRenderer.color = Color.blue;
            
        }
        if(yellow == true){
            m_SpriteRenderer.color = Color.yellow;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeText.text = TimerScript.time.ToString(@"mm\:ss\:fff");
        float horiz = Input.GetAxis("Horizontal");
        //change horiz axis by speed
        rBody.velocity = new Vector2(horiz * speed, rBody.velocity.y);

        playerColor();
        if(GroundCheck() && Input.GetKeyDown(KeyCode.Space)){
            rBody.velocity = Vector2.up * jumpForce;
        }

        /*ignore other colours collisions*/
        GameObject[] blueObjects = GameObject.FindGameObjectsWithTag("blue");
        GameObject[] redObjects = GameObject.FindGameObjectsWithTag("red");
        GameObject[] yellowObjects = GameObject.FindGameObjectsWithTag("yellow");

        GameObject[] redDeathObjects = GameObject.FindGameObjectsWithTag("redDeath");
        GameObject[] blueDeathObjects = GameObject.FindGameObjectsWithTag("blueDeath");
        GameObject[] yellowDeathObjects = GameObject.FindGameObjectsWithTag("yellowDeath");
        
        //if not red, ignore collision

        if(red == false){
            foreach (GameObject obj in redObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true); 
            }

            foreach (GameObject obj in redDeathObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true); 
            }

        }
        else{
            foreach (GameObject obj in redObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false); 
            }

            foreach (GameObject obj in redDeathObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false); 
            }
        }

        if(blue == false){
            foreach (GameObject obj in blueObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true); 
            }

            foreach (GameObject obj in blueDeathObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true); 
            }
        }
        else{
            foreach (GameObject obj in blueObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false); 
            }
            foreach (GameObject obj in blueDeathObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false); 
            }
        }

        if(yellow == false){
            foreach (GameObject obj in yellowObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true); 
            }
            foreach (GameObject obj in yellowDeathObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true); 
            }
        }
        else{
            foreach (GameObject obj in yellowObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false); 
            }
            foreach (GameObject obj in yellowDeathObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false); 
            }
        }
    }
    void ChangeRed(){
        red = true;
        blue = false;
        yellow = false;
        currentColour = "red";
    }

    void ChangeBlue(){
        red = false;
        blue = true;
        yellow = false;
        currentColour = "blue";
    }

    void ChangeYellow(){
        red = false;
        blue = false;
        yellow = true;
        currentColour = "yellow";
    }
    //For Physics
    void Lives(){
        if(lives == 3){
            
        }
        if(lives == 2){
            
        }
        if(lives == 1){
            
        }
    }

    //Player Ground Check
    private bool GroundCheck(){
        float extraHeight = 0.05f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + extraHeight, platformsLayerMask);
        Color rayColor;
        if(raycastHit.collider != null){
            rayColor = Color.green;
        }
        else{
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y + extraHeight), rayColor);
        return raycastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "green"){
            Debug.Log("Yon Won!!!");
            winCanvas.gameObject.SetActive(true);
            SceneManager.LoadScene(currentScene + 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.tag == "changeRed")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            ChangeRed();
            playerColor();
            Debug.Log("colour is now red");
            
        }

        if (other.gameObject.tag == "changeBlue")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            ChangeBlue();
            playerColor();
            Debug.Log("colour is now blue");
        }

        if (other.gameObject.tag == "changeYellow")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            ChangeYellow();
            playerColor();
            Debug.Log("colour is now yellow");
        }

        if (red == true && other.gameObject.tag == "redDeath" )
        {
            //If the GameObject has the same tag as specified, output this message in the console
            SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
            Debug.Log("you died!");
            lives -= 1;
            Lives();
        }

        if (blue == true && other.gameObject.tag == "blueDeath")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
            Debug.Log("you died!");
            lives -= 1;
            Lives();
        }

        if (yellow == true && other.gameObject.tag == "yellowDeath")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
            Debug.Log("you died!");
            lives -= 1;
            Lives();
        }
        
    }

}
