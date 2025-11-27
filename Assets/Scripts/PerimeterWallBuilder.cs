using UnityEngine;
using System.Collections.Generic;

public class PerimeterWallBuilder : MonoBehaviour
{
    [Header("Wall Settings")]
    public GameObject wallStraightPrefab;
    public GameObject wallCornerPrefab;
    public float wallSpacing = 1f;
    
    [Header("Perimeter Size")]
    public int areaWidth = 10;
    public int areaDepth = 8;
    
    private List<GameObject> walls = new List<GameObject>();

    void Start()
    {
        BuildPerimeterWalls();
    }

    public void BuildPerimeterWalls()
    {
        // Очищаем старые стены
        ClearWalls();
        
        // Строим стены по периметру
        BuildHorizontalWalls(); // Верхние и нижние стены
        BuildVerticalWalls();   // Левые и правые стены
        BuildCorners();         // Угловые элементы
    }

    void BuildHorizontalWalls()
    {
        // Нижняя стена (Z = 0)
        for (int x = 0; x < areaWidth; x++)
        {
            Vector3 position = new Vector3(x * wallSpacing, 0, 0);
            GameObject wall = Instantiate(wallStraightPrefab, position, Quaternion.identity, transform);
            walls.Add(wall);
        }
        
        // Верхняя стена (Z = areaDepth)
        for (int x = 0; x < areaWidth; x++)
        {
            Vector3 position = new Vector3(x * wallSpacing, 0, areaDepth * wallSpacing);
            GameObject wall = Instantiate(wallStraightPrefab, position, Quaternion.Euler(0, 180, 0), transform);
            walls.Add(wall);
        }
    }

    void BuildVerticalWalls()
    {
        // Левая стена (X = 0)
        for (int z = 1; z < areaDepth; z++)
        {
            Vector3 position = new Vector3(0, 0, z * wallSpacing);
            GameObject wall = Instantiate(wallStraightPrefab, position, Quaternion.Euler(0, 90, 0), transform);
            walls.Add(wall);
        }
        
        // Правая стена (X = areaWidth)
        for (int z = 1; z < areaDepth; z++)
        {
            Vector3 position = new Vector3(areaWidth * wallSpacing, 0, z * wallSpacing);
            GameObject wall = Instantiate(wallStraightPrefab, position, Quaternion.Euler(0, 270, 0), transform);
            walls.Add(wall);
        }
    }

    void BuildCorners()
    {
        // Четыре угла
        Vector3[] cornerPositions = {
            new Vector3(0, 0, 0),                                    // Левый нижний
            new Vector3(areaWidth * wallSpacing, 0, 0),              // Правый нижний
            new Vector3(0, 0, areaDepth * wallSpacing),              // Левый верхний
            new Vector3(areaWidth * wallSpacing, 0, areaDepth * wallSpacing) // Правый верхний
        };

        Quaternion[] cornerRotations = {
            Quaternion.Euler(0, 0, 0),      // Левый нижний
            Quaternion.Euler(0, 90, 0),     // Правый нижний  
            Quaternion.Euler(0, 270, 0),    // Левый верхний
            Quaternion.Euler(0, 180, 0)     // Правый верхний
        };

        for (int i = 0; i < cornerPositions.Length; i++)
        {
            GameObject corner = Instantiate(wallCornerPrefab, cornerPositions[i], cornerRotations[i], transform);
            walls.Add(corner);
        }
    }

    void ClearWalls()
    {
        foreach (GameObject wall in walls)
        {
            if (wall != null)
                DestroyImmediate(wall);
        }
        walls.Clear();
    }

    // Визуализация области в редакторе
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 center = new Vector3(areaWidth * wallSpacing * 0.5f, 0, areaDepth * wallSpacing * 0.5f);
        Vector3 size = new Vector3(areaWidth * wallSpacing, 0.1f, areaDepth * wallSpacing);
        Gizmos.DrawWireCube(center, size);
    }
}