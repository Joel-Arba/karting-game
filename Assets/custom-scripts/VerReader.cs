using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VerReader : MonoBehaviour
{
    public TMP_Text versionText;

    // Start is called before the first frame update
    void Start()
    {
        versionText.text = "v" + Application.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
