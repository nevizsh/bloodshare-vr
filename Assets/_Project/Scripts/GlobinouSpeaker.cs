using System.Collections;
using TMPro;
using UnityEngine;

public class GlobinouSpeaker : MonoBehaviour
{
    [Header("Texte")]
    [SerializeField] private TMP_Text texte;
    [SerializeField] private float vitesse = 0.035f;

    [Header("Voix")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip blip;
    [SerializeField] private int caracteresParBlip = 3;
    [SerializeField] private float variationHauteur = 0.15f;
    [SerializeField] private float volumeVoix = 0.5f;

    [Header("Globinou")]
    [SerializeField] private Transform globinou;
    [SerializeField] private float amplitudeParole = 0.03f;
    [SerializeField] private float vitesseParole = 14f;
    [SerializeField] private float amplitudeRepos = 0.012f;
    [SerializeField] private float vitesseRepos = 1.6f;

    private Vector3 basePos;
    private bool parle;

    private void Awake()
    {
        if (texte == null) texte = GetComponent<TMP_Text>();
        if (globinou != null) basePos = globinou.localPosition;
    }

    private void OnEnable()
    {
        StartCoroutine(Ecrire());
    }

    private void OnDisable()
    {
        parle = false;
        if (globinou != null) globinou.localPosition = basePos;
    }

    private void Update()
    {
        if (globinou == null) return;

        float a = parle ? amplitudeParole : amplitudeRepos;
        float v = parle ? vitesseParole : vitesseRepos;

        globinou.localPosition = basePos + Vector3.up * (Mathf.Sin(Time.time * v) * a);
    }

    private IEnumerator Ecrire()
    {
        if (texte == null) yield break;

        texte.ForceMeshUpdate();
        int total = texte.textInfo.characterCount;

        texte.maxVisibleCharacters = 0;
        parle = true;

        for (int i = 0; i <= total; i++)
        {
            texte.maxVisibleCharacters = i;

            if (i > 0 && blip != null && source != null && i % caracteresParBlip == 0)
            {
                char c = texte.textInfo.characterInfo[i - 1].character;
                if (!char.IsWhiteSpace(c))
                {
                    source.pitch = 1f + Random.Range(-variationHauteur, variationHauteur);
                    source.PlayOneShot(blip, volumeVoix);
                }
            }

            yield return new WaitForSeconds(vitesse);
        }

        parle = false;
    }
}