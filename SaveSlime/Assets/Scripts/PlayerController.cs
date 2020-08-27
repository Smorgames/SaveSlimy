using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float jumpHeight;
    public Animator animator;
    public Rigidbody2D rb;

    public Transform groundCheck;
    private bool onGround;

    private Vector2 jumpDirection = new Vector2(0, 1);

    private float gravityChangeRate = 5f;
    private float nextgravityChange = 0f;
    private float timerToNextGravityChange;

    public GravityBar gravityBar;

    public GameObject trailEffect, deadEffect;

    public GameObject gameOverPanel;

    public bool isPlayerDead;

    public Text scoreText, highScoreTextGO, scoreTextGO;
    private int score;

    public GameObject help;
    private bool helpSwitch;

    public AudioClip jumpAudio, deathAudio;
    public AudioSource playerAudioSource;

    private void Start()
    {
        scoreText.enabled = true;
        helpSwitch = true;
        help.SetActive(true);
        score = 0;
        scoreText.text = score.ToString();
        isPlayerDead = false;
        gravityBar.SetMaxMana(gravityChangeRate);
        timerToNextGravityChange = gravityChangeRate;
        trailEffect.SetActive(true);
    }

    private void Update()
    {
        timerToNextGravityChange += Time.deltaTime;
        gravityBar.SetMana(timerToNextGravityChange);
        IsLanding();
        if (Input.GetButtonDown("Jump"))
            playerAudioSource.PlayOneShot(jumpAudio);
        if (Input.GetButton("Jump") && onGround == true)
        {
            animator.SetBool("IsJumping", true);
            playerAudioSource.PlayOneShot(jumpAudio);
        }

        if (Time.time > nextgravityChange)
        {
            if (Input.GetButtonDown("Gravity"))
            {
                helpSwitch = false;
                if (helpSwitch == false)
                    help.SetActive(false);
                Physics2D.gravity = -Physics2D.gravity;
                timerToNextGravityChange = 0;
                nextgravityChange = Time.time + gravityChangeRate;
            }

        }

        GravityChange();
    }

    private void FixedUpdate()
    {
        IsLanding();
        if (Input.GetButton("Jump") && onGround == true)
        {
            rb.AddForce(jumpDirection * jumpHeight, ForceMode2D.Impulse);
        }
    }

    void IsLanding()
    {
        Collider2D[] ground = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        if (ground.Length > 1)
        {
            animator.SetBool("IsJumping", false);
            onGround = true;
        }
        else
        {
            animator.SetBool("IsJumping", true);
            onGround = false;
        }
    }

    void GravityChange()
    {
        if (Physics2D.gravity.y == -30)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            jumpDirection = Vector2.up;
        }
        if (Physics2D.gravity.y == 30)
        {
            transform.rotation = Quaternion.Euler(0, 180, 180);
            jumpDirection = Vector2.down;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(DeadFunc());
        }
    }

    IEnumerator DeadFunc()
    {
        scoreText.enabled = false;
        playerAudioSource.PlayOneShot(deathAudio);
        animator.SetBool("IsDead", true);
        isPlayerDead = true;
        trailEffect.SetActive(false);
        deadEffect.SetActive(true);
        enabled = false;
        yield return new WaitForSeconds(5);
        gameOverPanel.SetActive(true);
        if (score == 1)
            scoreTextGO.text = "You avoid " + score.ToString() + " spike";
        if (score != 1)
            scoreTextGO.text = "You avoid " + score.ToString() + " spikes";
        Highscore();
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
    }

    public void AddScore(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }

    private void Highscore()
    {
        if (PlayerPrefs.GetInt("Highscore", 0) < score)
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
        if (PlayerPrefs.GetInt("Highscore", 0) == 1)
            highScoreTextGO.text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0) + " spike";
        if (PlayerPrefs.GetInt("Highscore", 0) != 1)
            highScoreTextGO.text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0) + " spikes";
    }
}
