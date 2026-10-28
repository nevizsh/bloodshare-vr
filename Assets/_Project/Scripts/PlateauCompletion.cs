using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PlateauCompletion : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor[] sockets;
    [SerializeField] private UnityEvent onComplete;

    private bool done;

    private void OnEnable()
    {
        foreach (var s in sockets)
        {
            s.selectEntered.AddListener(_ => Check());
            s.selectExited.AddListener(_ => done = false);
        }
    }

    private void Check()
    {
        if (done) return;

        foreach (var s in sockets)
            if (!s.hasSelection) return;

        done = true;
        onComplete.Invoke();
    }
}