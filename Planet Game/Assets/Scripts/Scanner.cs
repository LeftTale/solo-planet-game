using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private TextDirector textDirector;
    private TextWriter textWriter;
    private TextMeshProUGUI scannerText;
    private Animator animator;
    private bool wipeText = true;
    private AudioSource audioSource;

    private void Start()
    {
        textDirector = GameObject.Find("AveryUI").transform.Find("TextDirector").gameObject
            .GetComponent<TextDirector>();

        textWriter = GameObject.Find("AveryUI").transform.Find("TextDirector").gameObject
            .GetComponent<TextWriter>();

        scannerText = GameObject.Find("AveryUI").transform.Find("ScannerImage").transform.Find("ScannerText").gameObject.GetComponent<TextMeshProUGUI>();
        audioSource = GameObject.Find("AveryUI").transform.Find("ScannerImage").transform.Find("ScannerText").gameObject.GetComponent<AudioSource>();
        
        animator = GameObject.Find("ScannerImage").GetComponent<Animator>();
    }

    private void Update()
    {
        if(wipeText)
            scannerText.text = null;

        if (textWriter.UIText == null)
            audioSource.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Planet"))
        {
            textDirector.SendScannerMessage(0);
            textDirector.SendStatusMessage(0);
        }
        else if (other.gameObject.CompareTag("Fire Planet"))
        {
            textDirector.SendScannerMessage(1);
            textDirector.SendStatusMessage(1);
        }
        else if (other.gameObject.CompareTag("RadioActive Planet"))
        {
            textDirector.SendScannerMessage(2);
            textDirector.SendStatusMessage(2);
        }
        else if (other.gameObject.CompareTag("Flooded Planet"))
        {
            textDirector.SendScannerMessage(3);
            textDirector.SendStatusMessage(3);
        }
        else if (other.gameObject.CompareTag("Moon"))
        {
            textDirector.SendScannerMessage(4);
            textDirector.SendStatusMessage(4);
        }


        //11 is the gravity field layer
        if (other.gameObject.layer == 11)
        {
            animator.SetBool("Darken", true);
            animator.SetBool("Undarken", false);
            wipeText = false;
            audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Gravity layer
        if (other.gameObject.layer == 11)
        {
            animator.SetBool("Darken",false);
            animator.SetBool("Undarken",true);
            wipeText = true;
            audioSource.Stop();
        }
    }
}

