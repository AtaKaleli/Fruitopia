using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BackgroundColors { blue, brown, gray,green,pink,purple,yellow }

public class AnimatedBackground : MonoBehaviour
{

    private MeshRenderer mesh;
    [SerializeField] private Vector2 meshDirection;

    [Header("Background Color Information")]
    [SerializeField] BackgroundColors backgroundColors;
    [SerializeField] private Texture2D[] textures;


    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        UpdateTexture();

    }

    private void Update()
    {
        mesh.material.mainTextureOffset += meshDirection * Time.deltaTime;
    }


    [ContextMenu("UpdateTexture")]
    private void UpdateTexture()
    {
        if (mesh == null)
            mesh = GetComponent<MeshRenderer>();

        mesh.material.mainTexture = textures[(int)backgroundColors];


    }

}
