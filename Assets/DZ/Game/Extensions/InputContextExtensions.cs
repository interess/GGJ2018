using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game
{
    public static class InputContextExtensions
    {
        public static InputEntity CreateEventEntity(this InputContext context)
        {
            var entity = context.CreateEntity();
            entity.flagEvent = true;
            return entity;
        }

        public static InputEntity CreateEventEntity(this InputContext context, string eventId)
        {
            var entity = context.CreateEventEntity();
            entity.eventId = eventId;
            return entity;
        }
    }
}
