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
    unsafe class ET_DiamondSprite_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(ET.DiamondSprite);

            field = type.GetField("Sprite", flag);
            app.RegisterCLRFieldGetter(field, get_Sprite_0);
            app.RegisterCLRFieldSetter(field, set_Sprite_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_Sprite_0, AssignFromStack_Sprite_0);


        }



        static object get_Sprite_0(ref object o)
        {
            return ((ET.DiamondSprite)o).Sprite;
        }

        static StackObject* CopyToStack_Sprite_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.DiamondSprite)o).Sprite;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_Sprite_0(ref object o, object v)
        {
            ((ET.DiamondSprite)o).Sprite = (UnityEngine.Sprite)v;
        }

        static StackObject* AssignFromStack_Sprite_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Sprite @Sprite = (UnityEngine.Sprite)typeof(UnityEngine.Sprite).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((ET.DiamondSprite)o).Sprite = @Sprite;
            return ptr_of_this_method;
        }



    }
}
