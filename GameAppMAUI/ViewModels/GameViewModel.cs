using GameApp.Core;
using Wojtalak_Szczerkowski.GameApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace GameAppMAUI.ViewModels
{
    public partial class GameViewModel : ObservableValidator, IGame
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Id musi być nadane")]
        private int id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(4, ErrorMessage = "Tytuł musi mieć przynajmniej 4 znaki")]
        private string? title;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Pozycja w rankingu jest wymagany")]
        [Range(1, int.MaxValue)]
        private int rank;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Platforma jest wymagana")]
        private string? platform;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Rok wydania jest wymagany")]
        [Range(1950, 2024, ErrorMessage = "Rok wydania musi być pomiędzy 1950 a 2024")]
        private int releaseYear;

        [ObservableProperty]
        private IDeveloper developer;

        [ObservableProperty]
        private Genre gen;

     
        public GameViewModel(IGame game)
        {
            id = game.Id;
            title = game.Title;
            releaseYear = game.ReleaseYear;
            developer = game.Developer;
            gen = game.Gen;
            rank = game.Rank;
            platform = game.Platform;

            ErrorsChanged += GameViewModel_ErrorsChanged;
        }

        public GameViewModel()
        {

        }

        ~GameViewModel()
        {
            ErrorsChanged -= GameViewModel_ErrorsChanged;
        }

        private void GameViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Errors));
        }

        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> Errors
        {
            get
            {
                var getErrors = GetErrors(null);

                foreach (var key in errors.Keys.ToList())
                {
                    if (getErrors.All(result => result.MemberNames.All(member => member != key)))
                        errors.Remove(key);
                }

                var query = from ValidationResult result in getErrors
                            from member in result.MemberNames
                            group result by member into grp
                            select grp;

                foreach (var group in query)
                {
                    var messages = group.Select(result => result.ErrorMessage).ToList();
                    if (errors.ContainsKey(group.Key))
                        errors.Remove(group.Key);
                    errors.Add(group.Key, messages);
                }

                return errors;
            }
        }

        public IEnumerable<string> this[string propertyName]
        {
            get
            {
                return from ValidationResult result in GetErrors(propertyName)
                       select result.ErrorMessage;
            }
        }
    }
}
