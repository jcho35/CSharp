using NAudio.Wave;

namespace MusicPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Button utilities

        /// <summary>
        /// Custom button class to override outborder highlight of buttons
        /// </summary>
        public class NoFocusCueButton : Button
        {
            public NoFocusCueButton() : base()
            {
                this.SetStyle(ControlStyles.Selectable, false);
            }

            protected override bool ShowFocusCues
            {
                get
                {
                    return false;
                }
            }
        }

        //Not using default constructor because Event allows to stop music and not create multiple instances
        IWavePlayer waveOutDevice = new WaveOutEvent();
        AudioFileReader audioFileReader;
        private bool randomOrder = false;
        private int previousSong = -1;

        #endregion

        #region Player

        private void play_Click(object sender, EventArgs e)
        {
            if (songsList.SelectedItem != null)
            {
                play.Image = waveOutDevice.PlaybackState != PlaybackState.Playing
                    ? MusicPlayer.Properties.Resources.Pause
                    : MusicPlayer.Properties.Resources.Play;

                audioFileReader = new AudioFileReader(songsList.SelectedItem.ToString());

                if (waveOutDevice.PlaybackState != PlaybackState.Playing)
                {
                    if (waveOutDevice.PlaybackState == PlaybackState.Stopped)
                        waveOutDevice.Init(audioFileReader);

                    waveOutDevice.Play();
                }
                else
                {
                    waveOutDevice.Pause();
                    //audioFileReader.Dispose();
                    //waveOutDevice.Dispose();
                }
            }
        }

        private void continue_Play(object sender, EventArgs e)
        {
            if (audioFileReader.TotalTime.TotalSeconds - audioFileReader.CurrentTime.TotalSeconds < 1 ) 
                next_Click(sender, e);
        }

        private void next_Click(object sender, EventArgs e)
        {
            if (songsList.SelectedItem != null)
            {
                var newSongIndex = songsList.SelectedIndex + 1;

                if (randomOrder)
                {
                    var randomRange = new Random();
                    newSongIndex = randomRange.Next(1, songsList.Items.Count);

                    while (newSongIndex == songsList.SelectedIndex)
                    {
                        newSongIndex = randomRange.Next();
                    }
                }

                previousSong = songsList.SelectedIndex; //save previous song to keep order while random

                songsList.SelectedIndex = newSongIndex;
                audioFileReader = new AudioFileReader(songsList.SelectedItem.ToString());

                waveOutDevice.Play();
            }
        }

        private void previous_Click(object sender, EventArgs e)
        {
            if (songsList.SelectedItem != null)
            {
                songsList.SelectedIndex = previousSong == -1 ? songsList.SelectedIndex - 1 : previousSong;
                audioFileReader = new AudioFileReader(songsList.SelectedItem.ToString());

                waveOutDevice.Play();
            }
        }

        /// <summary>
        /// Random order button click
        /// Switches control variable and changes background color in the UI to show random active/disact.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void random_Click(object sender, EventArgs e)
        {
            randomOrder = !randomOrder;
            random.BackColor = !randomOrder ? Color.Empty : Color.LightGreen;
        }

        private void song_Changed(object sender, EventArgs e)
        {
            try
            {
                if (!audioFileReader.FileName.Equals(songsList.SelectedItem.ToString()))
                {
                    waveOutDevice.Stop();

                    play_Click(sender, e);
                }
            }
            catch (NullReferenceException)
            {
                if (audioFileReader == null) play_Click(sender, e);
                else MessageBox.Show("Selezionare una canzone.");
            }
        }

        #endregion

        #region Load Songs

        private void loadSongsFromFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<FileInfo> songs = new List<FileInfo>();
                foreach (var file in ofd.FileNames)
                {
                    songs.Add(new FileInfo(file));
                }

                //load songs from my PC playlist
                songsList.Items.Clear();
                songsList.Items.AddRange(songs.ToArray());
                songsList.DisplayMember = "Name";   //sets the text displayed for each element

                //need to stop song playing because I don't know if it will be in the new selection
                waveOutDevice.Stop();
                play.Image = MusicPlayer.Properties.Resources.Play;
            }
        }

        private void loadSongsFromFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryInfo dInfo = new DirectoryInfo(ofd.SelectedPath);
                FileInfo[] songs = dInfo.GetFiles("*.mp3");

                //load songs from my PC playlist
                songsList.Items.Clear();
                songsList.Items.AddRange(songs);
                songsList.DisplayMember = "Name";   //sets the text displayed for each element

                //need to stop song playing because I don't know if it will be in the new selection
                waveOutDevice.Stop();
                play.Image = MusicPlayer.Properties.Resources.Play;
            }
        }

        #endregion
    }
}