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

            field = type.GetField("normalTexture", flag);
            app.RegisterCLRFieldGetter(field, get_normalTexture_0);
            app.RegisterCLRFieldSetter(field, set_normalTexture_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_normalTexture_0, AssignFromStack_normalTexture_0);
            field = type.GetField("BoomTexture", flag);
            app.RegisterCLRFieldGetter(field, get_BoomTexture_1);
            app.RegisterCLRFieldSetter(field, set_BoomTexture_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_BoomTexture_1, AssignFromStack_BoomTexture_1);
            field = type.GetField("BlackHoleTexture", flag);
            app.RegisterCLRFieldGetter(field, get_BlackHoleTexture_2);
            app.RegisterCLRFieldSetter(field, set_BlackHoleTexture_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_BlackHoleTexture_2, AssignFromStack_BlackHoleTexture_2);
            field = type.GetField("LazerHTexture", flag);
            app.RegisterCLRFieldGetter(field, get_LazerHTexture_3);
            app.RegisterCLRFieldSetter(field, set_LazerHTexture_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_LazerHTexture_3, AssignFromStack_LazerHTexture_3);
            field = type.GetField("LazerVTexture", flag);
            app.RegisterCLRFieldGetter(field, get_LazerVTexture_4);
            app.RegisterCLRFieldSetter(field, set_LazerVTexture_4);
            app.RegisterCLRFieldBinding(field, CopyToStack_LazerVTexture_4, AssignFromStack_LazerVTexture_4);


        }



        static object get_normalTexture_0(ref object o)
        {
            return ((ET.DiamondLibrary)o).normalTexture;
        }

        static StackObject* CopyToStack_normalTexture_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.DiamondLibrary)o).normalTexture;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_normalTexture_0(ref object o, object v)
        {
            ((ET.DiamondLibrary)o).normalTexture = (UnityEngine.Sprite)v;
        }

        static StackObject* AssignFromStack_normalTexture_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Sprite @normalTexture = (UnityEngine.Sprite)typeof(UnityEngine.Sprite).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((ET.DiamondLibrary)o).normalTexture = @normalTexture;
            return ptr_of_this_method;
        }

        static object get_BoomTexture_1(ref object o)
        {
            return ((ET.DiamondLibrary)o).BoomTexture;
        }

        static StackObject* CopyToStack_BoomTexture_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.DiamondLibrary)o).BoomTexture;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_BoomTexture_1(ref object o, object v)
        {
            ((ET.DiamondLibrary)o).BoomTexture = (UnityEngine.Sprite)v;
        }

        static StackObject* AssignFromStack_BoomTexture_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Sprite @BoomTexture = (UnityEngine.Sprite)typeof(UnityEngine.Sprite).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((ET.DiamondLibrary)o).BoomTexture = @BoomTexture;
            return ptr_of_this_method;
        }

        static object get_BlackHoleTexture_2(ref object o)
        {
            return ((ET.DiamondLibrary)o).BlackHoleTexture;
        }

        static StackObject* CopyToStack_BlackHoleTexture_2(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.DiamondLibrary)o).BlackHoleTexture;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_BlackHoleTexture_2(ref object o, object v)
        {
            ((ET.DiamondLibrary)o).BlackHoleTexture = (UnityEngine.Sprite)v;
        }

        static StackObject* AssignFromStack_BlackHoleTexture_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Sprite @BlackHoleTexture = (UnityEngine.Sprite)typeof(UnityEngine.Sprite).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((ET.DiamondLibrary)o).BlackHoleTexture = @BlackHoleTexture;
            return ptr_of_this_method;
        }

        static object get_LazerHTexture_3(ref object o)
        {
            return ((ET.DiamondLibrary)o).LazerHTexture;
        }

        static StackObject* CopyToStack_LazerHTexture_3(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.DiamondLibrary)o).LazerHTexture;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_LazerHTexture_3(ref object o, object v)
        {
            ((ET.DiamondLibrary)o).LazerHTexture = (UnityEngine.Sprite)v;
        }

        static StackObject* AssignFromStack_LazerHTexture_3(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Sprite @LazerHTexture = (UnityEngine.Sprite)typeof(UnityEngine.Sprite).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((ET.DiamondLibrary)o).LazerHTexture = @LazerHTexture;
            return ptr_of_this_method;
        }

        static object get_LazerVTexture_4(ref object o)
        {
            return ((ET.DiamondLibrary)o).LazerVTexture;
        }

        static StackObject* CopyToStack_LazerVTexture_4(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((ET.DiamondLibrary)o).LazerVTexture;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_LazerVTexture_4(ref object o, object v)
        {
            ((ET.DiamondLibrary)o).LazerVTexture = (UnityEngine.Sprite)v;
        }

        static StackObject* AssignFromStack_LazerVTexture_4(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            UnityEngine.Sprite @LazerVTexture = (UnityEngine.Sprite)typeof(UnityEngine.Sprite).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((ET.DiamondLibrary)o).LazerVTexture = @LazerVTexture;
            return ptr_of_this_method;
        }



    }
}
