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

            //play button
            this.play = new NoFocusCueButton
            {
                Width = 50,
                Height = 50,
                Image = MusicPlayer.Properties.Resources.Play
            };

            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(3, 3, play.Width - 7, play.Height - 7);
            play.Region = new Region(p);
            play.Location = new Point(this.Width / 2 - 30, this.Height - 100);

            play.Click += new EventHandler(play_Click);     //add event handler

            //songs
            this.songsList = new ListBox
            {
                ItemHeight = 20,
                Location = new Point(17, 10),
                Size = new Size(this.Width - 50, this.Height - 150)
            };

            songsList.SelectedIndexChanged += new EventHandler(song_Changed);

            //adding controls
            Controls.AddRange( new List<Control> { loadSongsFromFiles, loadSongsFromFolder, play, songsList }.ToArray());

            SuspendLayout();
            ResumeLayout(false);
        }

        private Button play;
        private ListBox songsList;
        private Button loadSongsFromFiles;
        private Button loadSongsFromFolder;

        #endregion
    }
}