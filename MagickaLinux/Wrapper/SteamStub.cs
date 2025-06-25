namespace SteamWrapper {
    public static class SteamAPI {
        public static bool RestartAppIfNecessary(uint _) => false;
        public static bool Init() => true;
        public static void Shutdown() {}
    }
    public static class SteamUtils {
        public static uint GetAppID() => 42910u;
    }
}

