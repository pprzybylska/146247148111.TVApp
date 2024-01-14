using _146247148111.TVApp.ViewModel;

namespace _146247148111.TVApp.MAUI;

public partial class ProducersCatalogView : ContentPage
{
	public ProducersCatalogView( ProducerCollectionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}