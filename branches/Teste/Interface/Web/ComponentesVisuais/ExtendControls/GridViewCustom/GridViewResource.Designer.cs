﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExtendControls.GridViewCustom {
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
    internal class GridViewResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GridViewResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ExtendControls.GridViewCustom.GridViewResource", typeof(GridViewResource).Assembly);
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
        ///   Looks up a localized string similar to .coluna
        ///{
        ///	visibility: hidden;
        ///	width: 0px;
        ///	position: absolute;
        ///}.
        /// </summary>
        internal static string css {
            get {
                return ResourceManager.GetString("css", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to function CheckAll(me,postback,start)
        ///{
        ///    flagAutoPostBack = true;
        ///    var index = me.name.indexOf(&apos;_HeaderButton&apos;);  
        ///    var prefix = me.name.substr(0,index);
        ///    prefix = prefix.replace(/_/g,&apos;$&apos;);
        ///    //prefix = Replace(prefix,&apos;$&apos;,&apos;_&apos;);
        ///    for(var i=0; i&lt;document.forms[0].length; i++) 
        ///    { 
        ///        var o = document.forms[0][i]; 
        ///        if (o.type == &apos;checkbox&apos;) 
        ///        { 
        ///            if (me.name != o.name) 
        ///            {
        ///                if (o.name.substring(0, prefix.length) == prefix [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string HotGridView {
            get {
                return ResourceManager.GetString("HotGridView", resourceCulture);
            }
        }
    }
}