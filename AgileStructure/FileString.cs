using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    class FileString
    {
        public static string GetFolder(string title, string currentFolder)
        {
            string answer = "Cancel";

            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            if (System.IO.Directory.Exists(currentFolder) == true)
            { fbd.SelectedPath = currentFolder; }
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {answer = fbd.SelectedPath;}

            return answer;
        }

        public static string OpenAs(string title, string fileExtension)
        {
            System.Windows.Forms.OpenFileDialog ofg = new System.Windows.Forms.OpenFileDialog();
            ofg.CheckFileExists = true;
            ofg.AddExtension = true;
            ofg.Title = title;
            ofg.ValidateNames = true;
            ofg.Filter = fileExtension;
            if (ofg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ofg.FileName != null)
                {
                    return ofg.FileName;
                }
                else
                {
                    return "Cancel";
                }
            }
            else
            {
                return "Cancel";
            }
        }

       public static string SaveAs(string title, string fileExtension)
        {
            System.Windows.Forms.SaveFileDialog ofg = new System.Windows.Forms.SaveFileDialog();
            ofg.AddExtension = true;
            ofg.Title = title;
            ofg.ValidateNames = true;
            ofg.Filter = fileExtension;
            if (ofg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ofg.FileName != null)
                {
                    return ofg.FileName;
                }
                else
                {
                    return "Cancel";
                }
            }
            else
            {
                return "Cancel";
            }
        }

    }
}
