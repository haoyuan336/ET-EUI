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
    unsafe class UnityEngine_ResourceManagement_AsyncOperations_DownloadStatus_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            FieldInfo field;
            Type[] args;
            Type type = typeof(UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus);
            args = new Type[]{};
            method = type.GetMethod("get_Percent", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_Percent_0);

            field = type.GetField("TotalBytes", flag);
            app.RegisterCLRFieldGetter(field, get_TotalBytes_0);
            app.RegisterCLRFieldSetter(field, set_TotalBytes_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_TotalBytes_0, AssignFromStack_TotalBytes_0);
            field = type.GetField("DownloadedBytes", flag);
            app.RegisterCLRFieldGetter(field, get_DownloadedBytes_1);
            app.RegisterCLRFieldSetter(field, set_DownloadedBytes_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_DownloadedBytes_1, AssignFromStack_DownloadedBytes_1);

            app.RegisterCLRCreateDefaultInstance(type, () => new UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus());


        }

        static void WriteBackInstance(ILRuntime.Runtime.Enviorment.AppDomain __domain, StackObject* ptr_of_this_method, IList<object> __mStack, ref UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus instance_of_this_method)
        {
            ptr_of_this_method = ILIntepreter.GetObjectAndResolveReference(ptr_of_this_method);
            switch(ptr_of_this_method->ObjectType)
            {
                case ObjectTypes.Object:
                    {
                        __mStack[ptr_of_this_method->Value] = instance_of_this_method;
                    }
                    break;
                case ObjectTypes.FieldReference:
                    {
                        var ___obj = __mStack[ptr_of_this_method->Value];
                        if(___obj is ILTypeInstance)
                        {
                            ((ILTypeInstance)___obj)[ptr_of_this_method->ValueLow] = instance_of_this_method;
                        }
                        else
                        {
                            var t = __domain.GetType(___obj.GetType()) as CLRType;
                            t.SetFieldValue(ptr_of_this_method->ValueLow, ref ___obj, instance_of_this_method);
                        }
                    }
                    break;
                case ObjectTypes.StaticFieldReference:
                    {
                        var t = __domain.GetType(ptr_of_this_method->Value);
                        if(t is ILType)
                        {
                            ((ILType)t).StaticInstance[ptr_of_this_method->ValueLow] = instance_of_this_method;
                        }
                        else
                        {
                            ((CLRType)t).SetStaticFieldValue(ptr_of_this_method->ValueLow, instance_of_this_method);
                        }
                    }
                    break;
                 case ObjectTypes.ArrayReference:
                    {
                        var instance_of_arrayReference = __mStack[ptr_of_this_method->Value] as UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus[];
                        instance_of_arrayReference[ptr_of_this_method->ValueLow] = instance_of_this_method;
                    }
                    break;
            }
        }

        static StackObject* get_Percent_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 1);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            ptr_of_this_method = ILIntepreter.GetObjectAndResolveReference(ptr_of_this_method);
            UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus instance_of_this_method = (UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus)typeof(UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)16);

            var result_of_this_method = instance_of_this_method.Percent;

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            WriteBackInstance(__domain, ptr_of_this_method, __mStack, ref instance_of_this_method);

            __intp.Free(ptr_of_this_method);
            __ret->ObjectType = ObjectTypes.Float;
            *(float*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }


        static object get_TotalBytes_0(ref object o)
        {
            return ((UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus)o).TotalBytes;
        }

        static StackObject* CopyToStack_TotalBytes_0(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus)o).TotalBytes;
            __ret->ObjectType = ObjectTypes.Long;
            *(long*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_TotalBytes_0(ref object o, object v)
        {
            UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus ins =(UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus)o;
            ins.TotalBytes = (System.Int64)v;
            o = ins;
        }

        static StackObject* AssignFromStack_TotalBytes_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int64 @TotalBytes = *(long*)&ptr_of_this_method->Value;
            UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus ins =(UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus)o;
            ins.TotalBytes = @TotalBytes;
            o = ins;
            return ptr_of_this_method;
        }

        static object get_DownloadedBytes_1(ref object o)
        {
            return ((UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus)o).DownloadedBytes;
        }

        static StackObject* CopyToStack_DownloadedBytes_1(ref object o, ILIntepreter __intp, StackObject* __ret, IList<object> __mStack)
        {
            var result_of_this_method = ((UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus)o).DownloadedBytes;
            __ret->ObjectType = ObjectTypes.Long;
            *(long*)&__ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_DownloadedBytes_1(ref object o, object v)
        {
            UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus ins =(UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus)o;
            ins.DownloadedBytes = (System.Int64)v;
            o = ins;
        }

        static StackObject* AssignFromStack_DownloadedBytes_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, IList<object> __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int64 @DownloadedBytes = *(long*)&ptr_of_this_method->Value;
            UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus ins =(UnityEngine.ResourceManagement.AsyncOperations.DownloadStatus)o;
            ins.DownloadedBytes = @DownloadedBytes;
            o = ins;
            return ptr_of_this_method;
        }



    }
}
