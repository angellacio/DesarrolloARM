//@(#)SCADE2(W:SKD08212CO2:SAT.DyP.Util.Service.WCF:ClassBuilder:0:21/May/2008[SAT.DyP.Util.Service.WCF:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using System.Collections;
using System.ServiceModel;
using System.Threading;

namespace SAT.DyP.Util.Service.WCF.DynamicClientProxy.Internal
{
	/// <summary>
	/// Helper to emit classes.	
	/// </summary>
	internal abstract class ClassBuilder<TInterface> where TInterface : class 
	{
		private static string _generatedAssemblyName = "DyP.WCF.DynamicClientProxy.Generated.";

		private const MethodAttributes _defaultMethodAttributes = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.NewSlot;

		private static bool _saveGeneratedAssembly = true;	// set to true to save the generated assembly to the disk

		private static readonly IDictionary<string,Type> _generatedTypes = new Dictionary<string,Type>();
		private static readonly object _generateLock = new object();

		private static AssemblyBuilder _assemblyBuilder;
		private static AssemblyName _assemblyName;
		private static ModuleBuilder _moduleBuilder;

		private readonly Type _baseClassType;

		private readonly string _generatedClassName;
		private readonly Type _iType;

		private readonly string _classNameSuffix;

		protected ClassBuilder(Type baseClassType, string classNameSuffix)
		{
			this._baseClassType = baseClassType;
			this._classNameSuffix = classNameSuffix;
			_iType = typeof(TInterface);
			if (_iType.Name.StartsWith("I") && char.IsUpper(_iType.Name, 1))
			{
				_generatedClassName = _iType.Namespace + "." + _iType.Name.Substring(1) + classNameSuffix;
			}
			else
			{
				_generatedClassName = typeof(TInterface).FullName + classNameSuffix;
			}
			_generatedAssemblyName = _generatedAssemblyName + _generatedClassName;
		}

		#region Properties
		public string GeneratedClassName
		{
			get { return this._generatedClassName; }
		}
		#endregion

		#region Generate Type
		public Type GenerateType()
		{
			Type type = TryGetType();
			if (type != null)
				return type;

			lock(_generateLock)
			{
				type = TryGetType();
				if (type != null)
					return type;

				GenerateAssembly();

				// Generate a new type
				type = GenerateTypeImplementation();
				lock (_generatedTypes)
				{
					_generatedTypes[_generatedClassName] = type;
				}
				return type;
			}
		}

		private Type TryGetType()
		{
			lock (_generatedTypes)
			{
				Type generatedType = null;
				if (_generatedTypes.TryGetValue(_generatedClassName, out generatedType))
					return generatedType;
			}
			return null;
		}
		#endregion

		private static void GenerateAssembly()
		{
			if (_assemblyBuilder == null)
			{
				_assemblyName = new AssemblyName();
				_assemblyName.Name = _generatedAssemblyName;
				_assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(_assemblyName, _saveGeneratedAssembly ? AssemblyBuilderAccess.RunAndSave : AssemblyBuilderAccess.Run);
				if (_saveGeneratedAssembly)
				{
					_moduleBuilder = _assemblyBuilder.DefineDynamicModule(_generatedAssemblyName, _generatedAssemblyName + ".dll");
				}
				else
				{
					_moduleBuilder = _assemblyBuilder.DefineDynamicModule(_generatedAssemblyName);
				}
			}
		}


		/// <summary>
		/// Generate a type declare like
		/// public class MyType : ClientBase < ITheInterface >, ITheInterface 
		/// that implements the interface for us
		/// </summary>
		/// <returns></returns>
		private Type GenerateTypeImplementation()
		{
			TypeBuilder builder = _moduleBuilder.DefineType(_generatedClassName, TypeAttributes.Public, _baseClassType);
			builder.AddInterfaceImplementation( _iType );	// implement the interface

			GenerateConstructor(builder);

			GenerateMethodImpl(builder);

			Type generatedType = builder.CreateType();

			return generatedType;
		}

		public static void SaveGeneratedAssembly()
		{
			if (_saveGeneratedAssembly)
			{
				_assemblyBuilder.Save(_generatedAssemblyName + ".dll");
				_assemblyBuilder = null;	// reset
			}
		}

		/// <summary>
		/// Read the interface declaration and emit a method for each method declaration
		/// </summary>
		/// <param name="builder"></param>
		protected virtual void GenerateMethodImpl(TypeBuilder builder)
		{
			GenerateMethodImpl(builder, _iType);
		}

		protected virtual void GenerateMethodImpl(TypeBuilder builder, Type currentType)
		{
			MethodInfo[] methods = currentType.GetMethods();
			foreach (MethodInfo method in methods)
			{
				Type[] parameterTypes = GetParameters(method.GetParameters());	// declare the method with the correct parameters
				MethodBuilder methodBuilder = builder.DefineMethod(method.Name, _defaultMethodAttributes, method.ReturnType, parameterTypes);

				// Start building the method
				methodBuilder.CreateMethodBody(null, 0);
				ILGenerator iLGenerator = methodBuilder.GetILGenerator();

				GenerateMethodImpl(method, parameterTypes, iLGenerator);

				// declare that we override the interface method
				builder.DefineMethodOverride(methodBuilder, method);
			}

			Type[] inheritedInterfaces = currentType.GetInterfaces();
			foreach (Type inheritedInterface in inheritedInterfaces)
			{
				GenerateMethodImpl(builder, inheritedInterface);
			}
		}

		protected abstract void GenerateMethodImpl(MethodInfo method, Type[] parameterTypes, ILGenerator iLGenerator);

		protected bool IsVoidMethod(MethodInfo methodInfo)
		{
			if (methodInfo.ReturnType == typeof(void))
				return true;
			else
				return false;
		}
		protected MethodInfo GetMethodFromBaseClass(string methodName)
		{
			return _baseClassType.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty);
		}

		/// <summary>
		/// Simply generate a constructor:
		/// public MyClass(string configName)
		///		: base ( configName )
		/// {
		/// }
		/// </summary>
		/// <param name="builder"></param>
		private void GenerateConstructor(TypeBuilder builder)
		{
			// Define the constructor
			Type[] constructorParameters = new Type[] { typeof(string) };
			ConstructorBuilder constructorBuilder = builder.DefineConstructor(MethodAttributes.Public | MethodAttributes.RTSpecialName, CallingConventions.Standard, constructorParameters);
			ILGenerator iLGenerator = constructorBuilder.GetILGenerator();

			iLGenerator.Emit(OpCodes.Ldarg_0);	// this
			iLGenerator.Emit(OpCodes.Ldarg_1);	// load the param

			// Call the base constructor
			ConstructorInfo originalConstructor = _baseClassType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, constructorParameters, null);
			iLGenerator.Emit(OpCodes.Call, originalConstructor);

			iLGenerator.Emit(OpCodes.Ret);
		}

		#region Utils
		private static Type[] GetParameters(ParameterInfo[] declareParams)
		{
			List<Type> parameters = new List<Type>();
			foreach (ParameterInfo param in declareParams)
			{
				parameters.Add(param.ParameterType);
			}
			return parameters.ToArray();
		}
		#endregion
	}
}
