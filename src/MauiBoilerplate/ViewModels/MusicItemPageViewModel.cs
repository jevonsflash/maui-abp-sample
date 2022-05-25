using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Runtime.Validation.Interception;
using MauiBoilerplate.Core.Entities;
using MauiBoilerplate.Core.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MauiBoilerplate.ViewModels
{
    public class MusicItemPageViewModel : ViewModelBase
    {
        private readonly IIocResolver iocResolver;
        private readonly IRepository<Song, long> songRepository;
        private readonly IValidationConfiguration _configuration;

        public event EventHandler<List<ValidationResult>> OnValidateErrors;
        public event EventHandler OnFinished;

        public MusicItemPageViewModel(
            IIocResolver iocResolver,
            IRepository<Song, long> songRepository,
            IValidationConfiguration configuration)
        {
            this.CommitCommand=new Command(Commit, (o) => CurrentSong!=null);
            this.iocResolver=iocResolver;
            this.songRepository=songRepository;
            this._configuration=configuration;

            this.PropertyChanged+=MusicItemPageViewModel_PropertyChanged;
        }

        private void MusicItemPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName==nameof(CurrentSong))
            {
                CommitCommand.ChangeCanExecute();
            }
        }

        private void Commit(object obj)
        {
            var validateErrors = GetValidationErrors(this.CurrentSong);
            if (validateErrors.Count==0)
            {
                songRepository.InsertOrUpdate(currentSong);
                this.OnFinished?.Invoke(this, EventArgs.Empty);

            }
            else
            {
                OnValidateErrors?.Invoke(this, validateErrors);
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
        protected List<ValidationResult> GetValidationErrors(Song validatingObject)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();

            foreach (var validatorType in _configuration.Validators)
            {
                using (var validator = iocResolver.ResolveAsDisposable<IMethodParameterValidator>(validatorType))
                {
                    var validationResults = validator.Object.Validate(validatingObject);
                    validationErrors.AddRange(validationResults);
                }

            }
            return validationErrors;
        }



        public Command CommitCommand { get; set; }


    }
}
