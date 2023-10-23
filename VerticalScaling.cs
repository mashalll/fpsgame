using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PillarScaler : MonoBehaviour
{
    public float maxHeight = 10f;
    public float scaleDownTo = 0.01f;
    public float animationDuration = 0.5f;

    private Vector3 originalScale;
    private GameObject[] allPillars;

    void Start()
    {
        originalScale = transform.localScale;
        allPillars = GameObject.FindGameObjectsWithTag("Pillar");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ScalePillarByName("Pillar1");
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ScalePillarByName("Pillar2");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ScalePillarByName("Pillar3");
        }
    }

    void ScalePillarByName(string pillarName)
    {
        foreach (GameObject pillar in allPillars)
        {
            if (pillar.name == pillarName)
            {
                StartCoroutine(ScaleToHeight(pillar, maxHeight));
            }
            else
            {
                StartCoroutine(ScaleToHeight(pillar, scaleDownTo));
            }
        }
    }

    void ScaleAllPillars()
    {
        foreach (GameObject pillar in allPillars)
        {
            StartCoroutine(ScaleToHeight(pillar, maxHeight));
        }
    }

    IEnumerator ScaleToHeight(GameObject pillar, float targetHeight)
    {
        float startTime = Time.time;
        Vector3 startScale = pillar.transform.localScale;

        while (Time.time < startTime + animationDuration)
        {
            float progress = (Time.time - startTime) / animationDuration;
            float scaledHeight = Mathf.Lerp(startScale.y, targetHeight, progress);
            pillar.transform.localScale = new Vector3(startScale.x, scaledHeight, startScale.z);
            yield return null;
        }

        pillar.transform.localScale = new Vector3(startScale.x, targetHeight, startScale.z);
    }
}
