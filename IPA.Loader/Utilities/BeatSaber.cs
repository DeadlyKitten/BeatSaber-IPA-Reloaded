﻿using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Version = SemVer.Version;

namespace IPA.Utilities
{
    /// <summary>
    /// Provides some basic utility methods and properties of Beat Saber
    /// </summary>
    public static class BeatSaber
    {
        private static Version _gameVersion;
        /// <summary>
        /// Provides the current game version
        /// </summary>
        public static Version GameVersion => _gameVersion ?? (_gameVersion = new Version(Application.version, true));

        /// <summary>
        /// The different types of releases of the game.
        /// </summary>
        public enum Release
        {
            /// <summary>
            /// Indicates a Steam release.
            /// </summary>
            Steam,
            /// <summary>
            /// Indicates an Oculus release.
            /// </summary>
            Oculus
        }
        private static Release? _releaseCache;
        /// <summary>
        /// Gets the <see cref="Release"/> type of this installation of Beat Saber
        /// </summary>
        public static Release ReleaseType => (_releaseCache ?? (_releaseCache = FindSteamVRAsset() ? Release.Steam : Release.Oculus)).Value;

        private static string _installRoot;
        /// <summary>
        /// The path to the Beat Saber install dir
        /// </summary>
        public static string InstallPath
        {
            get
            {
                if (_installRoot == null)
                    _installRoot = Path.GetFullPath(
                        Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", ".."));
                return _installRoot;
            }
        }
        /// <summary>
        /// The path to the `Libs` folder. Use only if necessary.
        /// </summary>
        public static string LibraryPath => Path.Combine(InstallPath, "Libs");
        /// <summary>
        /// The path to the `Libs\Native` folder. Use only if necessary.
        /// </summary>
        public static string NativeLibraryPath => Path.Combine(LibraryPath, "Native");
        /// <summary>
        /// The directory to load plugins from.
        /// </summary>
        public static string PluginsPath => Path.Combine(InstallPath, "Plugins");
        /// <summary>
        /// The path to the `UserData` folder.
        /// </summary>
        public static string UserDataPath => Path.Combine(InstallPath, "UserData");

        private static bool FindSteamVRAsset()
        {
            // these require assembly qualified names....
            var steamUser = Type.GetType("Steamworks.SteamUser, Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", false);

            return steamUser != null;
        }
    }
}
