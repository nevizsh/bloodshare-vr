using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private Renderer voile;
    [SerializeField] private string sceneSuivante;
    [SerializeField] private float attenteAvantFondu = 7f;
    [SerializeField] private float dureeFondu = 1.2f;

    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    private Material mat;

    private void Awake()
    {
        if (voile != null) mat = voile.material;
        StartCoroutine(Fondu(1f, 0f, null));
    }

    public void AllerSceneSuivante()
    {
        if (string.IsNullOrEmpty(sceneSuivante)) return;
        StopAllCoroutines();
        StartCoroutine(AttendrePuisPartir());
    }

    private IEnumerator AttendrePuisPartir()
    {
        yield return new WaitForSeconds(attenteAvantFondu);
        yield return Fondu(0f, 1f, sceneSuivante);
    }

    private IEnumerator Fondu(float de, float vers, string scene)
    {
        if (mat == null) yield break;

        float t = 0f;
        SetAlpha(de);

        while (t < dureeFondu)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(de, vers, t / dureeFondu));
            yield return null;
        }

        SetAlpha(vers);

        if (!string.IsNullOrEmpty(scene))
            SceneManager.LoadScene(scene);
    }

    private void SetAlpha(float a)
    {
        Color c = mat.GetColor(BaseColor);
        c.a = a;
        mat.SetColor(BaseColor, c);
    }
}