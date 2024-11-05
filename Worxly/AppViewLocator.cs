using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Worxly.ViewModels;
using Worxly.Views;

namespace Worxly
{
    public class AppViewLocator : ReactiveUI.IViewLocator
    {
        public IViewFor ResolveView<T>(T? viewModel, string? contract = null) 
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));
            Type ViewType = Type.GetType(viewModel.GetType().FullName.Replace("ViewModel", "View"));
            if (ViewType != null)
            {
                IViewFor? view = Activator.CreateInstance(ViewType) as IViewFor;
                if (view == null)
                    throw new Exception($"View creation failed for {ViewType.FullName}");
                PropertyInfo? dataContextProperty = ViewType.GetProperty("DataContext");
                dataContextProperty?.SetValue(view, viewModel);
                return view;

            }
            throw new Exception($"View associated with ViewModel {viewModel.GetType().FullName} not found");
        }
    }
}
