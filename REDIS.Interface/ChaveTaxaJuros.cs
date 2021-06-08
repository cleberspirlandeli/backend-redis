namespace REDIS.Interface
{
    public static class ChaveTaxaJuros
    {
        private static string KeyJurosComposto { get; } = "KeyJurosComposto";
        private static string KeyJurosSimples { get; } = "KeyJurosSimples";

        public static string GetKeyJurosComposto() => KeyJurosComposto;
        public static string GetKeyJurosSimples() => KeyJurosSimples;
    }
}
