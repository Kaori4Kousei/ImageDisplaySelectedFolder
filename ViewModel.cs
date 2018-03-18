using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageGallery
{
    public class ViewModel : INotifyPropertyChanged
    {
        public bool Opened;
        
        private string _filePath;

        public string FilePaths
        {
            get
            {
                return _filePath;
            }

            set
            {
                if (value == _filePath)
                {
                    return;
                }
                _filePath = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand BrowseCommand => new RelayCommand(
      () =>
      {
          FilePaths = Browse_Click();
          if (FilePaths!=null)
          LoadingPath();
      });

        public MainWindow windows;
        public ViewModel(MainWindow window)
        {
            windows = window;
            _imagePath = new ObservableCollection<ImagesPath>();
        }

        private readonly ObservableCollection<ImagesPath> _imagePath;

        public string Browse_Click()
        {
            var dlg = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dlg.ShowDialog(windows.GetIWin32Window());
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                return dlg.SelectedPath;
            }

            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<ImagesPath> ImagePath
        {
            get { return _imagePath; }
        }

        private void LoadingPath()
        {
            if (Opened != true)
            {
                string[] fileEntries = Directory.GetFiles(FilePaths);
                foreach (string fileName in fileEntries)
                    ImagePath.Add(new ImagesPath() { ImagePathString = fileName });
                Opened = true;
            }
            else
            {
                foreach (var itemToRemove in ImagePath.ToList())
                {
                    ImagePath.Remove(itemToRemove);
                }
                string[] fileEntries = Directory.GetFiles(FilePaths);
                foreach (string fileName in fileEntries)
                    ImagePath.Add(new ImagesPath() { ImagePathString = fileName });
            }
        }
    }

}
