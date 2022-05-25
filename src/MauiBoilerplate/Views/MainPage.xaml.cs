using Abp.Dependency;
using MauiBoilerplate.Core.Entities;
using MauiBoilerplate.ViewModels;

namespace MauiBoilerplate;

public partial class MainPage : ContentPageBase, ITransientDependency
{
    private readonly MainPageViewModel mainPageViewModel;
    private readonly MusicItemPageViewModel musicItemPageViewModel;
    private readonly MusicItemPage musicItemPage;

    public MainPage(MainPageViewModel mainPageViewModel, MusicItemPageViewModel musicItemPageViewModel, MusicItemPage musicItemPage)
    {
        InitializeComponent();
        this.mainPageViewModel=mainPageViewModel;
        this.musicItemPageViewModel=musicItemPageViewModel;
        this.musicItemPage=musicItemPage;
        BindingContext=this.mainPageViewModel;
       
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        mainPageViewModel.RefreshCommand.Execute(null);

    }

    private async void SongMoreButton_OnClicked(object sender, EventArgs e)
    {
        var currentsong = (sender as BindableObject).BindingContext as Song;
        string action = await DisplayActionSheet(currentsong.MusicTitle, "取消", null, "修改", "删除");
        if (action=="修改")
        {

            musicItemPageViewModel.CurrentSong  = currentsong;
            await Navigation.PushModalAsync(musicItemPage);

        }
        else if (action=="删除")
        {
            mainPageViewModel.DeleteCommand.Execute(currentsong);
            mainPageViewModel.RefreshCommand.Execute(null);
        }

    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        musicItemPageViewModel.CurrentSong  = new Song();

        await Navigation.PushModalAsync(musicItemPage);
    }
}