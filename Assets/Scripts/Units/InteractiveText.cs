using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveText : MonoBehaviour
{
    public float hoverScale = 1.25f;
    public float scaleSpeed = 10f;

    [Header("Idle motion")]
    public float floatAmplitude = 4f;
    public float floatFrequency = 2f;

    TextMeshProUGUI text;
    Camera uiCamera;

    float[] currentScales;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        uiCamera = null; // Screen Space - Overlay
    }

    void Start()
    {
        text.ForceMeshUpdate();
        currentScales = new float[text.textInfo.characterCount];

        for (int i = 0; i < currentScales.Length; i++)
            currentScales[i] = 1f;
    }

    void Update()
    {
        text.ForceMeshUpdate();

        int hoverChar = TMP_TextUtilities.FindIntersectingCharacter(
            text,
            Input.mousePosition,
            uiCamera,
            true
        );

        Debug.Log("Mouse input position: " + Input.mousePosition);

        TMP_TextInfo textInfo = text.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible)
                continue;

            float targetScale = (i == hoverChar) ? hoverScale : 1f;

            currentScales[i] = Mathf.Lerp(
                currentScales[i],
                targetScale,
                Time.deltaTime * scaleSpeed
            );

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

            Vector3 center =
                (vertices[vertexIndex + 0] +
                 vertices[vertexIndex + 2]) * 0.5f;

            float idleOffset =
                Mathf.Sin(Time.time * floatFrequency + i * 0.3f)
                * floatAmplitude;

            Vector3 idle = new Vector3(0, idleOffset, 0);

            for (int j = 0; j < 4; j++)
            {
                int idx = vertexIndex + j;

                Vector3 original = vertices[idx];

                Vector3 dir = original - center;
                Vector3 scaled = center + dir * currentScales[i];

                vertices[idx] = scaled + idle;
            }
        }

        text.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
    }
}


