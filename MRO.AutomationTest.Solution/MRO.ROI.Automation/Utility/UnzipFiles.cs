using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System;

namespace MRO.ROI.Automation
{
    public class UnzipFiles
    {
        public static bool UnzipFile()
        {
            try
            {
                string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                string downloadFolder = Path.Combine(userRoot, "Downloads");
                return ReadFileFromDirectory(downloadFolder);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to unzip file whose exception message is :{ex.Message} \n whose stack trace is :{ex.StackTrace}");
            }
        }
        public static bool ReadFileFromDirectory(string path)
        {
            bool isUnZipped =false;
            try
            {
                DirectoryInfo Dir = new DirectoryInfo(path);
                List<FileInfo> files = Dir.GetFiles().ToList();
                foreach (FileInfo fileinfo in files)
                {
                    if (fileinfo.Extension == ".zip" && fileinfo.Name.Contains("ROIInvoice"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        ZipFile.ExtractToDirectory(path + "\\" + fileinfo.Name, path);
                        break;
                    }
                }
                isUnZipped = true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to read file from directory whose exception message is :{ex.Message} \n whose stack trace is :{ex.StackTrace}");
            }
            return isUnZipped;
        }

        public static void ZipFileAndCopyToShareLocation(string sourcePath, string targetPat)
        {
            try
            {
                DirectoryInfo Dir = new DirectoryInfo(sourcePath);
                var folders = Dir.GetDirectories();
                foreach (var folder in folders)
                {
                        ZipFile.CreateFromDirectory(folder.FullName, $"{targetPat}{folder.Name}.zip");
                        System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to zip and copy files to shared location, whose exception message is: {ex.Message} {Environment.NewLine} Whose stackTrace is: {ex.StackTrace}");
            }
        }

        public static string ReadXLSFileFromDirectory(string path)
        {
            string fileName = string.Empty;
            try
            {
                DirectoryInfo Dir = new DirectoryInfo(path);
                List<FileInfo> files = Dir.GetFiles().ToList();

                foreach (FileInfo fileinfo in files)
                {
                    if (fileinfo.Extension == ".xls" && fileinfo.Name.Contains("Curahealth_883"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        fileName = fileinfo.ToString().Replace(".xls", "").Trim();
                    }
                }

            }


            catch (Exception ex)
            {
                throw new Exception($"Failed to read file from directory whose exception message is :{ex.Message} \n whose stack trace is :{ex.StackTrace}");
            }
            return fileName;
        }
    }
}
