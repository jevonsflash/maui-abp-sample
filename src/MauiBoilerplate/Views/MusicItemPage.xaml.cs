using Abp.Dependency;
using MauiBoilerplate.Core.Entities;
using MauiBoilerplate.ViewModels;

namespace MauiBoilerplate;

public partial class MusicItemPage : ContentPageBase, ITransientDependency
{
    private readonly MusicItemPageViewModel musicItemPageViewModel;

    public MusicItemPage(MusicItemPageViewModel musicItemPageViewModel)
    {
        InitializeComponent();
        this.musicItemPageViewModel=musicItemPageViewModel;
        this.musicItemPageViewModel.OnValidateErrors+=MusicItemPageViewModel_OnValidateErrors;
        this.musicItemPageViewModel.OnFinished+=MusicItemPageViewModel_OnFinished;
        BindingContext=this.musicItemPageViewModel;
        Unloaded+=MusicItemPage_Unloaded;
    }

    private async void MusicItemPageViewModel_OnFinished(object sender, EventArgs e)
    {
       await this.Navigation.PopModalAsync();
    }

    private void MusicItemPage_Unloaded(object sender, EventArgs e)
    {
        musicItemPageViewModel.CurrentSong = null;
    }

    private async void MusicItemPageViewModel_OnValidateErrors(object sender, List<System.ComponentModel.DataAnnotations.ValidationResult> e)
    {
        var content = string.Join(',', e);
        await DisplayAlert("Çë×¢Òâ", content, "ºÃµÄ");
    }
}