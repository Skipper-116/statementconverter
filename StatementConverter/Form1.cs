using System;
using System.Windows.Forms;

namespace StatementConverter
{
    public partial class Form1 : Form
    {
        private string filePath;

        private string GetFilePath()
        {
            return filePath;
        }

        private void SetFilePath(string value)
        {
            filePath = value;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BrowseClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Browse bank statement to convert",
                DefaultExt = "csv",
                CheckFileExists = true,
                CheckPathExists = true,

                Filter = "csv files (*.csv)|*.csv",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
                SetFilePath(dialog.FileName);
                button2.Visible = true;
            } 
        }

        private void ConvertClick(object sender, EventArgs e)
        {
            //Read File and convert here
            Cursor.Current = Cursors.WaitCursor;
            var result = new Services.FileReader().ReadContent(GetFilePath());
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Title = "Save converted bank statement",
                CheckPathExists = true,
                DefaultExt = "csv",
                Filter = "csv files (*.csv)|*.csv",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            Cursor.Current = Cursors.Default;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                new Services.FileCreater().WriteContent(result, saveFileDialog1.FileName);
                textBox1.Text = string.Empty;
                button2.Visible = false;
                MessageBox.Show("File Saved Successfully", "Successful");
            }
        }
    }
}
