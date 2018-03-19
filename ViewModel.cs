﻿using System;
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
        public bool canceled;
        
        private string _filePath;
        private int _selectedItemIndex;
        private string _selectedFileName;

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
                canceled = false;
            }

            else
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
                foreach (var fileName in fileEntries)
                {
                    {
                        if (System.IO.Path.GetExtension(fileName).ToLower() == ".jpg" || System.IO.Path.GetExtension(fileName).ToLower() == ".png" || System.IO.Path.GetExtension(fileName).ToLower() == ".jpeg")

                            ImagePath.Add(new ImagesPath() { ImagePathString = fileName });
                        Opened = true;
                    }
                }
                   
            }
            if(FilePaths==null)
            {
                foreach (var itemToRemove in ImagePath.ToList())
                {
                    ImagePath.Remove(itemToRemove);
                }
            }
            else
            {
                foreach (var itemToRemove in ImagePath.ToList())
                {
                    ImagePath.Remove(itemToRemove);
                }
                string[] fileEntries = Directory.GetFiles(FilePaths);
                foreach (string fileName in fileEntries)
                {
                    if (System.IO.Path.GetExtension(fileName).ToLower() == ".jpg" || System.IO.Path.GetExtension(fileName).ToLower() == ".png" || System.IO.Path.GetExtension(fileName).ToLower() == ".jpeg")

                        ImagePath.Add(new ImagesPath() { ImagePathString = fileName });
                    Opened = true;
                }
            }
        }

        public int  SelectedItemIndex
        {
            get => _selectedItemIndex;
            set
            {
                if (value == _selectedItemIndex)
                {
                    return;
                }

                _selectedItemIndex = value;
                ItemSelected();
                OnPropertyChanged();
            }
        }
        void ItemSelected()
        {
            SelectedFileName = ImagePath[_selectedItemIndex].ImagePathString;

            foreach (var itemToRemove in ImagePath.ToList())
            {
                ImagePath.Remove(itemToRemove);
            }
            ImagePath.Add(new ImagesPath() { ImagePathString = SelectedFileName });

        }

        public string SelectedFileName
        {
            get => _selectedFileName;
            set
            {
                if (value == _selectedFileName)
                {
                    return;
                }
                _selectedFileName = value;
                OnPropertyChanged();
            }
        }

    }

}
