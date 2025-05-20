using System.ComponentModel;

namespace GettingReal.ViewModel
{
    
    public class ProductViewModel : ViewModelBase
    {
        private string _selectedOption = string.Empty;
        public string SelectedOption
        {
            get => _selectedOption;
            set
            {
                if (_selectedOption != value)
                {
                    _selectedOption = value;
                    OnPropertyChanged(nameof(SelectedOption));
                }
            }
        }

        // Eksempel på knapperne med muligheder
        public string[] Options { get; } = { "Option1", "Option2", "Option3" };

        
    }
}