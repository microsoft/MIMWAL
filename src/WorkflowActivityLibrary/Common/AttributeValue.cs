using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.ResourceManagement.WebServices;

namespace FIM.Workflow.Common
{
    class AttributeValue
    {
        #region Declarations

        private bool _isMultiValued = false;
        private Object _value = null;
        private Type _baseType = null;
        private List<Object> _objectList = new List<Object>();
        
        #endregion

        #region Properties

        public bool IsMultiValued { get { return _isMultiValued; } }
        public Object Value { get { return _value; } }
        public Type Basetype { get { return _baseType; } }
        public List<Object> ObjectList { get { return _objectList; } }

        #endregion

        #region Methods

        public AttributeValue(Object value)
        {
            if (value == null) return;

            //
            // FIM inconsistently returns reference values as either UniqueIdentifiers or Guids
            // To prevent repetitive checks, convert all UniqueIdentifiers to Guids,
            // and any List<UniqueIdentifier> to List<Guid>
            //
            if (value.GetType() == typeof(UniqueIdentifier)) value = ((UniqueIdentifier)value).GetGuid();
            else if (value.GetType() == typeof(List<UniqueIdentifier>))
            {
                List<Guid> guids = new List<Guid>();
                foreach (UniqueIdentifier id in (List<UniqueIdentifier>)value) guids.Add(((UniqueIdentifier)id).GetGuid());
                value = guids;
            }

            //
            // Determine if the value is a list and the base type
            //
            if (value.GetType().IsGenericType && value.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                _isMultiValued = true;
                foreach (Object o in (IEnumerable)value)
                {
                    _baseType = o.GetType();
                    break;
                }
            }
            else _baseType = value.GetType();
        }

        #endregion
    }
}
