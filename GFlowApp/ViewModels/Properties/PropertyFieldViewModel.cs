using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GFlowApp.Services;

namespace GFlowApp.ViewModels.Properties
{
    public abstract partial class PropertyFieldViewModel : ObservableObject
    {
       
        public PropertySchema Schema { get; set; }


        protected PropertyFieldViewModel(PropertySchema schema)
        {
            Schema = schema;

        }

        public abstract JsonElement ToJsonElement();

        public abstract void LoadFromJson(JsonElement element);
    }
}