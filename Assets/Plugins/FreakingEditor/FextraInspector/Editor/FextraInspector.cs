using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FreakingEditor
{
    public interface IFextraInspector { }

    public interface IFextraBeforeInspector : IFextraInspector
    {
        void OnBeforeInspectorGUI(Object[] targets);
    }

    public interface IFextraAfterInspector : IFextraInspector
    {
        void OnAfterInspectorGUI(Object[] targets);
    }

    public interface IFextraMonoBehaviourBeforeInspector : IFextraBeforeInspector { }
    public interface IFextraMonoBehaviourAfterInspector : IFextraAfterInspector { }
    public interface IFextraScriptableObjectBeforeInspector : IFextraBeforeInspector { }
    public interface IFextraScriptableObjectAfterInspector : IFextraAfterInspector { }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class MonoBehaviourFextraInspector : UnityEditor.Editor
    {
        private static System.Collections.Generic.IEnumerable<System.Type> fextraClasses;
        private static System.Collections.Generic.IEnumerable<System.Type> fextraBeforeClasses;
        private static System.Collections.Generic.IEnumerable<System.Type> fextraAfterClasses;

        private static List<MethodInfo> methodsBefore = new List<MethodInfo>();
        private static List<IFextraBeforeInspector> classesBefore = new List<IFextraBeforeInspector>();
        private static List<MethodInfo> methodsAfter = new List<MethodInfo>();
        private static List<IFextraAfterInspector> classesAfter = new List<IFextraAfterInspector>();

        [InitializeOnLoadMethod]
        public static void Initialize()
        {
            var fextraInspectorType = typeof(IFextraInspector);
            var fextraBeforeInspector = typeof(IFextraMonoBehaviourBeforeInspector);
            var fextraAfterInspector = typeof(IFextraMonoBehaviourAfterInspector);

            fextraClasses = System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => fextraInspectorType.IsAssignableFrom(p))
                .Where(p => p.IsClass);

            fextraBeforeClasses = fextraClasses
                .Where(p => fextraBeforeInspector.IsAssignableFrom(p));

            fextraAfterClasses = fextraClasses
                .Where(p => fextraAfterInspector.IsAssignableFrom(p));

            foreach (var item in fextraBeforeClasses)
            {
                methodsBefore.Add(item.GetMethod("OnBeforeInspectorGUI", BindingFlags.Instance | BindingFlags.Public));
                classesBefore.Add((IFextraBeforeInspector) System.Activator.CreateInstance(item));
            }

            foreach (var item in fextraAfterClasses)
            {
                methodsAfter.Add(item.GetMethod("OnAfterInspectorGUI", BindingFlags.Instance | BindingFlags.Public));
                classesAfter.Add((IFextraAfterInspector) System.Activator.CreateInstance(item));
            }
        }

        public override void OnInspectorGUI()
        {
            var targetsObject = new object[] { targets };

            for (int i = 0; i < methodsBefore.Count; i++)
            {
                methodsBefore[i].Invoke(classesBefore[i], targetsObject);
            }

            DrawDefaultInspector();

            for (int i = 0; i < methodsAfter.Count; i++)
            {
                methodsAfter[i].Invoke(classesAfter[i], targetsObject);
            }
        }
    }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ScriptableObjectFextraInspector : UnityEditor.Editor
    {
        private static System.Collections.Generic.IEnumerable<System.Type> fextraClasses;
        private static System.Collections.Generic.IEnumerable<System.Type> fextraBeforeClasses;
        private static System.Collections.Generic.IEnumerable<System.Type> fextraAfterClasses;

        private static List<MethodInfo> methodsBefore = new List<MethodInfo>();
        private static List<IFextraBeforeInspector> classesBefore = new List<IFextraBeforeInspector>();
        private static List<MethodInfo> methodsAfter = new List<MethodInfo>();
        private static List<IFextraAfterInspector> classesAfter = new List<IFextraAfterInspector>();

        [InitializeOnLoadMethod]
        public static void Initialize()
        {
            var fextraInspectorType = typeof(IFextraInspector);
            var fextraBeforeInspector = typeof(IFextraMonoBehaviourBeforeInspector);
            var fextraAfterInspector = typeof(IFextraMonoBehaviourAfterInspector);

            fextraClasses = System.AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => fextraInspectorType.IsAssignableFrom(p))
                .Where(p => p.IsClass);

            fextraBeforeClasses = fextraClasses
                .Where(p => fextraBeforeInspector.IsAssignableFrom(p));

            fextraAfterClasses = fextraClasses
                .Where(p => fextraAfterInspector.IsAssignableFrom(p));

            foreach (var item in fextraBeforeClasses)
            {
                methodsBefore.Add(item.GetMethod("OnBeforeInspectorGUI", BindingFlags.Instance | BindingFlags.Public));
                classesBefore.Add((IFextraBeforeInspector) System.Activator.CreateInstance(item));
            }

            foreach (var item in fextraAfterClasses)
            {
                methodsAfter.Add(item.GetMethod("OnAfterInspectorGUI", BindingFlags.Instance | BindingFlags.Public));
                classesAfter.Add((IFextraAfterInspector) System.Activator.CreateInstance(item));
            }
        }

        public override void OnInspectorGUI()
        {
            var targetsObject = new object[] { targets };

            for (int i = 0; i < methodsBefore.Count; i++)
            {
                methodsBefore[i].Invoke(classesBefore[i], targetsObject);
            }

            DrawDefaultInspector();

            for (int i = 0; i < methodsAfter.Count; i++)
            {
                methodsAfter[i].Invoke(classesAfter[i], targetsObject);
            }
        }
    }
}
