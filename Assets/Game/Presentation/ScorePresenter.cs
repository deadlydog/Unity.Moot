using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScorePresenter : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI Scoretext;
    
    [Inject]
    
    
    // Start is called before the first frame update
    void Start()
    {
        Scoretext.text = "000";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
