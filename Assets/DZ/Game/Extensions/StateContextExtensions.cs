using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game
{
    public static class StateContextExtensions
    {
        public static StateEntity CreateConfigEntity(this StateContext context, string id)
        {
            var entity = context.CreateEntity();
            entity.configId = id;
            return entity;
        }
        
        public static StateEntity CreateLevelPartEntity(this StateContext context)
        {
            var entity = context.CreateEntity();
            entity.levelPart = true;
            return entity;
        }

        public static StateEntity CreateViewEntity(this StateContext context)
        {
            var entity = context.CreateEntity();
            entity.view = true;
            return entity;
        }

        public static StateEntity CreateEffectEntity(this StateContext context, string id)
        {
            var entity = context.CreateEntity();
            entity.effect = true;
            entity.effectId = id;
            return entity;
        }
    }
}
