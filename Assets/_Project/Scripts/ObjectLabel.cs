using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ObjectLabel : MonoBehaviour
{
    [SerializeField, TextArea(2, 3)] private string label = "Objet";

    private XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
    }

    private void OnEnable()
    {
        if (interactable == null) return;
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);
    }

    private void OnDisable()
    {
        if (interactable == null) return;
        interactable.hoverEntered.RemoveListener(OnHoverEntered);
        interactable.hoverExited.RemoveListener(OnHoverExited);
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (HoverLabel.Instance != null)
            HoverLabel.Instance.Show(transform, label);
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        if (HoverLabel.Instance != null)
            HoverLabel.Instance.Hide();
    }
}