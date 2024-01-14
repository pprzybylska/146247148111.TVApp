using _146247148111.TVApp.ViewModel;

namespace _146247148111.TVApp.MAUI;

public partial class CatalogTVView : ContentPage
{
	public CatalogTVView( TVCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}