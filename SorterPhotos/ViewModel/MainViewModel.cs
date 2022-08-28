using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace SorterPhotos.ViewModel
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
            //string[] second = Directory.GetFiles
        }


        #region *
        private string _lastAction;
        public string LastActionProp
        {
            get { return _lastAction; }
            set { SetProperty(ref _lastAction, value); }
        }
        #endregion



        #region Files
        private ObservableCollection<string> _filesJPEG;
        public ObservableCollection<string> FilesJPEGProp
        {
            get { return _filesJPEG; }
            set { SetProperty(ref _filesJPEG, value); }
        }
        #endregion

        #region Files
        private ObservableCollection<string> _filesCR2;
        public ObservableCollection<string> FilesCR2Prop
        {
            get { return _filesCR2; }
            set { SetProperty(ref _filesCR2, value); }
        }
        #endregion


        #region PathJPEG
        private string? _pathJPEG;
        public string? PathJPEGProp
        {
            get { return _pathJPEG; }
            set { SetProperty(ref _pathJPEG, value); }
        }
        #endregion


        #region PathCR2
        private string? _pathCR2;
        public string? PathCR2Prop
        {
            get { return _pathCR2; }
            set { SetProperty(ref _pathCR2, value); }
        }
        #endregion




        #region OpenDirectoryCR2
        private DelegateCommand _openDirectoryCR2;
        public DelegateCommand OpenDirectoryCR2 =>
            _openDirectoryCR2 ?? (_openDirectoryCR2 = new DelegateCommand(ExecuteOpenDirectoryCR2));

        void ExecuteOpenDirectoryCR2()
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PathCR2Prop = FBD.SelectedPath;
                LastActionProp = "Указан путь к директории с CR2-файлами";
            }
        }
        #endregion


        #region OpenDirectoryJPEG
        private DelegateCommand _openDirectoryJPEG;
        public DelegateCommand OpenDirectoryJPEG =>
            _openDirectoryJPEG ?? (_openDirectoryJPEG = new DelegateCommand(ExecuteOpenDirectoryJPEG));

        void ExecuteOpenDirectoryJPEG()
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PathJPEGProp = FBD.SelectedPath;
                FilesJPEGProp = new ObservableCollection<string>(Directory.GetFiles(PathJPEGProp).ToList());
                LastActionProp = "Указан путь к директории с JPEG-файлами";
            }
        }
        #endregion
    }
}
