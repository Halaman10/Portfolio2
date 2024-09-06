using System.Collections;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Renderer model;

    Color colorOrg;

    private void Start()
    {
        colorOrg = model.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.playerSpawnPos.transform.position != transform.position)
        {
            GameManager.Instance.playerSpawnPos.transform.position = transform.position;
            StartCoroutine(FlashModel());
        }
    }

    IEnumerator FlashModel()
    {
        GameManager.Instance.checkpointMenu.SetActive(true);
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        GameManager.Instance.checkpointMenu.SetActive(false);
        model.material.color = colorOrg;
    }
}
