using System;

namespace CustomOption.CustomOption
{
    public class Generate
    {
        public static void GenerateAll()
        {
            var num = 0;

            Patches.ExportButton = new Export(num++);
            Patches.ImportButton = new Import(num++);
        }
    }
}