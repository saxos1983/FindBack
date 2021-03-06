namespace FindBack.Touch.Views
{
	using MonoTouch.UIKit;
	using System.Collections.Generic;
	using Cirrious.MvvmCross.Binding.BindingContext;
	using Cirrious.MvvmCross.Touch.Views;
	using FindBack.Core.ViewModels;

	public partial class AddItemView : MvxViewController
	{
		public AddItemView ()
            : base("AddItemView", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NavigationItem.Title = "Add item";


			var s = new UIBarButtonItem () {
				Title = "Save"
			};

			this.AddBindings (new Dictionary<object, string> () {
				{ s, "Clicked SaveCommand" },
			});

			NavigationItem.SetRightBarButtonItem (s, false);


			var set = this.CreateBindingSet<AddItemView, AddItemViewModel> ();
			set.Bind (ItemText).To (item => item.ItemName);
			set.Bind (DescriptionText).To (item => item.Description);
			set.Bind (LongitudeLabel).To (item => item.Longitude).WithConversion ("LongitudeCoordinate");
			set.Bind (LatitudeLabel).To (item => item.Latitude).WithConversion ("LatitudeCoordinate");
			set.Bind (ItemImage).To (vm => vm.PictureBytes).WithConversion ("InMemoryImage");
			set.Apply ();

			//this.CreateBinding(TakePictureButton).To((AddItemViewModel item) => item.SaveCommand).Apply();
			this.AddBindings (new Dictionary<object, string> () {
 { TakePictureButton, "TouchUpInside TakePictureCommand" },
 { ChoosePictureButton, "TouchUpInside ChoosePictureCommand" },
			});

			var g = new UITapGestureRecognizer (() => {
				ItemText.ResignFirstResponder ();
				DescriptionText.ResignFirstResponder ();
			});

			View.AddGestureRecognizer (g);
		}
	}
}

