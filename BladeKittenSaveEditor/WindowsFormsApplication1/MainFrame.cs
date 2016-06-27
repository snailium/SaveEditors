using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SaveEditor
{
    public partial class BKSE : Form
    {
        String SaveFileName = "";
        FileStream SaveFile = null;
        long SaveOffset = 0;
        
        public BKSE()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            if (SaveFile != null)
                SaveFile.Close();
            this.Close();
        }

        private void BKSE_Load(object sender, EventArgs e)
        {
            SaveButton.Enabled = false;

            HPMaxButton.Enabled = false;
            StaminaMaxButton.Enabled = false;
            L1UnlockButton.Enabled = false;
            L2UnlockButton.Enabled = false;
            L3UnlockButton.Enabled = false;
            L4UnlockButton.Enabled = false;
            L5UnlockButton.Enabled = false;
            L6UnlockButton.Enabled = false;
            L8UnlockButton.Enabled = false;
            L9UnlockButton.Enabled = false;
            L10UnlockButton.Enabled = false;
            L11UnlockButton.Enabled = false;
            L12UnlockButton.Enabled = false;
            UnlockDPButton.Enabled = false;
            UnlockSkiffButton.Enabled = false;
            UnlockAllButton.Enabled = false;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            DialogResult result = OpenFile.ShowDialog();
            // OK button was pressed.
            if (result == DialogResult.OK)
            {
                SaveFileName = OpenFile.FileName;
                try
                {
                    // Output the requested file in richTextBox1.
                    SaveFile = new FileStream(SaveFileName, FileMode.Open, FileAccess.ReadWrite);
                    LoadSaveFile();
                }
                catch (Exception exp)
                {
                    MessageBox.Show("An error occurred while attempting to load the file. The error is:"
                                    + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
                }
            }

            // Cancel button was pressed.
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private void LoadSaveFile()
        {
            if (SaveFile != null && SaveFile.CanRead)
            {
                if (ReadIntFromFile(0x0) == 0x42756666)
                {
                    SaveOffset = 0;
                }
                else if (ReadIntFromFile(0xD000) == 0x42756666)
                {
                    SaveOffset = 0xD000;
                }
                else
                {
                    MessageBox.Show("Cannot recognize save!", "Error", MessageBoxButtons.OK);
                    SaveFile.Close();
                    return;
                }
                
                // Read Hex (Money)
                HexText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0020));

                // Read Health
                HPText.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x0034));

                // Read Stamina
                StaminaText.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x0038));

                // Read Kill Count
                KillText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0040));

                // Read Total Time
                TotalTimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x006C));

                // Read Level Stats
                L1ChestText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0088 + 0x30 * 0));
                L1SkiffText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x008C + 0x30 * 0));
                L1DatapakText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0090 + 0x30 * 0));
                L1TimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0098 + 0x30 * 0));
                L1HPLostText.Text = Convert.ToString(ReadFloatFromFile(SaveOffset + 0x00B0 + 0x30 * 0));

                L2ChestText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0088 + 0x30 * 1));
                L2SkiffText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x008C + 0x30 * 1));
                L2DatapakText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0090 + 0x30 * 1));
                L2TimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0098 + 0x30 * 1));
                L2HPLostText.Text = Convert.ToString(ReadFloatFromFile(SaveOffset + 0x00B0 + 0x30 * 1));

                L3ChestText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0088 + 0x30 * 2));
                L3SkiffText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x008C + 0x30 * 2));
                L3DatapakText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0090 + 0x30 * 2));
                L3TimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0098 + 0x30 * 2));
                L3HPLostText.Text = Convert.ToString(ReadFloatFromFile(SaveOffset + 0x00B0 + 0x30 * 2));

                L4ChestText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0088 + 0x30 * 3));
                L4SkiffText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x008C + 0x30 * 3));
                L4DatapakText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0090 + 0x30 * 3));
                L4TimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0098 + 0x30 * 3));
                L4HPLostText.Text = Convert.ToString(ReadFloatFromFile(SaveOffset + 0x00B0 + 0x30 * 3));

                L5ChestText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0088 + 0x30 * 4));
                L5SkiffText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x008C + 0x30 * 4));
                L5DatapakText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0090 + 0x30 * 4));
                L5TimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0098 + 0x30 * 4));
                L5HPLostText.Text = Convert.ToString(ReadFloatFromFile(SaveOffset + 0x00B0 + 0x30 * 4));

                L6ChestText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0088 + 0x30 * 5));
                L6SkiffText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x008C + 0x30 * 5));
                L6DatapakText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0090 + 0x30 * 5));
                L6TimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0098 + 0x30 * 5));
                L6HPLostText.Text = Convert.ToString(ReadFloatFromFile(SaveOffset + 0x00B0 + 0x30 * 5));

                L7ChestText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0088 + 0x30 * 6));
                L7SkiffText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x008C + 0x30 * 6));
                L7DatapakText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0090 + 0x30 * 6));
                L7TimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0098 + 0x30 * 6));
                L7HPLostText.Text = Convert.ToString(ReadFloatFromFile(SaveOffset + 0x00B0 + 0x30 * 6));

                L8ChestText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0088 + 0x30 * 7));
                L8SkiffText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x008C + 0x30 * 7));
                L8DatapakText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0090 + 0x30 * 7));
                L8TimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0098 + 0x30 * 7));
                L8HPLostText.Text = Convert.ToString(ReadFloatFromFile(SaveOffset + 0x00B0 + 0x30 * 7));

                L9ChestText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0088 + 0x30 * 8));
                L9SkiffText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x008C + 0x30 * 8));
                L9DatapakText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0090 + 0x30 * 8));
                L9TimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0098 + 0x30 * 8));
                L9HPLostText.Text = Convert.ToString(ReadFloatFromFile(SaveOffset + 0x00B0 + 0x30 * 8));

                L10ChestText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0088 + 0x30 * 9));
                L10SkiffText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x008C + 0x30 * 9));
                L10DatapakText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0090 + 0x30 * 9));
                L10TimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0098 + 0x30 * 9));
                L10HPLostText.Text = Convert.ToString(ReadFloatFromFile(SaveOffset + 0x00B0 + 0x30 * 9));

                L11ChestText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0088 + 0x30 * 10));
                L11SkiffText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x008C + 0x30 * 10));
                L11DatapakText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0090 + 0x30 * 10));
                L11TimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0098 + 0x30 * 10));
                L11HPLostText.Text = Convert.ToString(ReadFloatFromFile(SaveOffset + 0x00B0 + 0x30 * 10));

                L12ChestText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0088 + 0x30 * 11));
                L12SkiffText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x008C + 0x30 * 11));
                L12DatapakText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0090 + 0x30 * 11));
                L12TimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0098 + 0x30 * 11));
                L12HPLostText.Text = Convert.ToString(ReadFloatFromFile(SaveOffset + 0x00B0 + 0x30 * 11));

                L13ChestText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0088 + 0x30 * 12));
                L13SkiffText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x008C + 0x30 * 12));
                L13DatapakText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0090 + 0x30 * 12));
                L13TimeText.Text = Convert.ToString(ReadIntFromFile(SaveOffset + 0x0098 + 0x30 * 12));
                L13HPLostText.Text = Convert.ToString(ReadFloatFromFile(SaveOffset + 0x00B0 + 0x30 * 12));

                // Read Collectable Flags
                DatapakFlags.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x09E8));
                SkiffFlags.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x09EC));
                L1ChestFlags.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x0A14));
                L2ChestFlags.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x0A18));
                L3ChestFlags.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x0A1C));
                L4ChestFlags.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x0A20));
                L5ChestFlags.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x0A24));
                L6ChestFlags.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x0A28));
                L7ChestFlags.Text = "-"; // No Chest
                L8ChestFlags.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x0A2C));
                L9ChestFlags.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x0A30));
                L10ChestFlags.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x0A34));
                L11ChestFlags.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x0A38));
                L12ChestFlags.Text = String.Format("{0:X}", ReadIntFromFile(SaveOffset + 0x0A3C));
                L13ChestFlags.Text = "-"; // No Chest

                SaveButton.Enabled = true;
                LoadButton.Enabled = false;

                HPMaxButton.Enabled = true;
                StaminaMaxButton.Enabled = true;
                L1UnlockButton.Enabled = true;
                L2UnlockButton.Enabled = true;
                L3UnlockButton.Enabled = true;
                L4UnlockButton.Enabled = true;
                L5UnlockButton.Enabled = true;
                L6UnlockButton.Enabled = true;
                L8UnlockButton.Enabled = true;
                L9UnlockButton.Enabled = true;
                L10UnlockButton.Enabled = true;
                L11UnlockButton.Enabled = true;
                L12UnlockButton.Enabled = true;
                UnlockDPButton.Enabled = true;
                UnlockSkiffButton.Enabled = true;
                UnlockAllButton.Enabled = true;
            }
        }

        private void UpdateSaveFile()
        {
            if (SaveFile != null && SaveFile.CanWrite)
            {
                // Write Hex (Money)
                WriteIntToFile(SaveOffset + 0x0020, Convert.ToInt32(HexText.Text, 10));

                // Write Health
                WriteIntToFile(SaveOffset + 0x0034, Convert.ToInt32(HPText.Text, 16));

                // Write Stamina
                WriteIntToFile(SaveOffset + 0x0038, Convert.ToInt32(StaminaText.Text, 16));

                // Read Kill Count
                WriteIntToFile(SaveOffset + 0x0040, Convert.ToInt32(KillText.Text, 10));

                // Write Total Time
                WriteIntToFile(SaveOffset + 0x006C, Convert.ToInt32(TotalTimeText.Text, 10));

                // Write Level Stats
                WriteIntToFile(SaveOffset + 0x0088 + 0x30 * 0, Convert.ToInt32(L1ChestText.Text, 10));
                WriteIntToFile(SaveOffset + 0x008C + 0x30 * 0, Convert.ToInt32(L1SkiffText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0090 + 0x30 * 0, Convert.ToInt32(L1DatapakText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0098 + 0x30 * 0, Convert.ToInt32(L1TimeText.Text, 10));
                WriteFloatToFile(SaveOffset + 0x00B0 + 0x30 * 0, Convert.ToSingle(L1HPLostText.Text));

                WriteIntToFile(SaveOffset + 0x0088 + 0x30 * 1, Convert.ToInt32(L2ChestText.Text, 10));
                WriteIntToFile(SaveOffset + 0x008C + 0x30 * 1, Convert.ToInt32(L2SkiffText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0090 + 0x30 * 1, Convert.ToInt32(L2DatapakText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0098 + 0x30 * 1, Convert.ToInt32(L2TimeText.Text, 10));
                WriteFloatToFile(SaveOffset + 0x00B0 + 0x30 * 1, Convert.ToSingle(L2HPLostText.Text));

                WriteIntToFile(SaveOffset + 0x0088 + 0x30 * 2, Convert.ToInt32(L3ChestText.Text, 10));
                WriteIntToFile(SaveOffset + 0x008C + 0x30 * 2, Convert.ToInt32(L3SkiffText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0090 + 0x30 * 2, Convert.ToInt32(L3DatapakText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0098 + 0x30 * 2, Convert.ToInt32(L3TimeText.Text, 10));
                WriteFloatToFile(SaveOffset + 0x00B0 + 0x30 * 2, Convert.ToSingle(L3HPLostText.Text));

                WriteIntToFile(SaveOffset + 0x0088 + 0x30 * 3, Convert.ToInt32(L4ChestText.Text, 10));
                WriteIntToFile(SaveOffset + 0x008C + 0x30 * 3, Convert.ToInt32(L4SkiffText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0090 + 0x30 * 3, Convert.ToInt32(L4DatapakText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0098 + 0x30 * 3, Convert.ToInt32(L4TimeText.Text, 10));
                WriteFloatToFile(SaveOffset + 0x00B0 + 0x30 * 3, Convert.ToSingle(L4HPLostText.Text));

                WriteIntToFile(SaveOffset + 0x0088 + 0x30 * 4, Convert.ToInt32(L5ChestText.Text, 10));
                WriteIntToFile(SaveOffset + 0x008C + 0x30 * 4, Convert.ToInt32(L5SkiffText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0090 + 0x30 * 4, Convert.ToInt32(L5DatapakText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0098 + 0x30 * 4, Convert.ToInt32(L5TimeText.Text, 10));
                WriteFloatToFile(SaveOffset + 0x00B0 + 0x30 * 4, Convert.ToSingle(L5HPLostText.Text));

                WriteIntToFile(SaveOffset + 0x0088 + 0x30 * 5, Convert.ToInt32(L6ChestText.Text, 10));
                WriteIntToFile(SaveOffset + 0x008C + 0x30 * 5, Convert.ToInt32(L6SkiffText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0090 + 0x30 * 5, Convert.ToInt32(L6DatapakText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0098 + 0x30 * 5, Convert.ToInt32(L6TimeText.Text, 10));
                WriteFloatToFile(SaveOffset + 0x00B0 + 0x30 * 5, Convert.ToSingle(L6HPLostText.Text));

                WriteIntToFile(SaveOffset + 0x0088 + 0x30 * 6, Convert.ToInt32(L7ChestText.Text, 10));
                WriteIntToFile(SaveOffset + 0x008C + 0x30 * 6, Convert.ToInt32(L7SkiffText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0090 + 0x30 * 6, Convert.ToInt32(L7DatapakText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0098 + 0x30 * 6, Convert.ToInt32(L7TimeText.Text, 10));
                WriteFloatToFile(SaveOffset + 0x00B0 + 0x30 * 6, Convert.ToSingle(L7HPLostText.Text));

                WriteIntToFile(SaveOffset + 0x0088 + 0x30 * 7, Convert.ToInt32(L8ChestText.Text, 10));
                WriteIntToFile(SaveOffset + 0x008C + 0x30 * 7, Convert.ToInt32(L8SkiffText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0090 + 0x30 * 7, Convert.ToInt32(L8DatapakText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0098 + 0x30 * 7, Convert.ToInt32(L8TimeText.Text, 10));
                WriteFloatToFile(SaveOffset + 0x00B0 + 0x30 * 7, Convert.ToSingle(L8HPLostText.Text));

                WriteIntToFile(SaveOffset + 0x0088 + 0x30 * 8, Convert.ToInt32(L9ChestText.Text, 10));
                WriteIntToFile(SaveOffset + 0x008C + 0x30 * 8, Convert.ToInt32(L9SkiffText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0090 + 0x30 * 8, Convert.ToInt32(L9DatapakText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0098 + 0x30 * 8, Convert.ToInt32(L9TimeText.Text, 10));
                WriteFloatToFile(SaveOffset + 0x00B0 + 0x30 * 8, Convert.ToSingle(L9HPLostText.Text));

                WriteIntToFile(SaveOffset + 0x0088 + 0x30 * 9, Convert.ToInt32(L10ChestText.Text, 10));
                WriteIntToFile(SaveOffset + 0x008C + 0x30 * 9, Convert.ToInt32(L10SkiffText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0090 + 0x30 * 9, Convert.ToInt32(L10DatapakText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0098 + 0x30 * 9, Convert.ToInt32(L10TimeText.Text, 10));
                WriteFloatToFile(SaveOffset + 0x00B0 + 0x30 * 9, Convert.ToSingle(L10HPLostText.Text));

                WriteIntToFile(SaveOffset + 0x0088 + 0x30 * 10, Convert.ToInt32(L11ChestText.Text, 10));
                WriteIntToFile(SaveOffset + 0x008C + 0x30 * 10, Convert.ToInt32(L11SkiffText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0090 + 0x30 * 10, Convert.ToInt32(L11DatapakText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0098 + 0x30 * 10, Convert.ToInt32(L11TimeText.Text, 10));
                WriteFloatToFile(SaveOffset + 0x00B0 + 0x30 * 10, Convert.ToSingle(L11HPLostText.Text));

                WriteIntToFile(SaveOffset + 0x0088 + 0x30 * 11, Convert.ToInt32(L12ChestText.Text, 10));
                WriteIntToFile(SaveOffset + 0x008C + 0x30 * 11, Convert.ToInt32(L12SkiffText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0090 + 0x30 * 11, Convert.ToInt32(L12DatapakText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0098 + 0x30 * 11, Convert.ToInt32(L12TimeText.Text, 10));
                WriteFloatToFile(SaveOffset + 0x00B0 + 0x30 * 11, Convert.ToSingle(L12HPLostText.Text));

                WriteIntToFile(SaveOffset + 0x0088 + 0x30 * 12, Convert.ToInt32(L13ChestText.Text, 10));
                WriteIntToFile(SaveOffset + 0x008C + 0x30 * 12, Convert.ToInt32(L13SkiffText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0090 + 0x30 * 12, Convert.ToInt32(L13DatapakText.Text, 10));
                WriteIntToFile(SaveOffset + 0x0098 + 0x30 * 12, Convert.ToInt32(L13TimeText.Text, 10));
                WriteFloatToFile(SaveOffset + 0x00B0 + 0x30 * 12, Convert.ToSingle(L13HPLostText.Text));

                // Write Collectable Flags
                WriteIntToFile(SaveOffset + 0x09E8, Convert.ToInt32(DatapakFlags.Text, 16));
                WriteIntToFile(SaveOffset + 0x09EC, Convert.ToInt32(SkiffFlags.Text, 16));
                WriteIntToFile(SaveOffset + 0x0A14, Convert.ToInt32(L1ChestFlags.Text, 16));
                WriteIntToFile(SaveOffset + 0x0A18, Convert.ToInt32(L2ChestFlags.Text, 16));
                WriteIntToFile(SaveOffset + 0x0A1C, Convert.ToInt32(L3ChestFlags.Text, 16));
                WriteIntToFile(SaveOffset + 0x0A20, Convert.ToInt32(L4ChestFlags.Text, 16));
                WriteIntToFile(SaveOffset + 0x0A24, Convert.ToInt32(L5ChestFlags.Text, 16));
                WriteIntToFile(SaveOffset + 0x0A28, Convert.ToInt32(L6ChestFlags.Text, 16));
                WriteIntToFile(SaveOffset + 0x0A2C, Convert.ToInt32(L8ChestFlags.Text, 16));
                WriteIntToFile(SaveOffset + 0x0A30, Convert.ToInt32(L9ChestFlags.Text, 16));
                WriteIntToFile(SaveOffset + 0x0A34, Convert.ToInt32(L10ChestFlags.Text, 16));
                WriteIntToFile(SaveOffset + 0x0A38, Convert.ToInt32(L11ChestFlags.Text, 16));
                WriteIntToFile(SaveOffset + 0x0A3C, Convert.ToInt32(L12ChestFlags.Text, 16));

                MessageBox.Show("Save complete!", "Successful", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Save file is not writable!?", "Error", MessageBoxButtons.OK);
            }
        }

        private int ReadIntFromFile(long offset_from_beginning)
        {
            return BitConverter.ToInt32(ReadBytesFromFile(offset_from_beginning, 4), 0);
        }

        private void WriteIntToFile(long offset_from_beginning, int value)
        {
            byte[] temp = BitConverter.GetBytes(value);
            WriteBytesToFile(offset_from_beginning, temp, 4);
        }

        private float ReadFloatFromFile(long offset_from_beginning)
        {
            return BitConverter.ToSingle(ReadBytesFromFile(offset_from_beginning, 4), 0);
        }

        private void WriteFloatToFile(long offset_from_beginning, float value)
        {
            byte[] temp = BitConverter.GetBytes(value);
            WriteBytesToFile(offset_from_beginning, temp, 4);
        }

        private byte[] ReadBytesFromFile(long offset_from_beginning, int num_bytes)
        {
            byte[] tmpint = new byte[4];
            long ret = SaveFile.Seek(offset_from_beginning, SeekOrigin.Begin);
            SaveFile.Read(tmpint, 0, num_bytes);
            if (BitConverter.IsLittleEndian) Array.Reverse(tmpint);
            return tmpint;
        }

        private void WriteBytesToFile(long offset_from_beginning, byte[] value, int num_bytes = 0)
        {
            byte[] temp = value;
            int length = num_bytes;
            if (length == 0) length = temp.Length;
            long ret = SaveFile.Seek(offset_from_beginning, SeekOrigin.Begin);
            if (BitConverter.IsLittleEndian) Array.Reverse(temp);
            SaveFile.Write(temp, 0, length);
        }

        // Type: 0 - Chest, 1 - Datapak, 2 - Skiff
        private void UnlockCollectables(int Level, int Type)
        {
            int merged = (Level << 8) + Type;
            switch(merged) {
                case 0x0100: { L1ChestText.Text = "14";  L1ChestFlags.Text = "3FFF";   break; }
                case 0x0200: { L2ChestText.Text = "12";  L2ChestFlags.Text = "FFF";    break; } // 7FF
                case 0x0300: { L3ChestText.Text = "9";   L3ChestFlags.Text = "1FF";    break; } // FF
                case 0x0400: { L4ChestText.Text = "17";  L4ChestFlags.Text = "1FFFF";  break; }
                case 0x0500: { L5ChestText.Text = "17";  L5ChestFlags.Text = "1FFFF";  break; }
                case 0x0600: { L6ChestText.Text = "6";   L6ChestFlags.Text = "3F";     break; } // 1F
                case 0x0800: { L8ChestText.Text = "10";  L8ChestFlags.Text = "3FF";    break; }
                case 0x0900: { L9ChestText.Text = "21";  L9ChestFlags.Text = "1FFFFF"; break; }
                case 0x0A00: { L10ChestText.Text = "17"; L10ChestFlags.Text = "1FFF";  break; }
                case 0x0B00: { L11ChestText.Text = "7";  L11ChestFlags.Text = "7F";    break; }
                case 0x0C00: { L12ChestText.Text = "12"; L12ChestFlags.Text = "FFF";   break; }
                
                case 0x0201: { L2DatapakText.Text = "2"; DatapakFlags.Text = String.Format("{0:X}", (Convert.ToInt32(DatapakFlags.Text, 16) | 0x00003)); break; }
                case 0x0301: { L3DatapakText.Text = "2"; DatapakFlags.Text = String.Format("{0:X}", (Convert.ToInt32(DatapakFlags.Text, 16) | 0x0000C)); break; }
                case 0x0401: { L4DatapakText.Text = "2"; DatapakFlags.Text = String.Format("{0:X}", (Convert.ToInt32(DatapakFlags.Text, 16) | 0x00030)); break; }
                case 0x0501: { L5DatapakText.Text = "2"; DatapakFlags.Text = String.Format("{0:X}", (Convert.ToInt32(DatapakFlags.Text, 16) | 0x000C0)); break; }
                case 0x0601: { L6DatapakText.Text = "2"; DatapakFlags.Text = String.Format("{0:X}", (Convert.ToInt32(DatapakFlags.Text, 16) | 0x00300)); break; }
                case 0x0801: { L8DatapakText.Text = "2"; DatapakFlags.Text = String.Format("{0:X}", (Convert.ToInt32(DatapakFlags.Text, 16) | 0x00C00)); break; }
                case 0x0901: { L9DatapakText.Text = "2"; DatapakFlags.Text = String.Format("{0:X}", (Convert.ToInt32(DatapakFlags.Text, 16) | 0x03000)); break; }
                case 0x0A01: { L10DatapakText.Text = "2"; DatapakFlags.Text = String.Format("{0:X}", (Convert.ToInt32(DatapakFlags.Text, 16) | 0x0C000)); break; }
                case 0x0B01: { L11DatapakText.Text = "2"; DatapakFlags.Text = String.Format("{0:X}", (Convert.ToInt32(DatapakFlags.Text, 16) | 0x30000)); break; }
                case 0x0C01: { L12DatapakText.Text = "2"; DatapakFlags.Text = String.Format("{0:X}", (Convert.ToInt32(DatapakFlags.Text, 16) | 0xC0000)); break; }

                case 0x0202: { L2SkiffText.Text = "2"; SkiffFlags.Text = String.Format("{0:X}", (Convert.ToInt32(SkiffFlags.Text, 16) | 0x00003)); break; }
                case 0x0302: { L3SkiffText.Text = "2"; SkiffFlags.Text = String.Format("{0:X}", (Convert.ToInt32(SkiffFlags.Text, 16) | 0x0000C)); break; }
                case 0x0402: { L4SkiffText.Text = "2"; SkiffFlags.Text = String.Format("{0:X}", (Convert.ToInt32(SkiffFlags.Text, 16) | 0x00030)); break; }
                case 0x0502: { L5SkiffText.Text = "2"; SkiffFlags.Text = String.Format("{0:X}", (Convert.ToInt32(SkiffFlags.Text, 16) | 0x000C0)); break; }
                case 0x0602: { L6SkiffText.Text = "2"; SkiffFlags.Text = String.Format("{0:X}", (Convert.ToInt32(SkiffFlags.Text, 16) | 0x00300)); break; }
                case 0x0802: { L8SkiffText.Text = "2"; SkiffFlags.Text = String.Format("{0:X}", (Convert.ToInt32(SkiffFlags.Text, 16) | 0x00C00)); break; }
                case 0x0902: { L9SkiffText.Text = "2"; SkiffFlags.Text = String.Format("{0:X}", (Convert.ToInt32(SkiffFlags.Text, 16) | 0x03000)); break; }
                case 0x0A02: { L10SkiffText.Text = "2"; SkiffFlags.Text = String.Format("{0:X}", (Convert.ToInt32(SkiffFlags.Text, 16) | 0x0C000)); break; }
                case 0x0B02: { L11SkiffText.Text = "2"; SkiffFlags.Text = String.Format("{0:X}", (Convert.ToInt32(SkiffFlags.Text, 16) | 0x30000)); break; }
                case 0x0C02: { L12SkiffText.Text = "2"; SkiffFlags.Text = String.Format("{0:X}", (Convert.ToInt32(SkiffFlags.Text, 16) | 0xC0000)); break; }
                
                default: break;
            }
        }
        
        private void L1UnlockButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                UnlockCollectables(1, i);
        }

        private void L2UnlockButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                UnlockCollectables(2, i);
        }

        private void L3UnlockButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                UnlockCollectables(3, i);
        }

        private void L4UnlockButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                UnlockCollectables(4, i);
        }

        private void L5UnlockButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                UnlockCollectables(5, i);
        }

        private void L6UnlockButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                UnlockCollectables(6, i);
        }

        private void L8UnlockButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                UnlockCollectables(8, i);
        }

        private void L9UnlockButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                UnlockCollectables(9, i);
        }

        private void L10UnlockButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                UnlockCollectables(10, i);
        }

        private void L11UnlockButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                UnlockCollectables(11, i);
        }

        private void L12UnlockButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                UnlockCollectables(12, i);
        }

        private void UnlockDPButton_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 13; i++)
                UnlockCollectables(i, 1);
        }

        private void UnlockSkiffButton_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 13; i++)
                UnlockCollectables(i, 2);
        }

        private void UnlockAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 13; i++)
                for(int j = 0; j < 3; j++)
                    UnlockCollectables(i, j);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateSaveFile();
            }
            catch (Exception exp)
            {
                MessageBox.Show("An error occurred while attempting to update the file. The error is:"
                                + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
            }
        }

        private void HPMaxButton_Click(object sender, EventArgs e)
        {
            HPText.Text = "4F000000";
        }

        private void StaminaMaxButton_Click(object sender, EventArgs e)
        {
            StaminaText.Text = "4F000000";
        }


    }
}
