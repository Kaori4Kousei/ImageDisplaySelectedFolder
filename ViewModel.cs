using System;
using System.Collections.Generic;
using System.ComponentModel;
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
          Browse_Click();
      });

        public MainWindow windows;
        public ViewModel(MainWindow window)
        {
            windows = window;
        }

        void Browse_Click()
        {
            var dlg = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dlg.ShowDialog(windows.GetIWin32Window());
            string filename = dlg.SelectedPath;
            FilePaths = filename;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
