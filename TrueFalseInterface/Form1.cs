using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrueFalseInterface
{
    public partial class Form1 : Form
    {
        TrueFalse currentData;
        bool fileCreated;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void toolStripMenuItem_New_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentData = new TrueFalse(saveFileDialog.FileName);
                currentData.Add(new Question());
                currentData.Save();
                NUDNumber.Value = 1;
                NUDNumber.Maximum = 1;
                NUDNumber.Minimum = 1;
                Update();
                fileCreated = true;
            }
        }
        private new void Update()
        {
            NUDNumber.Value = 1;
            textBox1.Text = currentData[(int)NUDNumber.Value - 1].Text;

        }

        private void toolStripMenuItem_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentData = new TrueFalse(openFileDialog.FileName);
                currentData.Load();
                NUDNumber.Value = 1;
                NUDNumber.Maximum = currentData.Count();
                NUDNumber.Minimum = 1;
                Update();
                fileCreated = true;
            }
        }

        private void toolStripMenuItem_Save_Click(object sender, EventArgs e)
        {
            if (currentData != null && fileCreated)
            {
                currentData.Save();
            }else if (!fileCreated)
            {
                OnUncreatedDatabaseReadTry();
            }
        }
        private void OnUncreatedDatabaseReadTry()
        {
            DialogResult dr = MessageBox.Show("База данных не создана! Создать базу данных?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dr == DialogResult.Yes)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentData = new TrueFalse(saveFileDialog.FileName);
                    currentData.Add(new Question());
                    currentData.Save();
                    NUDNumber.Value = 1;
                    NUDNumber.Maximum = 1;
                    NUDNumber.Minimum = 1;
                    Update();
                    fileCreated = true;
                }
            }
        }
        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (fileCreated)
            {
                currentData.Add(new Question("#" + (currentData.Count() + 1), true));
                NUDNumber.Maximum = currentData.Count();
                NUDNumber.Value = currentData.Count();
            }else
            {
                OnUncreatedDatabaseReadTry();
            }
            
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (fileCreated && currentData.Count() > 1)
            {
                currentData.Remove((int)NUDNumber.Value - 1);
                NUDNumber.Maximum--;
            }else if(currentData.Count() == 1)
            {
                MessageBox.Show("Все элементы удалены. Пишите с чистого листа!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                currentData[0].Text = "#1";
                textBox1.Text = currentData[0].Text;
                rbYes.Checked = true;
                rbNo.Checked = false;
                NUDNumber.Value = 1;
                NUDNumber.Maximum = 1;
            }
            else
            {
                OnUncreatedDatabaseReadTry();
            }
            

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (fileCreated)
            {
                currentData[(int)NUDNumber.Value - 1].Text = textBox1.Text;
                currentData[(int)NUDNumber.Value - 1].TrueFalse = rbYes.Checked;
            }
            else
            {
                OnUncreatedDatabaseReadTry();
            }
            
        }

        private void NUDNumber_ValueChanged(object sender, EventArgs e)
        {
            if (fileCreated)
            {
                textBox1.Text = currentData[(int)NUDNumber.Value - 1].Text;
                rbYes.Checked = currentData[(int)NUDNumber.Value - 1].TrueFalse;
                rbNo.Checked = !currentData[(int)NUDNumber.Value - 1].TrueFalse;
            } else
            {
                OnUncreatedDatabaseReadTry();
            }

        }

        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            if (fileCreated)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentData.FileName = saveFileDialog.FileName;
                    currentData.Save();
                }
            }
            else
            {
                OnUncreatedDatabaseReadTry();
            }
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            info infoForm = new info();
            infoForm.Show();
        }
    }

}
