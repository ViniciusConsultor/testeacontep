using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;

namespace ExtendControls.WebControlColection
{
    class WebControlCustomEditor : CollectionEditor
    {
        public WebControlCustomEditor(Type type)
            : base(type)
        {
        }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        protected override Type CreateCollectionItemType()
        {
            return typeof(WebControlCustom);
        }
    }
}
