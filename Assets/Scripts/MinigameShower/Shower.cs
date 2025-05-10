using System;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[System.Serializable]
public struct IntPair
{
    public int begin;
    public int end;

    public IntPair(int begin, int end)
    {
        this.begin = begin;
        this.end = end;
    }
}

public class Shower : MonoBehaviour
{
    [SerializeField]
    private int totalValue = 100;
    [SerializeField]
    private float increment = 1;
    [SerializeField]
    public ProgressBar progressBar;

    public IntPair[] values = new IntPair[]
    {
        new IntPair(60, 80),
        new IntPair(20, 50),
        new IntPair(70, 90)
    };

    private int marckBegin = 60;
    private int marckEnd = 80;
    private int showerIndex = 0;
    private float currentValue = 0;
    private bool isKeyPressed = false;

    private void Start()
    {
        SetStatus();
        progressBar.SetFillValue(totalValue, currentValue);

    }

    private bool SetStatus()
    {
        if (values.Length != 0 && showerIndex < values.Length) 
        {
            marckBegin = values[showerIndex].begin;
            marckEnd = values[showerIndex].end;
            progressBar.SetMarck(showerIndex);
            showerIndex++;

            return true;
        }
        return false;
    }

    void Update()
    {
        if (isKeyPressed)
        {
            currentValue += increment;
            if (currentValue > totalValue) currentValue = 0;
            progressBar.SetFillValue(totalValue, currentValue);
        }
    }


    public void OnKeyPressed()
    {
        currentValue = 0;           
        isKeyPressed = true;  
    }

    public void OnKeyReleased()
    {
        // Aquí pones la acción que quieres hacer al soltar la tecla
        isKeyPressed = false;  // Detiene el contador

        if (isValid())
        {
            if (!SetStatus()) 
            {
                Debug.Log("FINNNNNNNNNNN");
            }
        }
        currentValue = 0;
        progressBar.SetFillValue(totalValue, currentValue);
    }

    private bool isValid() 
    {

        if (currentValue >= marckBegin && currentValue <= marckEnd) 
        {
            return true;
        }
        return false;
    }
}
