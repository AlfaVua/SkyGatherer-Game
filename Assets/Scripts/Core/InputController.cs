namespace Core
{
    public static class InputController
    {
        private static Inputs _inputs;

        public static Inputs Inputs => _inputs ??= new Inputs();
    }
}