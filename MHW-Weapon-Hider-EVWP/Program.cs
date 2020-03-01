using System;
using System.IO;

namespace MHW_Weapon_Hider_EVWP {
    public static class Program {
        [STAThread]
        public static void Main(string[] args) {
            if (args.Length < 1) return;
            var target = args[0];

            var mod3Files = Directory.EnumerateFiles(target, "*.evwp", SearchOption.AllDirectories);

            foreach (var file in mod3Files) {
                ParseEvwp(file);
            }
        }

        public static void ParseEvwp(string targetFile) { // ^\\wp\\.+\.evwp$
            using var writer = new BinaryWriter(new FileStream(targetFile, FileMode.Open, FileAccess.ReadWrite));
            var stream = writer.BaseStream;
            // x: 68, y: 72, z: 76
            stream.Seek(72, SeekOrigin.Begin);
            writer.Write(-5000f);
        }
    }
}