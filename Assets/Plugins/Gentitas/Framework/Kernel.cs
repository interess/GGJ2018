using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Entitas.Gentitas
{
    public abstract class Kernel : MonoBehaviour
    {
        public KernelInitializeType kernelInitializeType;
        public KernelExecuteType kernelExecuteType;
        public float intervalSeconds;
        public int intervalFrames;
        [Tooltip("Systems will execute when both intervals are fulfilled")]
        public bool waitForBothIntervals;
        protected Systems.ChainSystem rootChainSystem;

        float timerSeconds;
        int timerFrames;

        public Systems.ChainSystem GetChainForTest()
        {
            rootChainSystem = new Systems.ChainSystem(gameObject.name + ".VisualDebugging.Test");
            Setup();

            return rootChainSystem;
        }

        protected abstract void Setup();

        protected void Add(Systems.ChainSystem system)
        {
            rootChainSystem.Add(system);
        }

        protected void Add(Entitas.ISystem system)
        {
            rootChainSystem.Add(system);
        }

        void Initialize()
        {
            var rootChainName = gameObject.name + ".VisualDebugging";
            rootChainSystem = new Systems.ChainSystem(rootChainName);
            Setup();

#if UNITY_EDITOR
            rootChainSystem.gameObject.transform.SetParent(transform);
#endif

            rootChainSystem.Initialize();

            timerFrames = intervalFrames;
            timerSeconds = intervalSeconds;
        }

        void Execute()
        {
            rootChainSystem.Execute();
        }

        void Awake()
        {
            if (kernelInitializeType == KernelInitializeType.Awake)
            {
                Initialize();
            }
        }

        void Start()
        {
            if (kernelInitializeType == KernelInitializeType.Start)
            {
                Initialize();
            }
        }

        void Update()
        {
            if (kernelExecuteType == KernelExecuteType.Update)
            {
                if (intervalFrames == 0 && intervalSeconds == 0) Execute();
                else
                {
                    timerSeconds -= Time.deltaTime;
                    timerFrames -= 1;

                    if (waitForBothIntervals && timerSeconds <= 0 && timerFrames <= 0 ||
                        !waitForBothIntervals && (intervalSeconds > 0 && timerSeconds <= 0 || intervalFrames > 0 && timerFrames <= 0))
                    {
                        Execute();
                        timerSeconds = intervalSeconds;
                        timerFrames = intervalFrames;
                    }
                }
            }
        }

        void LateUpdate()
        {
            if (kernelExecuteType == KernelExecuteType.LateUpdate)
            {
                if (intervalFrames == 0 && intervalSeconds == 0) Execute();
                else
                {
                    timerSeconds -= Time.deltaTime;
                    timerFrames -= 1;

                    if (waitForBothIntervals && timerSeconds <= 0 && timerFrames <= 0 ||
                        !waitForBothIntervals && (intervalSeconds > 0 && timerSeconds <= 0 || intervalFrames > 0 && timerFrames <= 0))
                    {
                        Execute();
                        timerSeconds = intervalSeconds;
                        timerFrames = intervalFrames;
                    }
                }
            }
        }

        void FixedUpdate()
        {
            if (kernelExecuteType == KernelExecuteType.FixedUpdate)
            {
                if (intervalFrames == 0 && intervalSeconds == 0) Execute();
                else
                {
                    timerSeconds -= Time.deltaTime;
                    timerFrames -= 1;

                    if (waitForBothIntervals && timerSeconds <= 0 && timerFrames <= 0 ||
                        !waitForBothIntervals && (intervalSeconds > 0 && timerSeconds <= 0 || intervalFrames > 0 && timerFrames <= 0))
                    {
                        Execute();
                        timerSeconds = intervalSeconds;
                        timerFrames = intervalFrames;
                    }
                }
            }
        }
    }

    public enum KernelInitializeType
    {
        Awake,
        Start,
        Enable
    }

    public enum KernelExecuteType
    {
        Update,
        LateUpdate,
        FixedUpdate
    }
}
