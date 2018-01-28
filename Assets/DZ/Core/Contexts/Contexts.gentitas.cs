using Entitas.Gentitas.Declaration;

namespace DZ.Core.Declaration
{
    public class State : Context
    {
        public State()
        {
            var Loaded = Component();
            var Application = Component().GroupSingle;
            var LoadingProgress = Component<float>();
            var SceneName = Component<string>().Index;
            var LoadingManagerUnit = Component<Scripts.LoadingManagerUnit>().GroupSingle;
            var LoadingSeconds = Component<float>();
        }
    }
}
