using System;
using System.Collections;
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
    [SerializeField]
    private GameObject cover;

    public IntPair[] valuesMarck = new IntPair[]
    {
        new IntPair(60, 80),
        new IntPair(30, 50),
        new IntPair(50, 60)
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
        if (valuesMarck.Length != 0 && showerIndex < valuesMarck.Length) 
        {
            marckBegin = valuesMarck[showerIndex].begin;
            marckEnd = valuesMarck[showerIndex].end;
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
        isKeyPressed = false;

        if (isValid())
        {
            //WAIT
            StartCoroutine(Espera());
           
        }
        else 
        { 
            currentValue = 0;
            progressBar.SetFillValue(totalValue, currentValue);
        }
        
    }

    IEnumerator Espera()
    {
        progressBar.SetFillValue(totalValue, totalValue);

        //TODO SONIDITO 
        yield return new WaitForSeconds(1.0f);
        if (!SetStatus())
        {
            cover.SetActive(true);
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.GetComponent<PlayerController>().blockControls = false;
                player.GetComponent<Renderer>().enabled = true;
                player.GetComponent<PressKey>().enabled = false;
            }

            //TODO CAMBIO DE ESCENAAAA 
            Debug.Log("FINNNNNNNNNNN");
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
