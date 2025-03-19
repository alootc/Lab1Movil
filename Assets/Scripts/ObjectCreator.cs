using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCreator : MonoBehaviour
{
    public GameObject[] shapePrefabs; // Prefabs de las formas 3D
    public Color[] colors; // Colores disponibles

    private int selectedShapeIndex = -1; // �ndice de la forma seleccionada
    private int selectedColorIndex = -1; // �ndice del color seleccionado

    void Update()
    {
        // Detectar toques en la pantalla
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Solo se instancia si hay un toque nuevo (TouchPhase.Began)
            if (touch.phase == TouchPhase.Began)
            {
                // Verificar que tanto la forma como el color est�n seleccionados
                if (selectedShapeIndex != -1 && selectedColorIndex != -1)
                {
                    CreateShapeAtPosition(touch.position);
                }
                else
                {
                    Debug.Log("Selecciona una forma y un color antes de tocar la pantalla.");
                }
            }
        }
    }

    // M�todo para seleccionar un color
    public void SelectColor(int colorIndex)
    {
        if (colorIndex >= 0 && colorIndex < colors.Length)
        {
            selectedColorIndex = colorIndex;
            Debug.Log("Color seleccionado: " + colors[colorIndex].ToString());
        }
    }

    // M�todo para seleccionar una forma
    public void SelectShape(int shapeIndex)
    {
        if (shapeIndex >= 0 && shapeIndex < shapePrefabs.Length)
        {
            selectedShapeIndex = shapeIndex;
            Debug.Log("Forma seleccionada: " + shapePrefabs[shapeIndex].name);
        }
    }

    // M�todo para crear la forma en la posici�n del toque
    void CreateShapeAtPosition(Vector2 touchPosition)
    {
        // Convertir la posici�n del toque en una posici�n en el mundo 3D
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f); // Dibuja el rayo en la escena

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Instanciar la forma seleccionada en la posici�n del toque
            GameObject newShape = Instantiate(shapePrefabs[selectedShapeIndex], hit.point, Quaternion.identity);

            // Cambiar el color de la forma
            Renderer shapeRenderer = newShape.GetComponent<Renderer>();
            if (shapeRenderer != null)
            {
                shapeRenderer.material.color = colors[selectedColorIndex];
                Debug.Log("Forma creada con color: " + colors[selectedColorIndex].ToString());
            }
            else
            {
                Debug.LogWarning("El prefab de la forma no tiene un componente Renderer.");
            }
        }
        else
        {
            Debug.LogWarning("No se detect� una superficie v�lida para instanciar la forma. Aseg�rate de que haya un objeto con Collider en la escena.");
        }
    }
}
