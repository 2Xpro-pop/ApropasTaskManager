using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ApropasTaskManager.Shared.ViewModels
{
    [DataContract]
    public class CreateProjectViewModel: ReactiveObject
    {
        [Reactive, DataMember] public int Id { get; set; }
        [Reactive, DataMember] public string Name { get; set; }
        [Reactive, DataMember] public string Description { get; set; }
        [Reactive, DataMember] public int Priority { get; set; }
        [Reactive, DataMember] public string ProjectManagerId { get; set; }
    }
}
