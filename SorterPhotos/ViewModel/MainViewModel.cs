using System;
using System.Collections.Generic;
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


        #region Files
        private string[] _files;
        public string[] FilesProp
        {
            get { return _files; }
            set { SetProperty(ref _files, value); }
        }
        #endregion


        #region Path
        private string? _path;
        public string? PathProp
        {
            get { return _path; }
            set { SetProperty(ref _path, value); }
        }
        #endregion

        #region OpenDirectory
        private DelegateCommand _openDirectory;
        public DelegateCommand OpenDirectory =>
            _openDirectory ?? (_openDirectory = new DelegateCommand(ExecuteOpenDirectory));

        void ExecuteOpenDirectory()
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PathProp = FBD.SelectedPath;
            }

        }
        #endregion
    }
}
