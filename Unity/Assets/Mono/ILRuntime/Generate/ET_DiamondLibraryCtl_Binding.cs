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

            field = type.GetField("AllDiamondLibrary", flag);
            app.RegisterCLRFieldGetter(field, get_AllDiamondLibrary_0);
            app.RegisterCLRFieldSetter(field, set_AllDiamondLibrary_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_AllDiamondLibrary_0, AssignFromStack_AllDiamondLibrary_0);


        }



        static object get_AllDiamondLibrary_0(ref object o)
        {
            return ((ET.DiamondLibraryCtl)o).AllDiamondLibrary;
        }

        static StackObject* CopyToStack_AllDiamondLibrary_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.DiamondLibraryCtl)o).AllDiamondLibrary;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_AllDiamondLibrary_0(ref object o, object v)
        {
            ((ET.DiamondLibraryCtl)o).AllDiamondLibrary = (ET.AllDiamondLibrary)v;
        }

        static StackObject* AssignFromStack_AllDiamondLibrary_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            ET.AllDiamondLibrary @AllDiamondLibrary = (ET.AllDiamondLibrary)typeof(ET.AllDiamondLibrary).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((ET.DiamondLibraryCtl)o).AllDiamondLibrary = @AllDiamondLibrary;
            return ptr_of_this_method;
        }



    }
}
