using System;
using Assets.Game.Domain;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Game.Presentation
{
    public class EnemyDirector : MonoBehaviour
    {
       [Inject] 
       public IEnemyCommands EnemyCommands { private get; set; }
       
       [Inject]
       public IEnemyEvents EnemyEvents { private get; set; }

       private void Start()
       {
           Observable.NextFrame()
               .Subscribe(_ => SpawnAnEnemy())
               .AddTo(this);
       }

       public void SpawnAnEnemy()
           => EnemyCommands.SpawnEnemy(transform.position);
    }
    
}