using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordSelector.Properties;

namespace WordSelector
{
    public class FileAssociation
    {
        public string Extension { get; set; }
        public string ProgId { get; set; }
        public string FileTypeDescription { get; set; }
        public string ExecutableFilePath { get; set; } = Process.GetCurrentProcess().MainModule.FileName;
        public string IconPath { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "textIcon.ico");
    }

    public static class FileAssociations
    {
        // needed so that Explorer windows get refreshed after the registry is updated
        [System.Runtime.InteropServices.DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

        private const int SHCNE_ASSOCCHANGED = 0x8000000;
        private const int SHCNF_FLUSH = 0x1000;

        public static void EnsureAssociationsSet()
        {
            //string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "textIcon.ico");
            //var filePath = Process.GetCurrentProcess().MainModule.FileName;
            EnsureAssociationsSet(
                new FileAssociation
                {
                    Extension = ".binlog",
                    ProgId = "MSBuildBinaryLog",
                    FileTypeDescription = "MSBuild Binary Log"
                },
                new FileAssociation
                {
                    Extension = ".buildlog",
                    ProgId = "MSBuildStructuredLog"
                },
                new FileAssociation
                {
                    Extension = ".abc",
                    ProgId = "MyWordSelection",
                    FileTypeDescription = "My Word Selection Program"
                }
                );
        }

        public static void EnsureAssociationsSet(params FileAssociation[] associations)
        {
            bool madeChanges = false;
            foreach (var association in associations)
            {
                madeChanges |= SetAssociation(association);
            }

            if (madeChanges)
            {
                SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, IntPtr.Zero, IntPtr.Zero);
            }
        }

        public static bool SetAssociation(this FileAssociation fileAssociation)
        {
            bool madeChanges = false;
            madeChanges |= SetKeyDefaultValue(@"Software\Classes\" + fileAssociation.Extension, fileAssociation.ProgId);
            madeChanges |= SetKeyDefaultValue(@"Software\Classes\" + fileAssociation.ProgId, fileAssociation.FileTypeDescription);
            madeChanges |= SetKeyDefaultValue($@"Software\Classes\{fileAssociation.ProgId}\shell\open\command", "\"" + fileAssociation.ExecutableFilePath + "\" \"%1\"");
            madeChanges |= SetKeyDefaultValue(@"Software\Classes\" + fileAssociation.ProgId + @"\DefaultIcon", fileAssociation.IconPath);
            return madeChanges;
        }

        private static bool SetKeyDefaultValue(string keyPath, string value)
        {
            try
            {
                using (var key = Registry.CurrentUser.CreateSubKey(keyPath))
                {

                    if (key.GetValue(null) as string != value)
                    {
                        key.SetValue(null, value);
                        return true;
                    }
                }

                return false;
            }
            catch 
            {

            }
            return false;
        }
    }
}
