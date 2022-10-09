using System;

namespace WasmSpa.Helpers
{
    public static class GetStoreKeyHelper
    {
        public static string GetKey(Type type)
        {
            return type.FullName;
        }

        public static string GetKeyLocal(Type type)
        {
            return type.FullName + "local";
        }
    }
}