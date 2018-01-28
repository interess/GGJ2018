using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace FreakingEditor
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FbuttonEditorAttribute : PropertyAttribute
    { }

    [AttributeUsage(AttributeTargets.Method)]
    public class FbuttonPlayAttribute : PropertyAttribute
    { }
}
