using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float jump = 2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Animator inventoryAnimator;
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject pause;
    [SerializeField] private Text coinsAmount;
    [SerializeField] private GameObject coins;
    [SerializeField] private CanvasGroup coinsCanvasGroup;
    public int money = 0;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isCoroutineRunning;
    public static bool isInventoryOpen = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        if (PlayerPrefs.GetInt("Load") == 1) Load();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, ground);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        controller.Move(move * speed * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isInventoryOpen)
            {
                inventoryAnimator.SetBool("inventoryOpen", false);
                description.SetActive(false);
                if (!isCoroutineRunning) coins.SetActive(false);
                isInventoryOpen = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                inventoryAnimator.SetBool("inventoryOpen", true);
                StopCoroutine("ShowAndFadeRoutine");
                coinsCanvasGroup.alpha = 1;
                coins.SetActive(true);
                isInventoryOpen = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            pause.SetActive(!pause.activeSelf);
            if (!pause.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
            }
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Save()
    {
        SaveSystem.Save(this);
    }

    public void Load()
    {
        PlayerData data = SaveSystem.Load();

        money = data.coins;
        InventoryManager.instance.items = data.items;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("OneCoin"))
        {
            money++;
            coinsAmount.text = money.ToString();
            ResetStuff();
            StartCoroutine("ShowAndFadeRoutine");
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.CompareTag("Coins"))
        {
            ResetStuff();
            StartCoroutine(CoinsRoutine());
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator ShowAndFadeRoutine()
    {
        if (!inventoryAnimator.GetBool("inventoryOpen"))
        {
            yield return new WaitForSeconds(2f);
            float a = coinsCanvasGroup.alpha;
            while (a > 0)
            {
                a -= 0.05f;
                coinsCanvasGroup.alpha = a;
                yield return new WaitForSeconds(0.1f);
            }
            coins.SetActive(false);
        }
    }

    private IEnumerator CoinsRoutine()
    {
        isCoroutineRunning = true;
        for (int i = 0; i < 15; i++)
        {
            money++;
            coinsAmount.text = money.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine("ShowAndFadeRoutine");
        isCoroutineRunning = false;
    }

    private void ResetStuff()
    {
        StopCoroutine("ShowAndFadeRoutine");
        coinsCanvasGroup.alpha = 1;
        coins.SetActive(true);
    }
}
