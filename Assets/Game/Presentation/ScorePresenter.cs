using System.Collections;
using System.Collections.Generic;
using Assets.Game.Domain;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScorePresenter : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI Scoretext;
    
    [Inject]
    public IEnemyEvents EnemyEvents { private get; set; }

    private int _currentScore = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Scoretext.text = "000";
        EnemyEvents
            .OfType<EnemyEvent, EnemyEvent.EnemyKilled>()
            .Subscribe(_ => AddToScore())
            .AddTo(this);
    }

    void AddToScore()
    {
        _currentScore++;
        Scoretext.text = $"{_currentScore * 10}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
