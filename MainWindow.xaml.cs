using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaFileEditor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			tbFilePath.Text = @"D:\Siva\Personal\Songs\Selected\Telugu";
		}

		// test
		private void UpdateMediaFiles()
		{
			try
			{
				var files = System.IO.Directory.GetFiles(tbFilePath.Text, "*.mp3", SearchOption.AllDirectories);

				TagLib.File tagFile;
				foreach (var file in files)
				{
					try
					{
						lblStatus.Content = file;

						tagFile = TagLib.File.Create(file);

						// Title/ Comments/ Contributing artists
						string changeName;

						// Title
						if (!string.IsNullOrEmpty(tagFile.Tag.Title))
						{
							changeName = this.GetChangedName(tagFile.Tag.Title);
							if (!changeName.Equals(tagFile.Tag.Title))
							{
								tagFile.Tag.Title = changeName;
								tagFile.Save();
							}
						}

						// Comments
						if (!string.IsNullOrEmpty(tagFile.Tag.Comment))
						{
							changeName = this.GetChangedName(tagFile.Tag.Comment);
							if (!changeName.Equals(tagFile.Tag.Comment))
							{
								tagFile.Tag.Comment = changeName;
								tagFile.Save();
							}
						}

						// Album
						if (!string.IsNullOrEmpty(tagFile.Tag.Album))
						{
							changeName = this.GetChangedName(tagFile.Tag.Album);
							if (!changeName.Equals(tagFile.Tag.Album))
							{
								tagFile.Tag.Album = changeName;
								tagFile.Save();
							}
						}

						// Description
						if (!string.IsNullOrEmpty(tagFile.Tag.Description))
						{
							changeName = this.GetChangedName(tagFile.Tag.Description);
							if (!changeName.Equals(tagFile.Tag.Description))
							{
								tagFile.Tag.Description = changeName;
								tagFile.Save();
							}
						}

						FileInfo fileInfo = new FileInfo(file);
						changeName = this.GetChangedName(fileInfo.Name);
						if (!changeName.Equals(fileInfo.Name))
						{
							fileInfo.MoveTo(fileInfo.DirectoryName + "\\" + changeName);
						}
					}
					catch { }
				}
			}
			catch(Exception ex)
			{
				lblStatus.Content = ex.Message;
			}
		}


		private string GetChangedName(string value)
		{
			
			if (!string.IsNullOrEmpty(value))
			{
				string[] names = { "[iSongs.info] 01 - ", "[iSongs.info] 02 - ", "[iSongs.info] 03 - ", "[iSongs.info] 04 - ",
				"[iSongs.info] 05 - ", "[iSongs.info] 06 - ", "[iSongs.info] 07 - ", "[iSongs.info] "," [iSongs.info]", "[iSongs.info]",
				" [www.AtoZmp3.in]", "[www.AtoZmp3.in] " , "https://iSongs.info", "http://iSongs.info", "mp3songsdownloadsfree.blogspot.com",
				"[www.Rockmp3.in]","www.Rockmp3.in","[Gyanguru.org]","[DoReGaMa]","[Southmp3.Org]","Southmp3.Org","DoReGaMa.com","SouthMp3.Net",
				"www.AtoZmp3.blogspot.com.mp3","www.AtoZmp3.blogspot.com","www.TaazaSongs.Co.in","www.AtoZmp3.net","MusicRaaga.Co.In","(www.Southmp3.org)",
				"www.AtoZmp3.net","|- www.DoReGaMa.in -|","Its All About Quality","www.DesiMucom","MusicRaaga.Net","[MusicRaaga]","[Team SpicyDen]","Team SpicyDen",
				"[SouthMpOrg]","[Southmporg]","Ripped And Uploaded By","SouthMpOrg","SouthMp3.Org","[SouthMp3.Org]","SongsPK.cc","(www.songs.pk)","www.Songs.PK",
				"[","]",
				"01 - ","02 - ","03 - ","04 - ","05 - ","06 - ","07 - ","08 - ","09 - ","10 - ","01.","02.","03.","04.","05.","06.","07.","1.","2.","3.",
				"4.","5.","6.","7.","8.","9.","10.","-","_"};

				foreach (var name in names)
				{
					if (value.Contains(name, StringComparison.CurrentCultureIgnoreCase))
					{
						value = value.Replace(name, string.Empty);
					}
				}

				return value.Trim();
			}

			return value;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				btnSubmit.IsEnabled = false;
				lblStatus.Content = string.Empty;
				this.UpdateMediaFiles();
				lblStatus.Content = "Completed !";
				btnSubmit.IsEnabled = true;
			}
			catch (Exception ex)
			{
				lblStatus.Content = ex.Message;
			}
		}
	}
}
