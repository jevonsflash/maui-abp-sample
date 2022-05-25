using Abp.Domain.Repositories;
using MauiBoilerplate.Core.Entities;
using MauiBoilerplate.Core.ViewModel;
using System.Collections.ObjectModel;

namespace MauiBoilerplate.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IRepository<Song, long> songRepository;

        public MainPageViewModel(IRepository<Song, long> songRepository)
        {
            this.RefreshCommand=new Command(Refresh, (o) => true);
            this.DeleteCommand=new Command(Delete, (o) => true);
            this.songRepository=songRepository;

        }
        private void Delete(object obj)
        {
            songRepository.Delete(obj as Song);
        }
        private async void Refresh(object obj)
        {
            this.IsRefreshing=true;
            var getSongs = this.songRepository.GetAllListAsync();
            await getSongs.ContinueWith(r => IsRefreshing=false);
            var songs = await getSongs;
            this.Songs=new ObservableCollection<Song>(songs);
        }

        private ObservableCollection<Song> songs;

        public ObservableCollection<Song> Songs
        {
            get { return songs; }
            set
            {
                songs = value;
                RaisePropertyChanged();
            }
        }

        private Song currentSong;

        public Song CurrentSong
        {
            get { return currentSong; }
            set
            {
                currentSong = value;
                RaisePropertyChanged();
            }
        }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged();

            }
        }
        public Command RefreshCommand { get; set; }
        public Command DeleteCommand { get; private set; }
    }
}
