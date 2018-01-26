using Entitas.Gentitas.Declaration;

namespace DZ.Core.Declaration
{
    public class State : Context
    {
        public State()
        {
            var loaded = Component();
            var application = Component().GroupSingle;
            var loadingProgress = Component<float>();
            var sceneName = Component<string>().Index;
            var loadingManagerUnit = Component<Scripts.LoadingManagerUnit>().GroupSingle;
        }
    }
}
