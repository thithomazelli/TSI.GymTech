﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TSI.GymTech.Entity.App_LocalResources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class GateStatusMessage {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GateStatusMessage() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TSI.GymTech.Entity.App_LocalResources.GateStatusMessage", typeof(GateStatusMessage).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome. Have a nice training.
        /// </summary>
        public static string AllowedEntry {
            get {
                return ResourceManager.GetString("AllowedEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Registration expires tomorrow.
        /// </summary>
        public static string AllowedEntryExpiresOneDay {
            get {
                return ResourceManager.GetString("AllowedEntryExpiresOneDay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Registration incomplete.
        /// </summary>
        public static string AllowedEntryIncomplete {
            get {
                return ResourceManager.GetString("AllowedEntryIncomplete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Registration will be expired in {0} days.
        /// </summary>
        public static string AllowedEntryWillBeExpired {
            get {
                return ResourceManager.GetString("AllowedEntryWillBeExpired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to See you in the next training.
        /// </summary>
        public static string AllowedExit {
            get {
                return ResourceManager.GetString("AllowedExit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your access was denied.
        /// </summary>
        public static string Denied {
            get {
                return ResourceManager.GetString("Denied", resourceCulture);
            }
        }
    }
}
