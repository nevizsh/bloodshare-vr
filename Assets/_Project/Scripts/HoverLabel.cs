using TMPro;
using UnityEngine;

public class HoverLabel : MonoBehaviour
{
    public static HoverLabel Instance;

    [SerializeField] private TMP_Text text;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0.12f, 0f);

    private Transform target;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + offset;

        if (Camera.main != null)
            transform.forward = transform.position - Camera.main.transform.position;
    }

    public void Show(Transform t, string label)
    {
        target = t;
        if (text != null) text.text = label;
        SetVisible(true);
    }

    public void Hide()
    {
        target = null;
        SetVisible(false);
    }

    private void SetVisible(bool visible)
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(visible);
    }
}