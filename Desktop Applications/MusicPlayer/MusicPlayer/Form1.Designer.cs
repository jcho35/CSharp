using NAudio.Wave;
using System.Drawing.Drawing2D;

namespace MusicPlayer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "Form1";
            Text = "Music Player";

            //load songs from files button
            this.loadSongsFromFiles = new NoFocusCueButton
            {
                Size = new Size(100, 30),
                Text = "Select Songs...",
                TextAlign = ContentAlignment.MiddleCenter
            };

            loadSongsFromFiles.Location = new Point(17, this.Height - 125);
            loadSongsFromFiles.Click += new EventHandler(loadSongsFromFiles_Click);

            //load songs from files button
            this.loadSongsFromFolder = new NoFocusCueButton
            {
                Size = new Size(100, 30),
                Text = "Select Folder...",
                TextAlign = ContentAlignment.MiddleCenter
            };

            loadSongsFromFolder.Location = new Point(127, this.Height - 125);
            loadSongsFromFolder.Click += new EventHandler(loadSongsFromFolder_Click);

            #region Buttons

            //play button
            this.play = new NoFocusCueButton
            {
                Width = 60,
                Height = 60,
                Image = MusicPlayer.Properties.Resources.Play
            };

            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(3, 3, play.Width - 7, play.Height - 7);
            play.Region = new Region(p);
            play.Location = new Point(this.Width / 2 - 35, this.Height - 105);

            play.Click += new EventHandler(play_Click);     //add event handler

            //next button
            this.next = new NoFocusCueButton
            {
                Width = 46,
                Height = 44,
                Image = MusicPlayer.Properties.Resources.Next
            };

            GraphicsPath p2 = new GraphicsPath();
            p2.AddEllipse(3, 3, next.Width - 9, next.Height - 7);
            next.Region = new Region(p2);
            next.Location = new Point(this.Width / 2 + 20, this.Height - 95);

            next.Click += new EventHandler(next_Click);     //add event handler

            //prev button
            this.previous = new NoFocusCueButton
            {
                Width = 46,
                Height = 44,
                Image = MusicPlayer.Properties.Resources.Prev
            };

            GraphicsPath p3 = new GraphicsPath();
            p3.AddEllipse(3, 3, previous.Width - 9, previous.Height - 7);
            previous.Region = new Region(p3);
            previous.Location = new Point(this.Width / 2 - 75, this.Height - 95);

            previous.Click += new EventHandler(previous_Click);     //add event handler

            //random order button
            this.random = new NoFocusCueButton
            {
                Width = 43,
                Height = 43,
                Image = MusicPlayer.Properties.Resources.Random
            };

            GraphicsPath p4 = new GraphicsPath();
            p4.AddEllipse(3, 3, random.Width - 7, random.Height - 7);
            random.Region = new Region(p4);
            random.Location = new Point(this.Width / 2 - 120, this.Height - 95);

            random.Click += new EventHandler(random_Click);     //add event handler

            #endregion

            //songs
            this.songsList = new ListBox
            {
                ItemHeight = 20,
                Location = new Point(17, 10),
                Size = new Size(this.Width - 50, this.Height - 150)
            };

            songsList.SelectedIndexChanged += new EventHandler(song_Changed);

            //adding controls
            Controls.AddRange( new List<Control> { 
                loadSongsFromFiles, loadSongsFromFolder, 
                play, next, previous, random,
                songsList
            }.ToArray());

            waveOutDevice.PlaybackStopped += new EventHandler<StoppedEventArgs>(continue_Play);

            SuspendLayout();
            ResumeLayout(false);
        }

        private Button play;
        private Button next;
        private Button previous;
        private Button random;
        private ListBox songsList;
        private Button loadSongsFromFiles;
        private Button loadSongsFromFolder;

        #endregion
    }
}