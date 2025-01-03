using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrosswordGrid : MonoBehaviour
{
    public int gridSize = 10; // Grid boyutu
    public List<string> words; // Kelime listesi
    public GameObject cellPrefab; // Hücre için prefab
    public Transform gridParent; // Grid'in UI üzerindeki parent objesi

    private char[,] grid; // Grid verisi

    void Start()
    {
        InitializeGrid();
        PlaceWords();
        GenerateVisualGrid();
        PrintGrid();
    }

    void InitializeGrid()
    {
        grid = new char[gridSize, gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                grid[i, j] = '.'; // Boş hücreler
            }
        }
    }

    void PlaceWords()
    {
        foreach (var word in words)
        {
            bool placed = false;
            for (int x = 0; x < gridSize && !placed; x++)
            {
                for (int y = 0; y < gridSize && !placed; y++)
                {
                    if (CanPlaceWord(word, x, y, true)) // Yatay yerleştirme
                    {
                        PlaceWord(word, x, y, true);
                        placed = true;
                    }
                    else if (CanPlaceWord(word, x, y, false)) // Dikey yerleştirme
                    {
                        PlaceWord(word, x, y, false);
                        placed = true;
                    }
                }
            }
        }
    }

    bool CanPlaceWord(string word, int startX, int startY, bool horizontal)
    {
        if (horizontal && startY + word.Length > gridSize) return false;
        if (!horizontal && startX + word.Length > gridSize) return false;

        for (int i = 0; i < word.Length; i++)
        {
            int x = horizontal ? startX : startX + i;
            int y = horizontal ? startY + i : startY;

            if (grid[x, y] != '.' && grid[x, y] != word[i])
            {
                return false; // Çakışma veya uyuşmazlık var
            }
        }
        return true;
    }

    void PlaceWord(string word, int startX, int startY, bool horizontal)
    {
        for (int i = 0; i < word.Length; i++)
        {
            int x = horizontal ? startX : startX + i;
            int y = horizontal ? startY + i : startY;
            grid[x, y] = word[i];
        }
    }

    void GenerateVisualGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject cell = Instantiate(cellPrefab, gridParent);
                cell.GetComponentInChildren<TextMeshProUGUI>().text = grid[x, y] == '.' ? "" : grid[x, y].ToString();
            }
        }
    }

    void PrintGrid()
    {
        string gridOutput = "";
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                gridOutput += grid[i, j] + " ";
            }
            gridOutput += "\n";
        }
        Debug.Log(gridOutput);
    }
}
