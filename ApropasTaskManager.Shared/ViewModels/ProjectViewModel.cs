using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ApropasTaskManager.Shared.ViewModels
{
    [DataContract]
    public class ProjectViewModel : ReactiveObject
    {
        [Reactive, DataMember] public string Name { get; set; }
        [Reactive, DataMember] public string Description { get; set; }
        [Reactive, DataMember] public IEnumerable<int> Missions { get; set; }
        [Reactive, DataMember] public IEnumerable<string> Users { get; set; }
    }
}
