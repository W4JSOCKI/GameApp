using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using IDeveloper = Wojtalak_Szczerkowski.GameApp.Interfaces.IDeveloper;
using System.ComponentModel.DataAnnotations;

namespace GameAppMAUI.ViewModels
{
    public partial class DeveloperViewModel : ObservableValidator, IDeveloper
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private int id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(2)]
        [Required]
        private string name;

        [ObservableProperty]
        [Required]
        private string country;


        public DeveloperViewModel(IDeveloper developer)
        {
            id = developer.Id;
            name = developer.Name;
            country = developer.Country;
        }
        public DeveloperViewModel()
        {
        }
    }
}
