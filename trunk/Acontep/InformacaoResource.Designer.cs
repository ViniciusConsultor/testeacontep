//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Acontep {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class InformacaoResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal InformacaoResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Acontep.InformacaoResource", typeof(InformacaoResource).Assembly);
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
        ///   Looks up a localized string similar to Tem certeza de que deseja realizar a exclusão..
        /// </summary>
        public static string ConfirmarOperacaoApagar {
            get {
                return ResourceManager.GetString("ConfirmarOperacaoApagar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Operação realizada com sucesso..
        /// </summary>
        public static string SucessoOperacao {
            get {
                return ResourceManager.GetString("SucessoOperacao", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A alteração {0} foi concluída com sucesso..
        /// </summary>
        public static string SucessoOperacaoAlterar {
            get {
                return ResourceManager.GetString("SucessoOperacaoAlterar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A inclusão {0} foi realizada com sucesso..
        /// </summary>
        public static string SucessoOperacaoIncluir {
            get {
                return ResourceManager.GetString("SucessoOperacaoIncluir", resourceCulture);
            }
        }
    }
}
