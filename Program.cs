using System;
using System.IO;

namespace MHW_Weapon_Hider {
    public static class Program {
        [STAThread]
        public static void Main(string[] args) {
            if (args.Length < 1) return;
            var target = args[0];

            var mod3Files = Directory.EnumerateFiles(target, "*.mod3", SearchOption.AllDirectories);

            foreach (var file in mod3Files) {
                var isPartOrEmblem = file.Contains(@"\parts\") || file.Contains(@"\emblem\");
                ParseMod3(file, !isPartOrEmblem, isPartOrEmblem);
            }
        }

        private static void ParseMod3(string targetFile, bool alterVisCon, bool alterLod) { // ^\\wp\\.+\.mod3$
            if (!alterVisCon && !alterLod) return;

            using var reader = new BinaryReader(new FileStream(targetFile, FileMode.Open, FileAccess.ReadWrite));
            var stream = reader.BaseStream;

            stream.Seek(4, SeekOrigin.Begin);
            var version = reader.ReadByte();

            stream.Seek(3, SeekOrigin.Current);
            var meshCount = reader.ReadUInt16();

            var toSkip = 54;
            if (version < 190) toSkip += 4;
            if ((version < 190) || (version > 220)) toSkip += 8;
            stream.Seek(toSkip, SeekOrigin.Current);
            var meshOffset = reader.ReadUInt64();

            for (ulong i = 0; i < meshCount; i++) {
                var offset = (long) (meshOffset + 80 * i);

                if (alterVisCon) {
                    stream.Seek(offset + 4, SeekOrigin.Begin);
                    var visCon = reader.ReadInt16();

                    if (visCon == 0 || visCon == 20 || targetFile.Contains(@"\lbg\") || targetFile.Contains(@"\hbg\")) {
                        stream.Seek(-2, SeekOrigin.Current);
                        stream.WriteByte(1);
                        stream.WriteByte(0);
                    }
                }

                if (alterLod) {
                    stream.Seek(offset + 8, SeekOrigin.Begin);
                    for (var s = 0; s < 4; s++) {
                        stream.WriteByte(0);
                    }
                }
            }
        }
    }
}