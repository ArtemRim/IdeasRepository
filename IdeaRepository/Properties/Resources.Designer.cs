﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IdeaRepository.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("IdeaRepository.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to admin.
        /// </summary>
        internal static string Admin {
            get {
                return ResourceManager.GetString("Admin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This user have&apos;t ideas.
        /// </summary>
        internal static string IdeasNotFoundError {
            get {
                return ResourceManager.GetString("IdeasNotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User with such login already exist.
        /// </summary>
        internal static string LoginExistError {
            get {
                return ResourceManager.GetString("LoginExistError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Incorrect login or password.
        /// </summary>
        internal static string PasswordLoginError {
            get {
                return ResourceManager.GetString("PasswordLoginError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Request flag confirmation.
        /// </summary>
        internal static string RequestFlagConfirmedError {
            get {
                return ResourceManager.GetString("RequestFlagConfirmedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Request of idea.
        /// </summary>
        internal static string RequestIdeaError {
            get {
                return ResourceManager.GetString("RequestIdeaError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Request id of idea.
        /// </summary>
        internal static string RequestIdeaIdError {
            get {
                return ResourceManager.GetString("RequestIdeaIdError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Request text of idea.
        /// </summary>
        internal static string RequestTextIdeaError {
            get {
                return ResourceManager.GetString("RequestTextIdeaError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Request id of user.
        /// </summary>
        internal static string RequestUserIdError {
            get {
                return ResourceManager.GetString("RequestUserIdError", resourceCulture);
            }
        }
    }
}
