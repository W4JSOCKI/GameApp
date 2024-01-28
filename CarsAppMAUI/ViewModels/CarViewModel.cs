using Cars.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarsAppMAUI.ViewModels
{
    public partial class CarViewModel : ObservableValidator, ICar
    {

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage ="Id musi być nadane")]
        private int id;
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(2,ErrorMessage ="Nazwa musi mieć przynkfj oaih21")]
        private string? name;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage ="Rok produkcji musi być ")]
        [Range(1900,2024)]
        private int prodYear;
        [ObservableProperty]
        private int producerId;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [GreatherThan(nameof(Engine), ErrorMessage ="coś tam działa")]
        private int mileage;

        [ObservableProperty]
        private int engine;
        [ObservableProperty]
        [DefaultValue(0)]
        private TransmissionType transmission = TransmissionType.Manual;

        
        public CarViewModel(ICar car)
        {
            id = car.Id;
            name = car.Name;
            prodYear = car.ProdYear;
            producerId = car.ProducerId;
            mileage = car.Mileage;
            engine = car.Engine;
            transmission = car.Transmission;

            ErrorsChanged += CVM_ErrorsChanged;
            PropertyChanged += CVM_PropertyChanged;
        }

        ~CarViewModel()
        {
            ErrorsChanged -= CVM_ErrorsChanged;
            PropertyChanged -= CVM_PropertyChanged;
        }

        private void CarViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<string> AllTransmissions { get; } = Enum.GetNames(typeof(TransmissionType));

        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> Errors
        {
            get
            {
                //pobieramy wszystkie błędy
                var geterr = GetErrors(null);

                //przeszukujemy słownik żeby znaleźć właściwości, dla których już błędów nie ma..
                foreach (var kv in errors.ToList())
                {
                    if (geterr.All(r => r.MemberNames.All(a => a != kv.Key)))
                    {
                        errors.Remove(kv.Key);
                        
                    }
                }

                //wszystkie pozostałe błędy grupujemy
                var q = from ValidationResult e in geterr
                        from member in e.MemberNames
                        group e by member into gr
                        select gr;


                //tworzymy odpowiednie kolekcje w słowniku
                foreach (var prop in q)
                {
                    var messeges = prop.Select(r => r.ErrorMessage).ToList();
                    if (errors.ContainsKey(prop.Key))
                    {
                        errors.Remove(prop.Key);
                    }
                    errors.Add(prop.Key, messeges);
                    //RaiseErrorChanged(prop.Key);
                }
                //OnPropertyChanged(nameof(Errors));
                return errors;
            }
        }
        //=> string.Join( Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);

        public CarViewModel()
        {
            Transmission = TransmissionType.Manual;
        }

        private void CVM_PropertyChanged( object sender, PropertyChangedEventArgs e)
        {
            if ( e.PropertyName != nameof(HasErrors)) 
            { 
                OnPropertyChanged(nameof(HasErrors));
            }
        }

        private void CVM_ErrorsChanged( object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Errors));
            //OnPropertyChanged("Item[]");
        }

        /*public IEnumerable<string> NameErrors
        {
            get { return this["Name"]; }
        }*/

       // public Dictionary<string,IEnumerable<string>> ErrorsCol

       // [IndexerName("Item")]
        public IEnumerable<string> this[string propertyName]
        {
            get { return  from ValidationResult e in GetErrors(propertyName) select e.ErrorMessage; }

        }
    }


    public sealed class GreatherThanAttribute: ValidationAttribute
    {
        public string PropertyName { get; }
        public GreatherThanAttribute( string propertyName)
        {
            PropertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            object instance = validationContext.ObjectInstance;
            object otherValue = instance.GetType().GetProperty(PropertyName).GetValue(instance);

            if ( ((IComparable) value).CompareTo(otherValue) > 0)
            {
                return ValidationResult.Success;
            }
            
            

            return new ValidationResult(  $"nowa wartośc jest mniejsza niz ta {validationContext.MemberName} vs {PropertyName} ");
        }
    }

    public class ErrorsDictToSinglePropertyConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Dictionary<string, List<string>> errors = value as Dictionary<string, List<string>>;
            string propertyName = (string)parameter;
            if ( errors != null ) 
            { 
                if (errors.ContainsKey(propertyName))
                    return errors[propertyName];
                else
                    return null;
            }
            else return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ErrorsDictToBoolConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Dictionary<string, List<string>> errors = value as Dictionary<string, List<string>>;
            string propertyName = (string)parameter;

            if (( errors != null ) && errors.ContainsKey(propertyName) )
            {
                return errors[propertyName].ToList().Count > 0;
            }
            else return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
