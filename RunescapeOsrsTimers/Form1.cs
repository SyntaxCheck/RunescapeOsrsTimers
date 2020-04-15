using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunescapeOsrsTimers
{
    public partial class Form1 : Form
    {
        private const string serializeFilename = "TimerStorageValues.json";
        private const string soundFilename = "AlarmSound.wav";
        private StorageFile storage;
        private bool colorSwitch = true;
        private bool treeActive = false;
        private bool fruitTreeActive = false;
        private bool hardwoodActive = false;
        private bool herbActive = false;
        private bool birdhouseActive = false;
        private SystemSound sound = SystemSounds.Exclamation;
        private SoundPlayer customSound;

        public Form1()
        {
            InitializeComponent();
            Deserialize();
            timerMain.Start();

            if (File.Exists(soundFilename))
            {
                customSound = new SoundPlayer(soundFilename);
            }
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            colorSwitch = !colorSwitch;

            if (storage.TreeStart != default(DateTime))
            {
                TimeSpan elapsed = DateTime.Now - storage.TreeStart;
                TimeSpan remain = TimeSpan.FromMinutes(TreeMinutes()).Subtract(elapsed);
                if (remain.TotalSeconds < 0)
                {
                    remain = new TimeSpan(0, 0, 0);
                    if (treeActive)
                    {
                        treeActive = false;
                        PlaySound();
                    }
                }
                else
                {
                    treeActive = true;
                }
                SetText(lblTree, remain);
            }
            if (storage.FruitTreeStart != default(DateTime))
            {
                TimeSpan elapsed = DateTime.Now - storage.FruitTreeStart;
                TimeSpan remain = TimeSpan.FromMinutes(FruitTreeMinutes()).Subtract(elapsed);
                if (remain.TotalSeconds < 0)
                {
                    remain = new TimeSpan(0, 0, 0);
                    if (fruitTreeActive)
                    {
                        fruitTreeActive = false;
                        PlaySound();
                    }
                }
                else
                {
                    fruitTreeActive = true;
                }
                SetText(lblFruit, remain);
            }
            if (storage.HerbStart != default(DateTime))
            {
                TimeSpan elapsed = DateTime.Now - storage.HerbStart;
                TimeSpan remain = TimeSpan.FromMinutes(HerbMinutes()).Subtract(elapsed);
                if (remain.TotalSeconds < 0)
                {
                    remain = new TimeSpan(0, 0, 0);
                    if (herbActive)
                    {
                        herbActive = false;
                        PlaySound();
                    }
                }
                else
                {
                    herbActive = true;
                }
                SetText(lblHerb, remain);
            }
            if (storage.HardwoodStart != default(DateTime))
            {
                TimeSpan elapsed = DateTime.Now - storage.HardwoodStart;
                TimeSpan remain = TimeSpan.FromMinutes(HardwoodMinutes()).Subtract(elapsed);
                if (remain.TotalSeconds < 0)
                {
                    remain = new TimeSpan(0, 0, 0);
                    if (hardwoodActive)
                    {
                        hardwoodActive = false;
                        PlaySound();
                    }
                }
                else
                {
                    hardwoodActive = true;
                }
                SetText(lblHardwood, remain);
            }
            if (storage.BirdhouseStart != default(DateTime))
            {
                TimeSpan elapsed = DateTime.Now - storage.BirdhouseStart;
                TimeSpan remain = TimeSpan.FromMinutes(BirdhouseMinutes()).Subtract(elapsed);
                if (remain.TotalSeconds < 0)
                {
                    remain = new TimeSpan(0, 0, 0);
                    if (birdhouseActive)
                    {
                        birdhouseActive = false;
                        PlaySound();
                    }
                }
                else
                {
                    birdhouseActive = true;
                }
                SetText(lblBirdhouse, remain);
            }
            if (storage.ThroneMaxTime != default(DateTime))
            {
                TimeSpan elapsed = DateTime.Now - storage.ThroneMaxTime;
                int totalTicks = (int)(elapsed.TotalMinutes / ThroneMinutes());
                int remainTicks = (100 - totalTicks);
                lblThrone.Text = "Approval Rating: " + remainTicks.ToString() + "%";

                if (remainTicks > 90)
                    lblThrone.ForeColor = Color.Black;
                else
                    lblThrone.ForeColor = Color.Red;
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Serialize();
        }
        private void btnTree_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
            {
                lblTree.Visible = !lblTree.Visible;
            }
            else
            {
                if (String.IsNullOrEmpty(cbxTree.Text))
                {
                    MessageBox.Show("Must select a value", "Missing required value");
                }
                else
                {
                    storage.Tree = cbxTree.Text;
                    storage.TreeStart = DateTime.Now;
                }
            }
        }
        private void btnFruit_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
            {
                lblFruit.Visible = !lblFruit.Visible;
            }
            else
            {
                if (String.IsNullOrEmpty(cbxFruitTree.Text))
                {
                    MessageBox.Show("Must select a value", "Missing required value");
                }
                else
                {
                    storage.FruitTree = cbxFruitTree.Text;
                    storage.FruitTreeStart = DateTime.Now;
                }
            }
        }
        private void btnHardwood_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
            {
                lblHardwood.Visible = !lblHardwood.Visible;
            }
            else
            {
                if (String.IsNullOrEmpty(cbxHardwood.Text))
                {
                    MessageBox.Show("Must select a value", "Missing required value");
                }
                else
                {
                    storage.Hardwood = cbxHardwood.Text;
                    storage.HardwoodStart = DateTime.Now;
                }
            }
        }
        private void btnHerb_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
            {
                lblHerb.Visible = !lblHerb.Visible;
            }
            else
            {
                if (String.IsNullOrEmpty(cbxHerb.Text))
                {
                    MessageBox.Show("Must select a value", "Missing required value");
                }
                else
                {
                    storage.Herb = cbxHerb.Text;
                    storage.HerbStart = DateTime.Now;
                }
            }
        }
        private void btnBirdhouse_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
            {
                lblBirdhouse.Visible = !lblBirdhouse.Visible;
            }
            else
            {
                storage.BirdhouseStart = DateTime.Now;
            }
        }
        private void btnThrone_Click(object sender, EventArgs e)
        {
            if (ModifierKeys == Keys.Shift)
            {
                lblThrone.Visible = !lblThrone.Visible;
            }
            else
            {
                storage.ThroneMaxTime = DateTime.Now;
            }
        }
        private void cbxEnableAlarm_CheckedChanged(object sender, EventArgs e)
        {
            storage.PlaySound = cbxEnableAlarm.Checked;
        }

        //Private functions
        private int TreeMinutes()
        {
            int minutes = 0;

            switch (storage.Tree)
            {
                case "Oak":
                    minutes = 140;
                    break;
                case "Willow":
                    minutes = 220;
                    break;
                case "Maple":
                    minutes = 320;
                    break;
                case "Yew":
                    minutes = 400;
                    break;
                case "Magic":
                    minutes = 480;
                    break;
            }

            return minutes;
        }
        private int HardwoodMinutes()
        {
            int minutes = 0;

            switch (storage.Hardwood)
            {
                case "Teak":
                    minutes = 3920;
                    break;
                case "Mahogany":
                    minutes = 5120;
                    break;
            }

            return minutes;
        }
        private int FruitTreeMinutes()
        {
            return 960;
        }
        private int HerbMinutes()
        {
            return 80;
        }
        private int BirdhouseMinutes()
        {
            return 90;
        }
        private int ThroneMinutes()
        {
            return 1440;
        }
        private string TimeSpanToFormat(TimeSpan ts)
        {
            string formatted = String.Empty;

            if (ts.TotalSeconds <= 1)
                return "Finished!";

            if (ts.Days > 0)
                formatted += ts.Days.ToString() + " days ";
            if (ts.Hours > 0)
                formatted += ts.Hours.ToString() + " hours ";
            if (ts.Minutes > 0)
                formatted += ts.Minutes.ToString() + " minutes ";
            if (ts.Seconds > 0)
                formatted += ts.Seconds.ToString() + " seconds ";

            return formatted.Trim();
        }
        private void SetText(Label lbl, TimeSpan remaining)
        {
            lbl.Text = "Remaining: " + TimeSpanToFormat(remaining);
            if (remaining.TotalMinutes <= 0)
            {
                if (colorSwitch)
                    lbl.ForeColor = Color.Red;
                else
                    lbl.ForeColor = Color.Green;
            }
            else if (remaining.TotalMinutes < 5)
            {
                lbl.ForeColor = Color.Red;
            }
            else
            {
                lbl.ForeColor = Color.Black;
            }
        }
        private void PlaySound()
        {
            if (cbxEnableAlarm.Checked)
            {
                if (customSound != null)
                    customSound.Play();
                else
                    sound.Play();
            }
        }
        private void Serialize()
        {
            File.WriteAllText(serializeFilename, Newtonsoft.Json.JsonConvert.SerializeObject(storage));
        }
        private void Deserialize()
        {
            if (File.Exists(serializeFilename))
            {
                storage = Newtonsoft.Json.JsonConvert.DeserializeObject<StorageFile>(File.ReadAllText(serializeFilename));
                cbxTree.Text = storage.Tree;
                cbxFruitTree.Text = storage.FruitTree;
                cbxHardwood.Text = storage.Hardwood;
                cbxHerb.Text = storage.Herb;
                cbxEnableAlarm.Checked = storage.PlaySound;
            }
            else
            {
                storage = new StorageFile();
            }
        }
    }
}
