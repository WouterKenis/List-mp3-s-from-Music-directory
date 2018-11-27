using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace MusicGetter
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<MusicFile> musicFiles = new List<MusicFile>();

            string myMusicPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

            CustomTitle = "Listing music for " + myMusicPath;
            DataContext = this;

            string[] allMusicFiles = Directory.GetFiles(myMusicPath, "*.mp3*");

            foreach (string musicFile in allMusicFiles)
            {
                var file = TagLib.File.Create(musicFile);

                musicFiles.Add(new MusicFile
                {
                    Artist = file.Tag.FirstAlbumArtist,
                    Year = file.Tag.Year,
                    Genre = file.Tag.FirstGenre,
                    Title = file.Tag.Title
                });
            }

            musicFileDataGrid.ItemsSource = musicFiles;
        }

        public string CustomTitle { get; set; }
    }
}
