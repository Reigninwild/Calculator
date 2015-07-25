using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

    public class Run : MonoBehaviour
    {
        private FirstPersonController fpc;
        public string Name;

        void OnEnable()
        {
            fpc = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<FirstPersonController>();
        }

        public void OnRun()
        {
            fpc.m_IsWalking = false;
        }

        public void OnWalk()
        {
            fpc.m_IsWalking = true;
        }

        public void Update()
        {

        }
    }