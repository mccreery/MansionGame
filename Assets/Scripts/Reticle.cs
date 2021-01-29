﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Reticle : MonoBehaviour
{
    public PlayerData playerData;

    [Range(0, 1)]
    public float maxRadius;

    [Min(0)]
    public float smoothTime;

    private float radius;
    private float radiusVelocity;

    private Image image;
    private bool interested;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        interested = Physics.Raycast(ray, out RaycastHit hitInfo, playerData.reach)
            && hitInfo.transform.gameObject.CompareTag("Interactable");

        radius = Mathf.SmoothDamp(radius, interested ? maxRadius : 0, ref radiusVelocity, smoothTime);
        image.material.SetFloat("Radius", radius);

        if (Input.GetButtonDown("Fire1") && interested)
        {
            hitInfo.transform.GetComponent<IInteractable>().Interact();
        }
    }
}
