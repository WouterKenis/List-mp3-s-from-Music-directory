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
                var splitted = musicFile.Split('\\');

                MusicFile mf = new MusicFile();
                mf.FileName = splitted[splitted.Length - 1];
                mf.Year = file.Tag.Year;

                if (file.Tag.FirstAlbumArtist == null)
                    mf.Artist = "unknown";
                else
                    mf.Artist = file.Tag.FirstAlbumArtist;

                if (file.Tag.FirstGenre == null)
                    mf.Genre = "unknown";
                else
                    mf.Genre = file.Tag.FirstGenre;

                if (file.Tag.Title == null)
                    mf.Title = "unknown";
                else
                    mf.Title = file.Tag.Title;

                musicFiles.Add(mf);
            }

            musicFileDataGrid.ItemsSource = musicFiles;
        }

        public string CustomTitle { get; set; }
    }
}
