﻿using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace WinGoTag.ViewModel
{
    class ProfileViewModel : INotifyPropertyChanged
    {
        private bool _isbusy;
        private InstaMediaList _medlst;
        private InstaUserInfo _info;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsBusy
        {
            get { return _isbusy; }
            set { _isbusy = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsBusy")); }
        }
        public InstaUserInfo UserInfo
        {
            get { return _info; }
            set { _info = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserInfo")); }
        }
        public InstaMediaList MediaList
        {
            get { return _medlst; }
            set { _medlst = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MediaList")); }
        }

        public InstaMediaList GridList
        {
            get { return _medlst; }
            set { _medlst = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GridList")); }
        }

        CoreDispatcher Dispatcher { get; set; }
        public ProfileViewModel()
        {
            Dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            RunLoadPage();
        }

        async void RunLoadPage()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, LoadPage);
        }

        async void LoadPage()
        {
            var username = AppCore.InstaApi.GetLoggedUser().UserName;
            var user = await AppCore.InstaApi.GetUserInfoByUsernameAsync(username);
            UserInfo = user.Value;
            var media = await AppCore.InstaApi.GetUserMediaAsync(username, PaginationParameters.MaxPagesToLoad(1));
            MediaList = media.Value;
            GridList = media.Value;
        }
    }
}
