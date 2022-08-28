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
#if DEBUG
            PathJPEGProp = "G:\\29-08-2022\\ОТОБР\\JPEG";
            FilesJPEGProp = Directory.GetFiles(PathJPEGProp).ToList();
            LastActionProp = "Указан путь к директории с JPEG-файлами";
#endif
        }



        #region *
        private bool _isRotateImage;
        public bool IsRotateImageProp
        {
            get { return _isRotateImage; }
            set { SetProperty(ref _isRotateImage, value); }
        }
        #endregion




        #region *
        private string _lastAction;
        public string LastActionProp
        {
            get { return _lastAction; }
            set { SetProperty(ref _lastAction, value); }
        }
        #endregion



        #region FilesJPEG
        private List<string> _filesJPEG = new List<string>();
        public List<string> FilesJPEGProp
        {
            get { return _filesJPEG; }
            set { SetProperty(ref _filesJPEG, value); }
        }
        #endregion

        #region FilesCR2
        private List<string> _filesCR2 = new List<string>();
        public List<string> FilesCR2Prop
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
                FilesJPEGProp.Clear();
                PathCR2Prop = FBD.SelectedPath;
                LastActionProp = "Указан путь к директории с CR2-файлами";
            }
            else
            {
                LastActionProp = "Отменён выбор пути к директории с CR2-файлами";
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
                FilesJPEGProp.Clear();
                PathJPEGProp = FBD.SelectedPath;
                FilesJPEGProp = Directory.GetFiles(PathJPEGProp).ToList();
                LastActionProp = "Указан путь к директории с JPEG-файлами";
            }
            else
            {
                LastActionProp = "Отменён выбор пути к директории с JPEG-файлами";
            }
        }
        #endregion
    }
}
