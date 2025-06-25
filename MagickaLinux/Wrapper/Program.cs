using System;
using System.Globalization;
using System.IO;
using Magicka;
using Microsoft.Xna.Framework;

namespace Magicka.Wrapper
{
    internal static class Program
    {
        /// <summary>
        /// Точка входа для Linux-версии без Steam и DRM.
        /// </summary>
        private static int Main(string[] args)
        {
            // Пусть рабочая директория будет всегда папкой с запускаемым файлом
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            // Обработка параметров командной строки (fullscreen, lobby, password)
            bool fullscreen = true;
            ulong startupLobby = 0;
            string startupPassword = null;

            for (int i = 0; i < args.Length; i++)
            {
                var a = args[i].ToLowerInvariant();

                if (a.Contains("-window"))
                {
                    fullscreen = false;
                }
                else if (a.Contains("+connect_lobby") && i + 1 < args.Length)
                {
                    if (!ulong.TryParse(args[i + 1], NumberStyles.Integer, CultureInfo.InvariantCulture, out startupLobby))
                        startupLobby = 0;
                }
                else if (a.Contains("+password") && i + 1 < args.Length)
                {
                    startupPassword = args[i + 1];
                }
            }

            // Применяем настройки
            GlobalSettings.Instance.Fullscreen   = fullscreen;
            GlobalSettings.Instance.StartupLobby  = startupLobby;
            GlobalSettings.Instance.StartupPassword = startupPassword;

            // Запуск игры
            try
            {
                using (var game = Game.Instance)
                {
                    game.Run();
                }
            }
            catch (Exception ex)
            {
                // Здесь можно при необходимости залогировать или создать дамп
                Console.Error.WriteLine("Fatal error: " + ex);
                return 1;
            }

            return 0;
        }
    }
}

