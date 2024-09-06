using System.Collections;
using UnityEngine;

public class playerController : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController controller;
    [SerializeField] LayerMask ignoreMask;

    [SerializeField] int HP;

    [SerializeField] int speed;
    [SerializeField] int sprintMod;

    [SerializeField] int jumpMax;
    [SerializeField] int jumpSpeed;
    [SerializeField] int gravity;

    [SerializeField] int shootDamage;
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;

    Vector3 move;
    Vector3 playerVel;

    int jumpCount;
    int HPOrig;

    bool isSprinting;
    bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        HPOrig = HP;

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        HP = HPOrig;
        updatePLayerUI();

        controller.enabled = false;
        transform.position = GameManager.Instance.playerSpawnPos.transform.position;
        controller.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootDist, Color.blue);

        if (!GameManager.Instance.isPaused)
        {
            Movement();

        }
        Sprint();
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            jumpCount = 0;
            playerVel = Vector3.zero;
        }

        //move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //transform.position += move * speed * Time.deltaTime;

        move = Input.GetAxis("Vertical") * transform.forward +
            Input.GetAxis("Horizontal") * transform.right;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            jumpCount++;
            playerVel.y = jumpSpeed;
        }

        controller.Move(playerVel * Time.deltaTime);

        playerVel.y -= gravity * Time.deltaTime;

        if (Input.GetButton("Shoot") && !isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            speed *= sprintMod;
            isSprinting = true;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            speed /= sprintMod;
            isSprinting = false;
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootDist, ~ignoreMask))
        {
            // Debug.Log(hit.collider.name);
            IDamage dmg = hit.collider.GetComponent<IDamage>();

            if (dmg != null)
            {
                dmg.takeDamage(shootDamage);
            }
        }

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    public void takeDamage(int amount)
    {
        HP -= amount;

        updatePLayerUI();

        StartCoroutine(FlashDamage());

        if (HP <= 0)
        {
            GameManager.Instance.YouLose();
        }
    }

    IEnumerator FlashDamage()
    {
        GameManager.Instance.flashDamageScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.flashDamageScreen.SetActive(false);
    }

    public void updatePLayerUI()
    {
        GameManager.Instance.playerHPBar.fillAmount = (float)HP / HPOrig;
    }
}
