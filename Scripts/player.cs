using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class player : MonoBehaviour
{
    public static player instance;
    public float playerSpeed=1000;
    public float directionalSpeed=20;

    public AudioClip scoreUp;
    public AudioClip damage;
    public AudioClip coinCollect;
    public GameObject SceneManager;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSpeed < 3000)
        {
            playerSpeed = playerSpeed + 0.5f;
        }

#if UNITY_EDITOR || UNITY_STANDALONE || UNITYWEBPLAYER
        float moveHorizontal = Input.GetAxis("Horizontal");
        transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(Mathf.Clamp(gameObject.transform.position.x + moveHorizontal, -2.5f, 2.5f), gameObject.transform.position.y, gameObject.transform.position.z), directionalSpeed * Time.deltaTime);
#endif
        GetComponent<Rigidbody>().velocity = Vector3.forward * playerSpeed * Time.deltaTime;
        transform.Rotate(Vector3.right * GetComponent<Rigidbody>().velocity.z / 3);

       
        // Mobile Control
        Vector2 touch = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));
        if (Input.touchCount > 0)
        {
            transform.position = new Vector3(touch.x, transform.position.y, transform.position.z);
        }
    }

  

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "scoreup")
        {
           
            GetComponent<AudioSource>().PlayOneShot(scoreUp, 0.6f);
            //if (SceneManager.GetComponent<AppInitialisation>().hasGameOver == true)      // after game over mute sound
            //{
            //    GetComponent<AudioSource>().Stop();
            //}
           }

        if (other.gameObject.tag == "coin")
        {
            GetComponent<AudioSource>().PlayOneShot(coinCollect, 5f);
        }

        if (other.gameObject.tag == "triangle")
        {
           
            GetComponent<AudioSource>().PlayOneShot(damage, 5f);
            AppInitialisation.instance.GameOver();
            
            //SceneManager.GetComponent<AppInitialisation>().GameOver();
            if (AppInitialisation.instance.hasGameOver == true)    // after game over mute sound
            {
                GetComponent<AudioSource>().Stop();
            }

        }

    }

   

}
