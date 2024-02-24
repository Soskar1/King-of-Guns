using System;
using System.Collections;
using UnityEngine;

namespace KingOfGuns.Core
{
    public class Timer : MonoBehaviour, IService
    {
        public Coroutine StartTimer(float time, Action action) => StartCoroutine(StartCountdown(time, action));

        public void StopTimer(Coroutine timer) => StopCoroutine(timer);
        
        private IEnumerator StartCountdown(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }
    }
}