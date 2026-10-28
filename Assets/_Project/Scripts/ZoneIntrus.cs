using UnityEngine;

public class ZoneIntrus : MonoBehaviour
{
    [SerializeField] private GameObject message;
    [SerializeField] private string tagIntrus = "Intrus";
    [SerializeField] private float duree = 2.5f;

    private float timer;

    private void Awake()
    {
        if (message != null) message.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(tagIntrus)) return;
        if (message != null) message.SetActive(true);
        timer = duree;
    }

    private void Update()
    {
        if (timer <= 0f) return;

        timer -= Time.deltaTime;
        if (timer <= 0f && message != null)
            message.SetActive(false);
    }
}