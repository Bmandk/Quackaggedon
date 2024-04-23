using System;
using UnityEngine;

namespace DuckClicker
{
    public class DuckFeeder : MonoBehaviour
    {
        public Animator arm;
        public ParticleSystem breadParticles;
        public int breadCount = 30;

        public void ToggleFeeding(bool isFeeding)
        {
            arm.SetBool("Throwing", isFeeding);
        }

        public void ThrowBread()
        {
            breadParticles.Emit(breadCount);
        }
    }
}