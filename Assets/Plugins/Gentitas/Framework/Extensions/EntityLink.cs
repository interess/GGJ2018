using System;
using Entitas.Unity;
using UnityEngine;

namespace Entitas.Unity
{
    public static class EntitySafeLinkExtension
    {
        public static EntityLink LinkSafe(this GameObject gameObject, IEntity entity, IContext context, bool relink = true)
        {
            var link = gameObject.GetEntityLink();

            if (link == null)
            {
                link = gameObject.AddComponent<EntityLink>();
            }

            if (link.entity != null && relink)
            {
                link.Unlink();
                link.Link(entity, context);
            }
            else if (link.entity == null)
            {
                link.Link(entity, context);
            }

            return link;
        }

        public static void UnlinkSafe(this GameObject gameObject)
        {
            var link = gameObject.GetEntityLink();

            if (link == null)
            {
                return;
            }

            if (link.entity != null)
            {
                link.Unlink();
            }
        }
    }
}