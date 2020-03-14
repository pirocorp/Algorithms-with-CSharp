namespace LazyTypes
{
    public sealed class LazyVoid
    {
        private static readonly LazyVoid _instance = new LazyVoid();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static LazyVoid()
        {

        }

        private LazyVoid()
        {
            
        }

        public static LazyVoid Instance => _instance;
    }
}
