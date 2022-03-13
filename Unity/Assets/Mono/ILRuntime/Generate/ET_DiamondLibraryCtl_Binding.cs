using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class ET_DiamondLibraryCtl_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(ET.DiamondLibraryCtl);

            field = type.GetField("DiamondLibrary", flag);
            app.RegisterCLRFieldGetter(field, get_DiamondLibrary_0);
            app.RegisterCLRFieldSetter(field, set_DiamondLibrary_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_DiamondLibrary_0, AssignFromStack_DiamondLibrary_0);


        }



        static object get_DiamondLibrary_0(ref object o)
        {
            return ((ET.DiamondLibraryCtl)o).DiamondLibrary;
        }

        static StackObject* CopyToStack_DiamondLibrary_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.DiamondLibraryCtl)o).DiamondLibrary;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_DiamondLibrary_0(ref object o, object v)
        {
            ((ET.DiamondLibraryCtl)o).DiamondLibrary = (ET.DiamondLibrary)v;
        }

        static StackObject* AssignFromStack_DiamondLibrary_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            ET.DiamondLibrary @DiamondLibrary = (ET.DiamondLibrary)typeof(ET.DiamondLibrary).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((ET.DiamondLibraryCtl)o).DiamondLibrary = @DiamondLibrary;
            return ptr_of_this_method;
        }



    }
}
