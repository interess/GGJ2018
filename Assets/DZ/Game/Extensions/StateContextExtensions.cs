using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game
{
    public static class StateContextExtensions
    {
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
    }
}
