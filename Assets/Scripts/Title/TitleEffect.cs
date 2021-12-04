using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEffect : MonoBehaviour
{
    public Material duckMaterial;
    public Material titleMaterial;
    public Material buttonMaterial;

    private float fade;
    // Start is called before the first frame update
    void Start()
    {
        fade = -1;
    }

    // Update is called once per frame
    void Update()
    {
        fade += Time.deltaTime * 0.5f;
        duckMaterial.SetFloat("_Fade", fade);
        titleMaterial.SetFloat("_Fade", fade - 0.4f);
        buttonMaterial.SetFloat("_Fade", fade - 0.8f);
    }
}
