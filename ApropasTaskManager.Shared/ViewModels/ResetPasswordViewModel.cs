using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;

namespace ApropasTaskManager.Shared.ViewModels
{
    [DataContract]
    public class ResetPasswordViewModel: ReactiveObject, IValidatableViewModel
    {
        public static readonly Regex Alphanumirec = new Regex("^(?=.*[a-zA-Z])(?=.*[0-9])", RegexOptions.Compiled);
        
        [Reactive, DataMember] public string OldPassword { get; set; }
        [Reactive, DataMember] public string ConfirmPassword { get; set; }
        [Reactive, DataMember] public string NewPassword { get; set; }

        [JsonIgnore]
        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public ResetPasswordViewModel()
        {
            this.ValidationRule(
                vm => vm.NewPassword,
                newPassword => !string.IsNullOrEmpty(newPassword) && newPassword.Length >= 8,
                "Password the must have at least 8 characters"
            );

            this.ValidationRule(
                vm => vm.NewPassword,
                newPassword => !string.IsNullOrEmpty(newPassword) && Alphanumirec.IsMatch(newPassword),
                "Password the must have contains number and character"
            );

            var confirmValid = this.WhenAnyValue(
                vm => vm.ConfirmPassword, 
                vm => vm.NewPassword, 
                (confirmPassword, newPassword) => confirmPassword == newPassword
            );

            this.ValidationRule(
                vm => vm.ConfirmPassword,
                confirmValid,
                "confirm not equeals" 
            );
        }
    }
}
