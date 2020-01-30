using IFramework;

namespace FormSever
{
    public class QuitState : IAPPState
    {
        public void OnEnter()
        {
            Log.E("quit");
        }

        public void OnExit()
        {
        }

        public void Update()
        {
        }
    }

}
