using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using NAudio.Wave;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                BinaryReader br = new BinaryReader(File.OpenRead(ofd.FileName));
                byte[] header = br.ReadBytes(4);
                Int16 o = br.ReadInt16();
                Int16 samplewidth = br.ReadInt16();
                Int16 numchanel = br.ReadInt16();
                Int16 word1 = br.ReadInt16();
                Int32 framerate = br.ReadInt32();
                Int32 numframes = br.ReadInt32();

                byte[] framedata = br.ReadBytes((samplewidth / 8) * numframes * numchanel);

                var ms = new MemoryStream(framedata);
                var rs = new RawSourceWaveStream(ms, new WaveFormat(framerate, 16, 1));

                var wo = new WaveOutEvent();
                wo.Init(rs);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(500);
                }
                wo.Dispose();

            }
        }
    }
}
