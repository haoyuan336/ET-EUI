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
    unsafe class ET_DiamondLibrary_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(ET.DiamondLibrary);

            field = type.GetField("DiamondSpriteMap", flag);
            app.RegisterCLRFieldGetter(field, get_DiamondSpriteMap_0);
            app.RegisterCLRFieldSetter(field, set_DiamondSpriteMap_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_DiamondSpriteMap_0, AssignFromStack_DiamondSpriteMap_0);


        }



        static object get_DiamondSpriteMap_0(ref object o)
        {
            return ((ET.DiamondLibrary)o).DiamondSpriteMap;
        }

        static StackObject* CopyToStack_DiamondSpriteMap_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.DiamondLibrary)o).DiamondSpriteMap;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_DiamondSpriteMap_0(ref object o, object v)
        {
            ((ET.DiamondLibrary)o).DiamondSpriteMap = (System.Collections.Generic.Dictionary<System.Int32, ET.DiamondSprite>)v;
        }

        static StackObject* AssignFromStack_DiamondSpriteMap_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.Dictionary<System.Int32, ET.DiamondSprite> @DiamondSpriteMap = (System.Collections.Generic.Dictionary<System.Int32, ET.DiamondSprite>)typeof(System.Collections.Generic.Dictionary<System.Int32, ET.DiamondSprite>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((ET.DiamondLibrary)o).DiamondSpriteMap = @DiamondSpriteMap;
            return ptr_of_this_method;
        }



    }
}
