using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using MessageBox = System.Windows.MessageBox;

namespace SorterPhotos.ViewModel
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
            //string[] second = Directory.GetFiles
//#if DEBUG
//            PathJPEGProp = "G:\\29-08-2022\\ОТОБР\\JPEG";
//            FilesJPEGProp = Directory.GetFiles(PathJPEGProp).ToList();
//            LastActionProp = "Указан путь к директории с JPEG-файлами";
//#endif
        }


        //#region IsFillJPEGButton
        //private bool _isFillJPEGButton;
        //public bool IsFillJPEGButtonProp
        //{
        //    get { return _isFillJPEGButton; }
        //    set { SetProperty(ref _isFillJPEGButton, value); }
        //}
        //#endregion
        //#region IsFillCR2Button
        //private bool _isFillCR2Button;
        //public bool IsFillCR2ButtonProp
        //{
        //    get { return _isFillCR2Button; }
        //    set { SetProperty(ref _isFillCR2Button, value); }
        //}
        //#endregion
        //#region IsFillMoveButton
        //private bool _isFillMoveButton;
        //public bool IsFillMoveButtonProp
        //{
        //    get { return _isFillMoveButton; }
        //    set { SetProperty(ref _isFillMoveButton, value); }
        //}
        //#endregion


        #region IsSorting
        private bool _isSorting = false;
        public bool IsSortingProp
        {
            get { return _isSorting; }
            set
            {
                SetProperty(ref _isSorting, value);
                RaisePropertyChanged(nameof(TitleProp));
            }
        }
        #endregion
        #region Title
        public string TitleProp
        {
            get
            {
                if (IsSortingProp == false)
                {
                    return "Сортировка фотографий";
                }
                else
                {
                    return "В работе. Ожидайте завершения сортировки...";
                }
            }
        }
        #endregion


        #region LastAction
        private string _lastAction;
        public string LastActionProp
        {
            get { return _lastAction; }
            set { SetProperty(ref _lastAction, value); }
        }
        #endregion

        #region FilesJPEGUri
        private List<string> _filesJPEG = new List<string>();
        public List<string> FilesJPEGProp
        {
            get { return _filesJPEG; }
            set { SetProperty(ref _filesJPEG, value); }
        }
        #endregion
        #region FilesCR2Uri
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
        #region PathMove
        private string _pathMove;
        public string PathMoveProp
        {
            get { return _pathMove; }
            set { SetProperty(ref _pathMove, value); }
        }
        #endregion



        #region OpenDirectoryCR2
        private DelegateCommand _openDirectoryCR2;
        public DelegateCommand OpenDirectoryCR2 =>
            _openDirectoryCR2 ?? (_openDirectoryCR2 = new DelegateCommand(ExecuteOpenDirectoryCR2));

        void ExecuteOpenDirectoryCR2()
        {

            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.Description = "Выберите директорию с CR2-файлами";
            FBD.ShowNewFolderButton = false;
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FilesCR2Prop.Clear();
                PathCR2Prop = FBD.SelectedPath;

                List<string> _temp = new List<string>();
                foreach (var item in Directory.GetFiles(PathCR2Prop).ToList())
                {
                    if (item.ToLower().Contains(".cr2"))
                    {
                        _temp.Add(item);
                    }
                }
                FilesCR2Prop = _temp;

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
            FBD.Description = "Выберите директорию с JPG / JPEG-файлами";
            FBD.ShowNewFolderButton = false;
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FilesJPEGProp.Clear();
                PathJPEGProp = FBD.SelectedPath;
                List<string> _temp = new List<string>();
                foreach (var item in Directory.GetFiles(PathJPEGProp).ToList())
                {
                    if (item.ToLower().Contains(".jpeg") || item.ToLower().Contains(".jpg"))
                    {
                        _temp.Add(item);
                    }
                }
                FilesJPEGProp = _temp;
                LastActionProp = "Указан путь к директории с JPG / JPEG-файлами";
            }
            else
            {
                LastActionProp = "Отменён выбор пути к директории с JPG / JPEG-файлами";
            }
        }
        #endregion
        #region OpenDirectoryMove
        private DelegateCommand _openDirectoryMove;
        public DelegateCommand OpenDirectoryMove =>
            _openDirectoryMove ?? (_openDirectoryMove = new DelegateCommand(ExecuteOpenDirectoryMove));

        void ExecuteOpenDirectoryMove()
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PathMoveProp = FBD.SelectedPath;
                LastActionProp = "Указан путь к директории перемещения";
            }
            else
            {
                LastActionProp = "Отменён выбор пути к директории перемещения";
            }
        }
        #endregion
        #region Sorting
        private DelegateCommand _sorting;
        public DelegateCommand Sorting =>
            _sorting ?? (_sorting = new DelegateCommand(ExecuteSorting, CanExecuteSorting)
            .ObservesProperty(() => PathJPEGProp)
            .ObservesProperty(() => PathCR2Prop)
            .ObservesProperty(() => PathMoveProp));

        void ExecuteSorting()
        {
            if (FilesJPEGProp.Count > 0 && FilesCR2Prop.Count > 0)
            {
                StringBuilder errors = new StringBuilder();
                IsSortingProp = true;
                foreach (var item in FilesJPEGProp)
                {
                    string _name = item.Replace(PathJPEGProp+"\\", "").Split('.')[0];
                    try
                    {
                        File.Move(PathCR2Prop + "\\" + _name + ".CR2", PathMoveProp + "\\" + _name + ".CR2");
                        LastActionProp = $"Переносится файл: {_name}";
                    }
                    catch (Exception ex)
                    {
                        errors.AppendLine(ex.Message);
                        LastActionProp = ex.Message;
                    }
                }
                if (errors.Length != 0)
                {
                    MessageBox.Show(errors.ToString(), "Ошибки", MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            IsSortingProp = false;
            MessageBox.Show("Перенесение фотографий выполнено успешно!");
            LastActionProp = "Перенос завершён";
        }

        bool CanExecuteSorting()
        {
            return !String.IsNullOrEmpty(PathJPEGProp) &&
                !String.IsNullOrEmpty(PathCR2Prop) &&
                !String.IsNullOrEmpty(PathMoveProp) &&
                FilesCR2Prop.Count != 0 &&
                FilesJPEGProp.Count != 0;
        }
        #endregion
    }
}
