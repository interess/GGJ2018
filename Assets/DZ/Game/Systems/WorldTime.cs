using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace DZ.Game.Systems.WorldTime
{
    public class Chain : Entitas.Gentitas.Systems.ChainSystem
    {
        public Chain() : base("DZ.Game.WorldTime")
        {
            Add(new CreateWorldTime());
            Add(new TickWorldTime());
        }
    }

    public class CreateWorldTime : InitializeSystem
    {
        protected override void Act()
        {
            var worldTimeEntity = state.CreateEntity();
            worldTimeEntity.worldTime = 0f;
            worldTimeEntity.worldTimeSpeed = 3f;
        }
    }

    public class TickWorldTime : ExecuteSystem
    {
        protected override void Act()
        {
            state.worldTime += state.worldTimeEntity.worldTimeSpeed * Time.deltaTime;
        }
    }
}
