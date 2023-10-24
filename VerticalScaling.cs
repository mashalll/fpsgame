using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PillarScaler : MonoBehaviour
{
    public float scaleDownTo = 0.01f;
    private const float animationDuration = 2.0f; // Constant animation duration in seconds

    private Vector3 originalScale;

    // Remove the public maxHeight property

    void Start()
    {
        originalScale = transform.localScale;
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
        GameObject[] allPillars = GameObject.FindGameObjectsWithTag("Pillar");

        foreach (GameObject pillar in allPillars)
        {
            if (pillar.name == pillarName)
            {
                // Read the individual maxHeight from the GameObject's Inspector
                float maxHeight = pillar.GetComponent<PillarHeight>().maxHeight;
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
        GameObject[] allPillars = GameObject.FindGameObjectsWithTag("Pillar");

        foreach (GameObject pillar in allPillars)
        {
            // Read the individual maxHeight from the GameObject's Inspector
            float maxHeight = pillar.GetComponent<PillarHeight>().maxHeight;
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
