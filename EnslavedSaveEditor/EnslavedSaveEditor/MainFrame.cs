using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace EnslavedSaveEditor
{
    public partial class MainFrame : Form
    {
        String SaveFileName = "";
        FileStream SaveFile = null;
        FileStream NewFile = null;

        public MainFrame()
        {
            InitializeComponent();
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            SaveButton.Enabled = false;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            DialogResult result = OpenFileCtrl.ShowDialog();
            // OK button was pressed.
            if (result == DialogResult.OK)
            {
                SaveFileName = OpenFileCtrl.FileName;
                try
                {
                    // Output the requested file in richTextBox1.
                    SaveFile = new FileStream(SaveFileName, FileMode.Open, FileAccess.Read);
                    if (SaveFile != null && SaveFile.CanRead)
                    {
                        if (ReadIntFromFile(0x0) == 0x434F4E20)
                        {
                            MessageBox.Show("Please extract actual save file from CON file!", "Caution", MessageBoxButtons.OK);
                            SaveFile.Close();
                        }
                        SaveFile.Close();
                        MessageBox.Show("Original save file backup as \"" + SaveFileName + ".BAK" + "\"", "Note", MessageBoxButtons.OK);
                        File.Copy(SaveFileName, SaveFileName + ".BAK");
                        SaveFile = new FileStream(SaveFileName + ".BAK", FileMode.Open, FileAccess.Read);

                        SaveButton.Enabled = true;
                        LoadButton.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show(SaveFileName + ": File is not readable!?", "Error", MessageBoxButtons.OK);
                        SaveFile.Close();
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred while attempting to load the file. The error is: "
                                    + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
                }
            }

            // Cancel button was pressed.
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private int ReadIntFromFile(long offset_from_beginning)
        {
            byte[] tmpint = ReadBytesFromFile(offset_from_beginning, 4);
            if (BitConverter.IsLittleEndian) Array.Reverse(tmpint);
            return BitConverter.ToInt32(tmpint, 0);
        }

        private void WriteIntToFile( int value)
        {
            byte[] temp = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) Array.Reverse(temp);
            WriteBytesToFile(temp, 4);
        }

        private float ReadFloatFromFile(long offset_from_beginning)
        {
            byte[] tmpint = ReadBytesFromFile(offset_from_beginning, 4);
            if (BitConverter.IsLittleEndian) Array.Reverse(tmpint);
            return BitConverter.ToSingle(tmpint, 0);
        }

        private void WriteFloatToFile(float value)
        {
            byte[] temp = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) Array.Reverse(temp);
            WriteBytesToFile(temp, 4);
        }

        private byte[] ReadBytesFromFile(long offset_from_beginning, int num_bytes)
        {
            byte[] tmpint = new byte[num_bytes];
            long ret = SaveFile.Seek(offset_from_beginning, SeekOrigin.Begin);
            SaveFile.Read(tmpint, 0, num_bytes);
            return tmpint;
        }

        private void WriteBytesToFile(byte[] value, int num_bytes = 0)
        {
            byte[] temp = value;
            int length = num_bytes;
            if (length == 0) length = temp.Length;
            //long ret = NewFile.Seek(offset_from_beginning, SeekOrigin.Begin);
            NewFile.Write(temp, 0, length);
        }

        private string ReadStringFromFile(long offset_from_beginning, int num_bytes)
        {
            byte[] temp = ReadBytesFromFile(offset_from_beginning, num_bytes);
            return ASCIIEncoding.ASCII.GetString(temp);
        }

        private void WriteStringToFile(string value)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(value);
            WriteBytesToFile(temp, temp.Length);
            if (temp[temp.Length - 1] != 0x00)
            {
                byte[] tmp = { 0x00 };
                WriteBytesToFile(tmp, tmp.Length);
            }
        }

        private long FindFFFF(long offset_from_beginning)
        {
            long offset;
            bool isFound = false;
            for(offset = offset_from_beginning; isFound == false; offset++) {
                byte[] temp = ReadBytesFromFile(offset, 1);
                if(temp[0] == 0xFF){
                    byte[] tmp = ReadBytesFromFile(offset, 4);
                    if(tmp[0] == 0xFF && tmp[1] == 0xFF && tmp[2] == 0xFF && tmp[3] == 0xFF)
                        isFound = true;
                }
            }
            return offset-1;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Output the requested file in richTextBox1.
                NewFile = new FileStream(SaveFileName, FileMode.Create, FileAccess.Write);
                if (NewFile != null && NewFile.CanWrite)
                {
                    long OrgOffset = 0x0;

                    OrgOffset += CopyUntilFFFF(OrgOffset); // Copy first block (descriptive strings + additional info)
                    OrgOffset += CopyUntilFFFF(OrgOffset); // Copy third block (checkpoint info)

                    OrgOffset += CopyUntilFFFF(OrgOffset); // ch1_main

                    OrgOffset += CopyUntilFFFF(OrgOffset); // ch2_main
                    OrgOffset += CopyBlock(OrgOffset, OrgOffset + 0x10); // Prolude
                    OrgOffset += UnlockOrbArea(OrgOffset, 20, "ch2_main");

                    CopyBlock(OrgOffset, SaveFile.Length);
                    SaveFile.Close();
                    NewFile.Close();

                    // Now fix the hash.
                    SHA1 sha = new SHA1CryptoServiceProvider();
                    NewFile = new FileStream(SaveFileName, FileMode.Open, FileAccess.ReadWrite);
                    NewFile.Seek(0x14, SeekOrigin.Begin);
                    byte[] FileContent = new byte[(NewFile.Length - 0x14)];
                    int ret = NewFile.Read(FileContent, 0, (int) (NewFile.Length - 0x14));
                    byte[] HashBytes = sha.ComputeHash(FileContent);
                    NewFile.Seek(0x0, SeekOrigin.Begin);
                    NewFile.Write(HashBytes, 0, 0x14);
                    NewFile.Close();

                    MessageBox.Show("Save has been patched!", "Successful", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("File is not writable!?", "Error", MessageBoxButtons.OK);
                    NewFile.Close();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("An error occurred while attempting to patch the file. The error is:"
                                + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
            }
        }

        private long CopyBlock(long StartOffset, long EndOffset)
        {
            long CopyLength = EndOffset - StartOffset;
            byte[] temp = ReadBytesFromFile(StartOffset, (int) CopyLength);
            WriteBytesToFile(temp);
            return CopyLength;
        }

        private long CopyUntilFFFF(long StartOffset)
        {
            long FFFFOffset = FindFFFF(StartOffset);
#if DEBUG
            //MessageBox.Show("FFFF Found @ " + String.Format("{0:X}", FFFFOffset) + " From " + String.Format("{0:X}", StartOffset), "Debug", MessageBoxButtons.OK);
#endif
            return CopyBlock(StartOffset, (FFFFOffset + 4));
        }

        private long UnlockOrbArea(long StartOffset, int AreaOrbs, string AreaName = "")
        {
            long ReadOffset = StartOffset;
            bool[] OrbUnlocked = new bool[AreaOrbs];
            for (int i = 0; i < OrbUnlocked.Length; i++) OrbUnlocked[i] = false;
            long ActualEntries = ReadIntFromFile(ReadOffset);
            ReadOffset += 4;
            WriteIntToFile(AreaOrbs);
            for (int i = 0; i < ActualEntries; i++)
            {
                int StringLength = ReadIntFromFile(ReadOffset);
                ReadOffset += 4;
                WriteIntToFile(StringLength);
                string OrbString = ReadStringFromFile(ReadOffset, StringLength - 1);
                ReadOffset += StringLength;
                WriteStringToFile(OrbString);
                int OrbIndex = Convert.ToInt32(OrbString.Remove(0, 18), 10);
                if(OrbIndex < OrbUnlocked.Length)
                    OrbUnlocked[OrbIndex] = true;
                else
                    MessageBox.Show(AreaName + " area has more orbs than expected. " + System.Environment.NewLine + 
                                    "Real: " + Convert.ToString(OrbIndex) + System.Environment.NewLine +
                                    "Expected: " + Convert.ToString(AreaOrbs) + System.Environment.NewLine + System.Environment.NewLine +
                                    "Your new save file may be corrupted." + System.Environment.NewLine +
                                    "Please use the backup instead." + System.Environment.NewLine + System.Environment.NewLine +
                                    "Please report this error back.", "Error", MessageBoxButtons.OK);
            }
            for (int i = 0; i < OrbUnlocked.Length; i++)
            {
                if (OrbUnlocked[i] == false)
                {
                    WriteIntToFile((i < 10) ? 20 : (i < 100) ? 21 : 22);
                    WriteStringToFile("MKPickup_TechOrbs_" + Convert.ToString(i));
                }
            }

            return ReadOffset - StartOffset;
        }

    }
}
