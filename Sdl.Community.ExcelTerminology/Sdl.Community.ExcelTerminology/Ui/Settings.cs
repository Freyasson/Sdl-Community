﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sdl.Community.ExcelTerminology.Model;
using Sdl.Community.ExcelTerminology.Services;

namespace Sdl.Community.ExcelTerminology.Ui
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            sourceBox.Text = "A";
            targetBox.Text = "B";
            approvedBox.Text = "C";
        }

        private void defaultSettingsLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            var file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                filePath = file.FileName;
            }

            pathTextBox.Text = filePath;
        }

        private void sourceLanguageComboBox_Click(object sender, EventArgs e)
        {
            
            sourceLanguageComboBox.DataSource = GetCultureNames();
        }

        protected virtual List<string> GetCultureNames()
        {
            var cultureName = CultureInfo.GetCultures(CultureTypes.NeutralCultures).Select(ci => ci.DisplayName).ToList();
            return cultureName;
        }

        private void targetLanguageComboBox_Click(object sender, EventArgs e)
        {
            targetLanguageComboBox.DataSource = GetCultureNames();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            var provider = new ProviderSettings
            {
                HasHeader = hasHeader.Checked,
                ApprovedColumn = approvedBox.Text,
                SourceColumn = sourceBox.Text,
                TargetColumn = targetBox.Text,
                SourceLanguage = sourceLanguageComboBox.Text,
                TargetLanguage = targetLanguageComboBox.Text,
                Separator = separatorTextBox.Text,
                TermFilePath = pathTextBox.Text
            };
            
            if (string.IsNullOrWhiteSpace(provider.SourceLanguage) || string.IsNullOrWhiteSpace(provider.TargetLanguage) ||
                string.IsNullOrWhiteSpace(provider.TermFilePath) || string.IsNullOrWhiteSpace(provider.Separator))
            {
                MessageBox.Show(@"Please complete all fields", "", MessageBoxButtons.OK);

            }else if (!provider.TermFilePath.Contains("xlsx"))
            {
                MessageBox.Show(@"Please select an Excel file", "", MessageBoxButtons.OK);
            }
            else
            {
                var persistence = new PersistenceService();
                persistence.Save(provider);
            }
            
        }
    }
}
