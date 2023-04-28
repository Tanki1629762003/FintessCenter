﻿using FitnessCenter.Core;
using FitnessCenter.Views.Windows.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Imaging;
using FitnessCenter.BD.EntitiesBD;

namespace FitnessCenter.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        //Для слайдера
        List<string> SliderImages = new List<string>();
        int SliderImagesIndex = 0;

        #region Accessors (helpers for ui design)

        #region HeaderText
        private string _headerText = "Главная";

        public string HeaderText
        {
            get => _headerText;

            set
            {
                if(_headerText != value)
                {
                    _headerText = value;
                    OnPropertyChanged(nameof(HeaderText));
                }
            }
        }
        #endregion

        #region SliderImage
        private string _sliderImage;

        public string SliderImage
        {
            get => _sliderImage;

            set
            {
                if (_sliderImage != value)
                {
                    _sliderImage = value;
                    OnPropertyChanged(nameof(SliderImage));
                }
            }
        }
        #endregion

        #region AbonementItems
        private List<Abonements> _abonementItems = new List<Abonements>
        {
            new Abonements(),
            new Abonements(),
            new Abonements(),
            new Abonements(),
        };

        public List<Abonements> AbonementItems
        {
            get => _abonementItems;

            set
            {
                if (_abonementItems != value)
                {
                    _abonementItems = value;
                    OnPropertyChanged(nameof(SliderImage));
                }
            }
        }
        #endregion

        #region ForMainVisibility
        private Visibility _forMainVisibility = Visibility.Visible;

        public Visibility ForMainVisibility
        {
            get => _forMainVisibility;

            set
            {
                if (_forMainVisibility != value)
                {
                    _forMainVisibility = value;
                    OnPropertyChanged(nameof(ForMainVisibility));
                }
            }
        }
        #endregion

        #region AbonementsCoreVisibility
        private Visibility _abonementsCoreVisibility = Visibility.Collapsed;

        public Visibility AbonementsCoreVisibility
        {
            get => _abonementsCoreVisibility;

            set
            {
                if (_abonementsCoreVisibility != value)
                {
                    _abonementsCoreVisibility = value;
                    OnPropertyChanged(nameof(AbonementsCoreVisibility));
                }
            }
        }
        #endregion

        #region AdminPanelVisibility
        private Visibility _adminPanelVisibility = Visibility.Collapsed;

        public Visibility AdminPanelVisibility
        {
            get => _adminPanelVisibility;

            set
            {
                if (_adminPanelVisibility != value)
                {
                    _adminPanelVisibility = value;
                    OnPropertyChanged(nameof(AdminPanelVisibility));
                }
            }
        }
        #endregion



        #endregion

        #region Commands

        #region LeftImageCpmmand
        public ICommand LeftImageCpmmand { get; }

        private bool CanLeftImageCommand(object p)
        {
            return SliderImages.Count > 0;
        }

        private void OnLeftImageCommand(object p)
        {
            if ((--SliderImagesIndex) < 0)
            {
                SliderImagesIndex = SliderImages.Count - 1;
            }

            SliderImage = SliderImages[SliderImagesIndex];
        }
        #endregion

        #region RightImageCpmmand
        public ICommand RightImageCpmmand { get; }

        private bool CanRightImageCommand(object p) => SliderImages.Count > 0;

        private void OnRightImageCommand(object p)
        {
            if ((++SliderImagesIndex) == (SliderImages.Count))
            {
                SliderImagesIndex = 0;
            }

            SliderImage = SliderImages[SliderImagesIndex];
        }
        #endregion

        #region ShowAbonementsCore
        public ICommand ShowAbonementsCore { get; }

        private bool CanShowAbonementsCoreCommand(object p) => true;

        private void OnShowAbonementsCoreCommand(object p)
        {
            AbonementsCoreVisibility = Visibility.Visible;
            ForMainVisibility = Visibility.Collapsed;
            AdminPanelVisibility = Visibility.Collapsed;
            HeaderText = "Абонементы";
        }
        #endregion

        #region ShowForMain
        public ICommand ShowForMain { get; }

        private bool CanShowForMainCommand(object p) => true;

        private void OnShowForMainCommand(object p)
        {
            AbonementsCoreVisibility = Visibility.Collapsed;
            ForMainVisibility = Visibility.Visible;
            AdminPanelVisibility = Visibility.Collapsed;
            HeaderText = "Главная";
        }
        #endregion

        #region ShowAdminPanel
        public ICommand ShowAdminPanel { get; }

        private bool CanShowAdminPanelCommand(object p) => true;

        private void OnShowAdminPanelCommand(object p)
        {
            AbonementsCoreVisibility = Visibility.Collapsed;
            ForMainVisibility = Visibility.Collapsed;
            AdminPanelVisibility = Visibility.Visible;
            HeaderText = "Главная";
        }
        #endregion

        #endregion

        public MainViewModel()
        {
            LeftImageCpmmand = new RelayCommand(OnLeftImageCommand, CanLeftImageCommand);
            RightImageCpmmand = new RelayCommand(OnRightImageCommand, CanRightImageCommand);
            ShowAbonementsCore = new RelayCommand(OnShowAbonementsCoreCommand, CanShowAbonementsCoreCommand);
            ShowForMain = new RelayCommand(OnShowForMainCommand, CanShowForMainCommand);
            ShowAdminPanel = new RelayCommand(OnShowAdminPanelCommand, CanShowAdminPanelCommand);

            //Найти словарь
            var imagesDict = Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source?.OriginalString == "/Resources/ImagesDictionary/Images.xaml");
            
            //Переписать с него данные
            if (imagesDict != null)
            {
                foreach (string key in imagesDict.Values)
                {
                    SliderImages.Add(key);
                    // обрабатываем каждый элемент в словаре
                }
            }

            if(SliderImages.Count > 0)
            SliderImage = SliderImages[0];
        }
    }
}
